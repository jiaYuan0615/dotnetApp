using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using dotnetApp.Dtos;
using dotnetApp.Helpers;
using dotnetApp.Models;
using dotnetApp.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnetApp.Controllers
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
    private readonly string _folder;
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
      IWebHostEnvironment env
      )
    {
      _imageService = imageService;
      _logger = logger;
      _mapper = mapper;
      _folder = $"{env.WebRootPath}/storage/image";
    }

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

    [HttpPost]
    public async Task<IActionResult> PostImage([FromForm] ImageUpload imageUpload)
    {
      try
      {
        Image image = _mapper.Map<Image>(imageUpload);
        await _imageService.PostImage(image);
        return Ok(new { message = "上傳圖片成功" });
      }
      catch (System.Exception)
      {
        throw new AppException("上傳圖片失敗");
      }
    }
  }
}
