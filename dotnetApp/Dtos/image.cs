using Microsoft.AspNetCore.Http;

namespace dotnetApp.dotnetApp.Dtos
{
  public class ImageUpload
  {
    public IFormFile image { get; set; }
  }
}
