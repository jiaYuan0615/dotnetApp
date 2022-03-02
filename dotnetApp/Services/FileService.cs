using System;
using System.IO;
using dotnetApp.dotnetApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace dotnetApp.dotnetApp.Services
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
    public Image UploadImage(string folder, IFormFile file)
    {
      string imageId = Guid.NewGuid().ToString();
      // 不一定需要使用絕對路徑
      // string targetPath = Path.GetFullPath($"{_folder}/{folder}");
      string targetPath = $"{_folder}/{folder}";

      if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
      string path = Path.Combine(targetPath, $"{imageId}{Path.GetExtension(file.FileName)}");
      // 區塊使用 using 會在結束時自動釋放記憶體 （因為實作 IDisposable 介面的關係)
      using (FileStream stream = new FileStream(path, FileMode.Create))
      {
        file.CopyTo(stream);
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
