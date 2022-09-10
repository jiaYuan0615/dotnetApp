using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using dotnetApp.dotnetApp.Dtos.Singer;
using dotnetApp.dotnetApp.Dvos.Singer;
using dotnetApp.dotnetApp.Helpers;
using dotnetApp.dotnetApp.Models;
using dotnetApp.dotnetApp.Services;
using dotnetApp.dotnetApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnetApp.dotnetApp.Controllers
{
  [Route("api/[controller]")]
  [Consumes("application/json")]
  [ApiController]
  public class SingerController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly ILogger<SingerController> _logger;
    private readonly SingerService _singerService;
    private readonly FileService _fileService;
    private readonly ImageService _imageService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private string api;
    public SingerController(
      IMapper mapper,
      ILogger<SingerController> logger,
      SingerService singerService,
      FileService fileService,
      ImageService imageService,
      IHttpContextAccessor httpContextAccessor
    )
    {
      _mapper = mapper;
      _logger = logger;
      _singerService = singerService;
      _fileService = fileService;
      _imageService = imageService;
      _httpContextAccessor = httpContextAccessor;
      api = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/api/image";
    }

    // api/singer
    /// <summary>
    /// 查詢所有歌手
    /// </summary>
    /// <returns>所有歌手</returns>
    /// <response code="200">所有歌手資訊</response>
    [HttpGet]
    public IActionResult GetSinger()
    {
      List<SingerGroup> data = _singerService.GetSinger();
      List<SingerGroups> singer = data
      .GroupBy(x => x.id)
      .Select(x => _mapper.Map<SingerGroups>(x))
      .ToList();
      return Ok(new { singer });
    }

    // Get api/singer/{id}
    /// <summary>
    /// 查詢特定歌手
    /// </summary>
    /// <param name="id">歌手編號</param>
    /// <returns>歌手資訊</returns>
    /// <response code="200">歌手資訊</response>
    /// <response code="404">找不到該歌手</response>
    [HttpGet("{id}")]
    public IActionResult GetAssignSinger(Guid id)
    {
      Singer data = _singerService.GetAssignSinger(id);
      if (data == null) return NotFound(new { message = "找不到該歌手" });
      SingerRead singer = _mapper.Map<SingerRead>(data);
      return Ok(new { singer });
    }

    // Post api/singer
    /// <summary>
    /// 新增歌手
    /// </summary>
    /// <returns>新增歌手</returns>
    /// <response code="200">新增歌手成功</response>
    /// <response code="400">新增歌手失敗</response>
    [Consumes("multipart/form-data")]
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PostSinger([FromForm] SingerCreate singerCreate)
    {
      string _method = "新增歌手";
      try
      {
        Singer singer = _mapper.Map<Singer>(singerCreate);
        Image image = _fileService.UploadImage("singer", singerCreate.avatar);
        await _imageService.PostImage(image);
        singer.avatar = Path.Combine(api, image.id.ToString());
        await _singerService.PostSinger(singer);
        return Ok(new { message = $"{_method}成功" });
      }
      catch (Exception e)
      {
        _logger.LogError(LogEvent.error, $"執行{_method} 出現輸入的內容有誤");

        throw new AppException(e.ToString());
        // throw new AppException($"{_method}失敗");
      }
    }


    // Put api/singer/{id}
    /// <summary>
    /// 修改歌手
    /// </summary>
    public async Task<IActionResult> PutSinger(string id, [FromBody] SingerUpdate singerUpdate)
    {
      string _method = "修改歌手";
      try
      {
        Singer singer = _singerService.GetAssignSinger(Guid.Parse(id));
        if (singer == null) return NotFound(new { message = "找不到歌手" });
        _mapper.Map(singerUpdate, singer);
        await _singerService.UpdateSinger();
        return Ok(new { message = $"{_method}成功" });
      }
      catch (System.Exception)
      {
        _logger.LogError(LogEvent.error, $"執行{_method} 出現例外錯誤");
        throw new AppException($"{_method}失敗");
      }
    }

    // Delete api/singer/{id}
    /// <summary>
    /// 刪除歌手資訊
    /// </summary>
    /// <param name="id">歌手編號</param>
    /// <response code="200">刪除歌手成功</response>
    /// <response code="400">刪除歌手失敗</response>
    /// <response code="404">找不到該歌手</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSinger(string id)
    {
      string _method = "刪除歌手";
      try
      {
        Singer singer = _singerService.GetAssignSinger(Guid.Parse(id));
        if (singer == null) return NotFound(new { message = "找不到歌手" });
        await _singerService.DeleteSinger(singer);
        return Ok(new { message = $"{_method}成功" });
      }
      catch (Exception)
      {
        _logger.LogError(LogEvent.error, $"執行{_method} 出現例外錯誤");
        throw new AppException($"{_method}失敗");
      }
    }
  }
}
