using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using dotnetApp.dotnetApp.Dtos.Sound;
using dotnetApp.dotnetApp.Helpers;
using dotnetApp.dotnetApp.Models;
using dotnetApp.dotnetApp.Services;
using dotnetApp.dotnetApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnetApp.dotnetApp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Consumes("application/json")]
  public class SoundController : ControllerBase
  {
    private readonly SoundService _soundService;
    private readonly ILogger<SoundController> _logger;
    private readonly MemberService _memberService;
    private readonly IMapper _mapper;

    public SoundController(
      SoundService soundService,
      ILogger<SoundController> logger,
      IMapper mapper,
      MemberService memberService
    )
    {
      _soundService = soundService;
      _logger = logger;
      _memberService = memberService;
      _mapper = mapper;

    }

    // GET api/sound
    /// <summary>
    /// 查詢歌曲
    /// </summary>
    /// <returns>歌曲清單</returns>
    /// <response code="200">歌曲清單</response>
    [HttpGet]
    public IActionResult GetSound()
    {
      // 自訂Query名稱 [FromQuery(Name ="query[]")] string[] query, [FromQuery(Name ="item[]")] string[] item
      // e.g. /api/sound?query[]=123&query[]=456&item[]=135&item[]=246
      // 不定義名稱則會預設使用參數名稱 [FromQuery] string[] query, [FromQuery] string[] item,
      // e.g. /api/sound?query=123&query=456&item=135&item=246
      string _method = "查詢所有歌曲";
      try
      {
        List<Sound> sounds = _soundService.GetSound();
        return Ok(new { sounds });
      }
      catch (Exception)
      {
        _logger.LogError(LogEvent.error, $"執行{_method} 發生例外錯誤");
        throw new AppException("執行發生例外錯誤");
      }
    }

    // GET api/sound/{id}
    /// <summary>
    /// 查詢特定歌曲
    /// </summary>
    /// <param name="id">歌曲編號</param>
    /// <returns>使用者資訊</returns>
    /// <response code="200">歌曲資訊</response>
    /// <response code="404">找不到該歌曲</response>
    [HttpGet("{id}")]
    public IActionResult GetAssignSound(string id)
    {
      Sound sound = _soundService.GetAssignSound(Guid.Parse(id));
      if (sound == null) return NotFound(new { message = "找不到該歌曲" });
      return Ok(new { sound });
    }

    // POST api/sound
    /// <summary>
    /// 新增歌曲
    /// </summary>
    /// <response code="200">新增歌曲成功</response>
    /// <response code="400">輸入的內容有誤</response>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PostSound([FromBody] SoundCreate soundCreate)
    {
      string _method = "新增歌曲";
      string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      _logger.LogInformation(LogEvent.process, $"執行{_method}");
      try
      {
        Sound sound = _mapper.Map<Sound>(soundCreate);
        string soundId = await _soundService.PostSound(sound);
        _logger.LogInformation(LogEvent.success, $"執行{_method}成功，歌曲編號[{soundId}]");
        return Ok(new { message = "新增歌曲成功" });
      }
      catch (Exception)
      {
        _logger.LogError(LogEvent.error, $"執行{_method}出現錯誤");
        throw new AppException("輸入的內容有誤");
      }
    }

    //PUT api/sound/{id}
    /// <summary>
    /// 修改歌曲資訊
    /// </summary>
    /// <param name="id">歌曲編號</param>
    /// <param name="soundUpdate"></param>
    /// <response code="200">更新歌曲成功</response>
    /// <response code="400">更新歌曲失敗</response>
    /// <response code="404">找不到該歌曲</response>
    [HttpPut("id")]
    public async Task<IActionResult> UpdateSound(string id, [FromBody] SoundUpdate soundUpdate)
    {
      string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      try
      {
        Sound sound = _soundService.GetAssignSound(Guid.Parse(id));
        if (sound == null) throw new NotFoundException("找不到該歌曲");
        await _soundService.UpdateSound(sound, soundUpdate);
        return Ok(new { message = "更新歌曲成功" });
      }
      catch (Exception)
      {
        throw new AppException("更新歌曲失敗");
      }
    }

    // Delete api/sound/{id}
    /// <summary>
    /// 刪除歌曲資訊
    /// </summary>
    /// <param name="id">歌曲編號</param>
    /// <response code="200">刪除歌曲成功</response>
    /// <response code="400">刪除歌曲失敗</response>
    /// <response code="404">找不到該歌曲</response>
    [HttpDelete("id")]
    public async Task<IActionResult> DeleteSound(string id)
    {
      try
      {
        Sound sound = _soundService.GetAssignSound(Guid.Parse(id));
        if (sound == null) throw new NotFoundException("找不到該歌曲");
        await _soundService.DeleteSound(sound);
        return Ok(new { message = "刪除歌曲成功" });
      }
      catch (Exception)
      {
        throw new AppException("刪除歌曲失敗");
      }
    }
  }
}
