using Microsoft.AspNetCore.Http;

namespace dotnetApp.Dtos
{
  public class ImageUpload
  {
    public IFormFile image { get; set; }
  }
}
