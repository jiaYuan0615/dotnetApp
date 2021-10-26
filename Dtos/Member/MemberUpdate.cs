using Microsoft.AspNetCore.Http;

namespace dotnetApp.Dtos.Member
{
  public class MemberUpdate
  {
    public string email { get; set; }
    public string name { get; set; }
    public IFormFile avatar { get; set; }
  }
}
