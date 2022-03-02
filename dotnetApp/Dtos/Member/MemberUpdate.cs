using Microsoft.AspNetCore.Http;

namespace dotnetApp.dotnetApp.Dtos.Member
{
  public class MemberUpdate
  {
    public string name { get; set; }
    public IFormFile avatar { get; set; }
  }
}
