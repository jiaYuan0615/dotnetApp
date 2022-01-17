using dotnetApp.Services;
using Microsoft.AspNetCore.Http;

namespace dotnetApp.Dtos.Member
{
  public class MemberRegister
  {
    public string email { get; set; }
    public string password { get; set; }
    public IFormFile avatar { get; set; }
    public string name { get; set; }
    public string gender { get; set; }
  }
}
