
using Google.Rpc;
using invest_api.Common;
using invest_api.Helpers;
using invest_api.SQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static Google.Rpc.Context.AttributeContext.Types;

namespace invest_api.Controllers
{
  [Route("api/auth")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IConfiguration _config;
    public AuthController(IConfiguration config)
    {
      _config = config;
    }
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto request)
    {
      var passwordHash = PasswordHelper.HashPassword(request.Password);

      var user = new User{ Login = request.login, PasswordHash = passwordHash };

      var res = await AuthDBService.WriteUserAsync(user);

      var savedUser = await AuthDBService.GetUserAsync(user.Login);

      if (res.Status == Enum.ECommonStatus.Ok && savedUser != null)
      {
        var token = GenerateJwtToken(savedUser);

        var refreshToken = GenerateRefreshToken();
        var expiryDate = DateTime.UtcNow.AddDays(14);

        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
          HttpOnly = true,
          Secure = true,
          SameSite = SameSiteMode.None,
          Expires = expiryDate
        }); 
        await AuthDBService.SaveRefreshTokenAsync(savedUser.Id, refreshToken, expiryDate);

        return Ok(new CommonResponse(Enum.ECommonStatus.Ok, $"Пользователь {request.login} успешно зарегистрирован ", new AuthResponse (token, refreshToken)));
      }

      return BadRequest(new CommonResponse(Enum.ECommonStatus.Error, res.Message));
    }
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDto request)
    {
      var user = await AuthDBService.GetUserAsync(request.login);
      if (user == null) {
        return BadRequest(new CommonResponse(Enum.ECommonStatus.Error, $"Пользователя {request.login} не существует"));
      } else {
        var isPassCorrect = PasswordHelper.VerifyPassword(request.Password, user?.PasswordHash);
        if (!isPassCorrect) {
          return BadRequest(new CommonResponse(Enum.ECommonStatus.Error, $"Пароль/логин не верный"));
        }
        var token = GenerateJwtToken(user);
        var refreshToken = GenerateRefreshToken();
        var expiryDate = DateTime.UtcNow.AddDays(14);

        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
          HttpOnly = true,
          Secure = true,
          SameSite = SameSiteMode.None,
          Expires = expiryDate
        });

        await AuthDBService.SaveRefreshTokenAsync(user.Id, refreshToken, expiryDate);

        return Ok(new CommonResponse(Enum.ECommonStatus.Ok, $"Здравствуйте {request.login}!", new { accessToken = token }));
      }
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
      var refreshToken = Request.Cookies["refreshToken"];

      if (string.IsNullOrEmpty(refreshToken))
      {
        return BadRequest(new CommonResponse(Enum.ECommonStatus.Error, "Refresh token is missing"));
      }

      var userId = await AuthDBService.GetUserIdFromRefreshTokenAsync(refreshToken);  // Получаем ID пользователя по refresh токену
      if (userId == null)
      {
        return BadRequest(new CommonResponse(Enum.ECommonStatus.Error, "Invalid refresh token"));
      }

      // Генерация нового access токена
      var user = await AuthDBService.GetUserByIdAsync(userId);
      var newAccessToken = GenerateJwtToken(user);

      // Генерация нового refresh токена и его сохранение
      var newRefreshToken = GenerateRefreshToken();
      var expiryDate = DateTime.UtcNow.AddDays(14);
      await AuthDBService.UpdateRefreshTokenAsync(user.Id, newRefreshToken, expiryDate);

      // Сохраняем новый refreshToken в куки
      Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
      {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.None,
        Expires = expiryDate
      });

      return Ok(new CommonResponse(Enum.ECommonStatus.Ok, $"RefreshToken ok", new { accessToken = newAccessToken }));
    }

    private string GenerateJwtToken(User user)
    {
      var secretKey = Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]);
      var claims = new[]
      {
        new Claim(ClaimTypes.Name, user.Login),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
      };

      var credentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256);
      var token = new JwtSecurityToken(
          claims: claims,
          expires: DateTime.UtcNow.AddMinutes(60),
          signingCredentials: credentials
      );
      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
      var randomNumber = new byte[32];

      // Используем RandomNumberGenerator для создания криптографически безопасных случайных чисел
      using (var rng = RandomNumberGenerator.Create())
      {
        rng.GetBytes(randomNumber);
      }
      return Convert.ToBase64String(randomNumber);
    }


  }
}
