using System;
using System.IO;
using System.Threading.Tasks;
using dotnetApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace dotnetApp.Services
{
  public class FileService
  {
    private readonly string _folder;
    public FileService(
      IWebHostEnvironment env
      )
    {
      _folder = env.WebRootPath;
    }
    public async Task<Image> UploadImage(string folder, IFormFile file)
    {
      string imageId = Guid.NewGuid().ToString();
      // 不一定需要使用絕對路徑
      // string targetPath = Path.GetFullPath($"{_folder}/{folder}");
      string targetPath = $"{_folder}/{folder}";
      if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
      string path = Path.Combine(targetPath, $"{imageId}{Path.GetExtension(file.FileName)}");
      using (FileStream stream = new FileStream(path, FileMode.Create))
      {
        await file.CopyToAsync(stream);
      }

      Image image = new Image()
      {
        id = Guid.Parse(imageId),
        FileName = file.FileName,
        path = path,
        Length = (int)file.Length,
        ContentType = file.ContentType,
      };
      return image;
    }

    public Boolean DeleteImage(string path)
    {
      if (!File.Exists(path)) return false;
      File.Delete(path);
      return true;
    }
  }

}
