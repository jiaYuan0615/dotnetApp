using dotnetApp.Services;

namespace dotnetApp.Dtos.Member
{
  public class MemberRegister
  {
    PasswordService passwordService = new PasswordService();
    private string hash;
    public string email { get; set; }
    public string password
    {
      get
      {
        return hash;
      }
      set
      {
        hash = passwordService.HashPassword(value);
      }
    }
    public string avatar { get; set; }
    public string name { get; set; }
    public string gender { get; set; }
  }
}
