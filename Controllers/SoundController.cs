using System;
using System.Collections.Generic;
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
    public IActionResult getSound()
    {
      var data = _soundService.GetSound();
      var sound = _mapper.Map<IEnumerable<Sound>>(data);
      return Ok(new { sound });
    }

    [HttpGet("{id}")]
    public IActionResult getAssignSound(string id)
    {
      var sound = _soundService.GetAssignSound(Guid.Parse(id));
      if (sound == null) return NotFound(new { message = "找不到該歌曲" });
      return Ok(new { sound });
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> postSound([FromBody] SoundCreate soundCreate)
    {
      try
      {
        var sound = _mapper.Map<Sound>(soundCreate);
        await _soundService.CreateSound(sound);
        return Ok(new { message = "新增歌曲成功" });
      }
      catch (Exception)
      {
        throw new AppException("輸入的內容有誤");
      }
    }
  }
}
