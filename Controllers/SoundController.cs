using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using dotnetApp.Dtos.Sound;
using dotnetApp.Helpers;
using dotnetApp.Models;
using dotnetApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetApp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  [Consumes("application/json")]
  public class SoundController : ControllerBase
  {
    private readonly ISoundService _soundService;
    private readonly IMemberService _memberService;
    private readonly IMapper _mapper;

    public SoundController(
      ISoundService soundService,
      IMapper mapper,
      IMemberService memberService
    )
    {
      _soundService = soundService;
      _memberService = memberService;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetSound()
    {
      var data = _soundService.GetSound();
      var sounds = _mapper.Map<IEnumerable<Sound>>(data);
      return Ok(new { sounds });
    }

    [HttpGet("{id}")]
    public IActionResult GetAssignSound(string id)
    {
      var sound = _soundService.GetAssignSound(Guid.Parse(id));
      if (sound == null) return NotFound(new { message = "找不到該歌曲" });
      return Ok(new { sound });
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PostSound([FromForm] SoundCreate soundCreate)
    {
      try
      {
        var sound = _mapper.Map<Sound>(soundCreate);
        await _soundService.PostSound(sound);
        return Ok(new { message = "新增歌曲成功" });
      }
      catch (Exception)
      {
        throw new AppException("輸入的內容有誤");
      }
    }

    [HttpPut("id")]
    public async Task<IActionResult> UpdateSound(string id, [FromBody] SoundUpdate soundUpdate)
    {
      try
      {
        var sound = _soundService.GetAssignSound(Guid.Parse(id));
        if (sound == null) throw new NotFoundException("找不到該歌曲");
        await _soundService.UpdateSound(sound, soundUpdate);
        return Ok(new { message = "更新歌曲成功" });
      }
      catch (Exception)
      {
        throw new AppException("更新歌曲失敗");
      }
    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteSound(string id)
    {
      try
      {
        var sound = _soundService.GetAssignSound(Guid.Parse(id));
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
