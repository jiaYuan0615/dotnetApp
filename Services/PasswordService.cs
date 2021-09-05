namespace dotnetApp.Services
{
  public class PasswordService
  {
    public bool CheckPassword(string plain, string hash)
    {
      return BCrypt.Net.BCrypt.Verify(plain, hash);
    }

    public string HashPassword(string plain)
    {
      return BCrypt.Net.BCrypt.HashPassword(plain);
    }
  }
}
