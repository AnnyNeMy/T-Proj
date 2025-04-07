namespace invest_api.Common
{
  public class AuthResponse
  {
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

    public AuthResponse(string accessToken, string refreshToken)
    {
      AccessToken = accessToken;
      RefreshToken = refreshToken;
    }

  }
}
