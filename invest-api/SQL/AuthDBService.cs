using invest_api.Common;
using invest_api.Enum;
using Npgsql;

namespace invest_api.SQL
{
  public class AuthDBService
  {
    public static async Task<User> GetUserAsync(string login)
    {
      try
      {
       var user = new User();
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "select Id, Login, PasswordHash, CreatedAt from public.Users where Login = @login;";

        using (var cmd = new NpgsqlCommand(cmdStr, conn))
        {
          cmd.Parameters.AddWithValue("login", login);

          var reader = await cmd.ExecuteReaderAsync();

          int count = 0;

          while (await reader.ReadAsync())
          {
            if (count > 0)
            {
              throw new Exception("Ошибка: найдено несколько пользователей с одним логином!");
            }

            count++;

            var id = reader.GetInt32(0);
            var lod = reader.GetString(1);
            var passHesh = reader.GetString(2);
            var date = reader.GetDateTime(3);

            user = new User()
            {
              Id = id,
              Login = login,
              PasswordHash = passHesh,
              CreatedAt = date,
            };
           };
        if (count == 0) return null;
        }
        return user;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public static async Task<User> GetUserByIdAsync(int id)
    {
      try
      {
        var user = new User();
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "select Id, Login, PasswordHash, CreatedAt from public.Users where Id = @id;";

        using (var cmd = new NpgsqlCommand(cmdStr, conn))
        {
          cmd.Parameters.AddWithValue("id", id);

          var reader = await cmd.ExecuteReaderAsync();

          int count = 0;

          while (await reader.ReadAsync())
          {
            if (count > 0)
            {
              throw new Exception("Ошибка: найдено несколько пользователей с одним id!");
            }

            count++;

            var iserId = reader.GetInt32(0);
            var log = reader.GetString(1);
            var passHesh = reader.GetString(2);
            var date = reader.GetDateTime(3);

            user = new User()
            {
              Id = iserId,
              Login = log,
              PasswordHash = passHesh,
              CreatedAt = date,
            };
          };
          if (count == 0) return null;
        }
        return user;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public static async Task<CommonResponse> WriteUserAsync(User user)
    {
      var login = user.Login;
      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStrFind = "SELECT COUNT(*) FROM public.Users WHERE Login = @login";
        await using var cmdFind = new NpgsqlCommand(cmdStrFind, conn);
        cmdFind.Parameters.AddWithValue("@login", login);

        var res = await cmdFind.ExecuteScalarAsync() ?? 0;
        int count = Convert.ToInt32(res);

        if (count > 0)
        {
          return new CommonResponse(ECommonStatus.Error, $"Ошибка добавления {login}, так как уже найден пользователь с таким логином");
        }

        else {  
          var cmdStr = "INSERT INTO Users (Login, PasswordHash, CreatedAt)" +
          "VALUES (@Login, @PasswordHash, @CreatedAt)";
          await using var cmd = new NpgsqlCommand(cmdStr, conn);
          cmd.Parameters.AddWithValue("Login", user.Login);
          cmd.Parameters.AddWithValue("PasswordHash", user.PasswordHash);
          cmd.Parameters.AddWithValue("CreatedAt", DateTime.Now);

          var result = await cmd.ExecuteNonQueryAsync();

          return result > 0 ? new CommonResponse(ECommonStatus.Ok, $" Пользователь {login} успешно добавлен") : new CommonResponse(ECommonStatus.Error, $"Ошибка добавления пользователя {login}");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return new CommonResponse(ECommonStatus.Error, $"Ошибка добавления {user.Login}, Exeption: {ex.Message}");
      }
    }

    public static async Task SaveRefreshTokenAsync(int userId, string refreshToken, DateTime expiryDate)
    {
      using (var conn = new NpgsqlConnection(CommonVariables.ConnectionString))
      {
        await conn.OpenAsync();

        var cmdStr = "INSERT INTO RefreshTokens (UserId, Token, ExpiryDate) VALUES (@UserId, @Token, @ExpiryDate)";
        using (var cmd = new NpgsqlCommand(cmdStr, conn))
        {
          cmd.Parameters.AddWithValue("@UserId", userId);
          cmd.Parameters.AddWithValue("@Token", refreshToken);
          cmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);
          await cmd.ExecuteNonQueryAsync();
        }
      }
    }

    public static async Task UpdateRefreshTokenAsync(int userId, string refreshToken, DateTime expiryDate)
    {
      using (var conn = new NpgsqlConnection(CommonVariables.ConnectionString))
      {
        await conn.OpenAsync();

        var deleteCmdStr = "DELETE FROM public.RefreshTokens WHERE UserId = @UserId";
        using (var deleteCmd = new NpgsqlCommand(deleteCmdStr, conn))
        {
          deleteCmd.Parameters.AddWithValue("@UserId", userId);
          await deleteCmd.ExecuteNonQueryAsync();
        }

        // Добавляем новый refresh токен
        var insertCmdStr = "INSERT INTO RefreshTokens (UserId, Token, ExpiryDate) VALUES (@UserId, @Token, @ExpiryDate)";
        using (var insertCmd = new NpgsqlCommand(insertCmdStr, conn))
        {
          insertCmd.Parameters.AddWithValue("@UserId", userId);
          insertCmd.Parameters.AddWithValue("@Token", refreshToken);
          insertCmd.Parameters.AddWithValue("@ExpiryDate", expiryDate);
          await insertCmd.ExecuteNonQueryAsync();
        }
      }
    }

    public static async Task<string> GetRefreshTokenAsync(int userId)
    {
      using (var conn = new NpgsqlConnection(CommonVariables.ConnectionString))
      {
        await conn.OpenAsync();

        var cmdStr = "SELECT Token FROM public.RefreshTokens WHERE UserId = @UserId AND ExpiryDate > @CurrentDate";
        using (var cmd = new NpgsqlCommand(cmdStr, conn))
        {
          cmd.Parameters.AddWithValue("@UserId", userId);
          cmd.Parameters.AddWithValue("@CurrentDate", DateTime.UtcNow);

          var result = await cmd.ExecuteScalarAsync();
          return result?.ToString();
        }
      }
    }

    public static async Task<int> GetUserIdFromRefreshTokenAsync(string refreshToken)
    {
      using (var conn = new NpgsqlConnection(CommonVariables.ConnectionString))
      {
        await conn.OpenAsync();

        var cmdStr = "SELECT UserId FROM public.RefreshTokens WHERE Token = @Token AND ExpiryDate > @CurrentDate";
        using (var cmd = new NpgsqlCommand(cmdStr, conn))
        {
          cmd.Parameters.AddWithValue("@Token", refreshToken);
          cmd.Parameters.AddWithValue("@CurrentDate", DateTime.UtcNow);

          var result = await cmd.ExecuteScalarAsync();
          return result != null ? Convert.ToInt32(result) : 0;
        }
      }
    }

  }
}
