using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using dotnetApp.Context;
using dotnetApp.Dtos.Group;
using dotnetApp.Helpers;
using dotnetApp.Models;
using dotnetApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnetApp.Controllers
{
  [Route("api/[controller]")]
  [Consumes("application/json")]
  [ApiController]
  public class GroupController : ControllerBase
  {
    private readonly DatabaseContext _databaseContext;
    private readonly IMapper _mapper;
    private readonly IGroupService _groupService;

    public GroupController(
      DatabaseContext databaseContext,
      IMapper mapper,
      IGroupService groupService
    )
    {
      _databaseContext = databaseContext;
      _mapper = mapper;
      _groupService = groupService;
    }


    [HttpGet]
    public IActionResult GetGroup()
    {
      var data = _groupService.GetGroup();
      var group = _mapper.Map<IEnumerable<GroupRead>>(data);
      return Ok(new { group });
    }

    [HttpPost]
    public async Task<IActionResult> PostGroup([FromBody] GroupCreate groupCreate)
    {
      try
      {
        var group = _mapper.Map<Group>(groupCreate);
        await _groupService.PostGroup(group);
        return Ok(new { message = "新增團體成功" });
      }
      catch (Exception)
      {
        throw new AppException("輸入的內容有誤");
      }
    }
  }
}
