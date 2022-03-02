using Microsoft.AspNetCore.Http;

namespace dotnetApp.dotnetApp.Dtos.Group
{
  public class GroupCreate
  {
    public string name { get; set; }
    public IFormFile avatar { get; set; }
    public string biography { get; set; }
    public string publishYear { get; set; }
  }
}
