namespace invest_api.SQL
{
  public class User
  {
    public int Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }

  }
}
