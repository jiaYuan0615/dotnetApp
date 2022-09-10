using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using dotnetApp.dotnetApp.Dtos;
using dotnetApp.dotnetApp.Helpers;
using dotnetApp.dotnetApp.Models;
using dotnetApp.dotnetApp.Services;
using dotnetApp.dotnetApp.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnetApp.dotnetApp.Controllers
{
  [Route("api/[controller]")]
  // [Consumes("application/json")]
  [ApiController]
  public class ImageController : ControllerBase
  {
    private ImageService _imageService;
    // GetFullPath 配上路徑會直接回傳該路徑位置
    private readonly string defaultImage = Path.GetFullPath("wwwroot/storage/404.png");
    private ILogger<ImageController> _logger;
    private readonly IMapper _mapper;
    private readonly FileService _fileService;
    private readonly string _folder;
    private readonly string _path;
    private readonly IHttpClientFactory _httpClientFactory;

    private readonly static Dictionary<string, string> _contentTypes = new Dictionary<string, string>
        {
            {".png", "image/png"},
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"},
        };
    public ImageController(
      ImageService imageService,
      IMapper mapper,
      ILogger<ImageController> logger,
      FileService fileService,
      IWebHostEnvironment env,
      IHttpClientFactory httpClientFactory
      )
    {
      _imageService = imageService;
      _logger = logger;
      _mapper = mapper;
      _fileService = fileService;
      _folder = $"{env.WebRootPath}/storage/image";
      _path = $"{env.WebRootPath}/storage/";
      _httpClientFactory = httpClientFactory;
    }

    // Get api/image/{id}
    [HttpGet("{id}")]
    public IActionResult GetImage(string id)
    {
      try
      {
        Image item = _imageService.GetAssignImageById(Guid.Parse(id));
        if (item == null) throw new Exception("找不到該圖片");
        FileStream image = System.IO.File.OpenRead(item.path);
        return File(image, item.ContentType);
      }
      catch (System.Exception)
      {
        FileStream image = System.IO.File.OpenRead(defaultImage);
        return File(image, "image/png");
      }
    }

    [HttpGet]
    public async Task<IActionResult> TestPostImage()
    {
      try
      {
        // Currently call self api is not work
        // solution is apply ssl certificate
        HttpClient httpClient = _httpClientFactory.CreateClient();
        string url = "https://localhost:5000/api/image";
        string target = Path.Combine(_path, "404.png");
        FileStream stream = System.IO.File.OpenRead(target);

        HttpResponseMessage response = null;
        StreamContent image = new StreamContent(stream);
        // 需要加上 ContentType 才不會出錯
        // 呼叫其他服務則可加可不加
        image.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        using (var content = new MultipartFormDataContent())
        {
          content.Add(image, "image", Path.GetFileName(target));
          // httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
          response = await httpClient.PostAsync(url, content);
          content.Dispose();
        }
        return Ok(new
        {
          message = "測試通過",
          response = response.IsSuccessStatusCode
        });
      }
      catch (System.Exception)
      {
        throw new AppException("測試失敗");
      }
    }

    // Post api/image
    [HttpPost]
    public async Task<IActionResult> PostImage([FromForm] ImageUpload imageUpload)
    {
      string _method = "上傳圖片";
      try
      {
        // Image image = _mapper.Map<Image>(imageUpload.image);
        Image image = _fileService.UploadImage("image", imageUpload.image);
        await _imageService.PostImage(image);
        return Ok(new
        {
          message = $"{_method}成功",
          imageId = image.id.ToString()
        });
      }
      catch (System.Exception)
      {
        throw new AppException($"{_method}失敗");
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteImage(string id)
    {
      string _method = "刪除圖片";
      try
      {
        Image image = _imageService.GetAssignImageById(Guid.Parse(id));
        if (image == null) return NotFound(new { message = "找不到圖片" });
        bool isRemove = _fileService.DeleteImage(image.path);
        if (!isRemove) return NotFound(new { message = "刪除圖片失敗" });
        await _imageService.DelteImage(image);
        return Ok(new { message = $"{_method}成功" });
      }
      catch (System.Exception)
      {
        _logger.LogError(LogEvent.error, $"執行{_method} 出現例外錯誤");
        throw new AppException($"{_method}失敗");
      }
    }
  }
}
