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

namespace dotnetApp.Controllers
{
  [Route("api/[controller]")]
  [Consumes("application/json")]
  [ApiController]
  public class SingerController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly ISingerService _singerService;

    public SingerController(
      IMapper mapper,
      ISingerService singerService
    )
    {

      _mapper = mapper;
      _singerService = singerService;
    }

    [HttpGet]
    public IActionResult GetSinger()
    {
      IEnumerable<Singer> data = _singerService.GetSinger();
      List<SingerRead> singer = _mapper.Map<List<SingerRead>>(data);
      return Ok(new { singer });
    }

    [HttpGet("{id}")]
    public IActionResult GetAssignSinger(Guid id)
    {
      var data = _singerService.GetAssignSinger(id);
      if (data == null) return NotFound(new { message = "找不到該歌手" });
      var singer = _mapper.Map<SingerRead>(data);
      return Ok(new { singer });
    }

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
        throw new AppException("輸入的內容有誤");
      }
    }
  }
}
