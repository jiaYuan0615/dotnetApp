using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using dotnetApp.Dtos.Singer;
using dotnetApp.Helpers;
using dotnetApp.Models;
using dotnetApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnetApp.Controllers
{
  [Route("api/[controller]")]
  [Consumes("application/json")]
  [ApiController]
  public class SingerController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly ILogger<SingerController> _logger;
    private readonly ISingerService _singerService;

    public SingerController(
      IMapper mapper,
      ILogger<SingerController> logger,
      ISingerService singerService
    )
    {
      _mapper = mapper;
      _logger = logger;
      _singerService = singerService;
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
      IEnumerable<Singer> data = _singerService.GetSinger();
      List<SingerRead> singer = _mapper.Map<List<SingerRead>>(data);
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
      var data = _singerService.GetAssignSinger(id);
      if (data == null) return NotFound(new { message = "找不到該歌手" });
      var singer = _mapper.Map<SingerRead>(data);
      return Ok(new { singer });
    }


    // Post api/group
    /// <summary>
    /// 新增歌手
    /// </summary>
    /// <returns>新增歌手</returns>
    /// <response code="200">新增歌手成功</response>
    /// <response code="400">新增歌手失敗</response>
    [HttpPost]
    public async Task<IActionResult> PostSinger([FromBody] SingerCreate singerCreate)
    {
      try
      {
        var singer = _mapper.Map<Singer>(singerCreate);
        await _singerService.PostSinger(singer);
        return Ok(new { message = "新增歌手成功" });
      }
      catch (Exception)
      {
        throw new AppException("新增歌手失敗");
      }
    }
  }
}
