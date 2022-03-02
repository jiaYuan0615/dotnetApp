namespace dotnetApp.dotnetApp.Services
{
  public interface IPasswordService
  {
    bool CheckPassword(string plain, string hash);
    string HashPassword(string plain);
  }
  public class PasswordService : IPasswordService
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
