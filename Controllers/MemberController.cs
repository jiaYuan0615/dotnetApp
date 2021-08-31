using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnetApp.Context;
using dotnetApp.Helpers;
using dotnetApp.Models;
using dotnetApp.Repositories.User;
using dotnetApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetApp.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class MemberController : ControllerBase
  {
    private readonly MemberService _memberService;
    private readonly JwtHelpers _jwt;
    private readonly IMapper _mapper;

    public MemberController(DatabaseContext databaseContext, IMapper mapper, JwtHelpers jwt)
    {
      _memberService = new MemberService(databaseContext);
      _jwt = jwt;
      _mapper = mapper;
    }

    // GET api/member
    [HttpGet]
    // Filter
    public IActionResult GetMockMember()
    {
      // string[] data = new[] { "1", "2" };
      // List<string> datas = new List<string> { "2", "4" };
      // string userId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      string userId = User.Identity.Name;
      // var members = _memberService.GetAssignMember(Guid.Parse(userId));
      List<Member> members = new List<Member>
        {
          new Member{id =Guid.NewGuid(),email="chenyan@gmail.com",password="password",avatar="avatar",name="辰諺",gender="男",email_verified=DateTime.Now.AddHours(1) }
        };
      var member = _memberService.GetMember().Select(x => new
      {
        id = x.id,
        gender = x.gender == "男" ? "1" : "0",
      }).First();
      DateTime AfterFiveHour = DateTime.Now.AddHours(5);
      // var member = _memberService.GetMember();
      return Ok(new { member, userId });
    }

    // GET api/member/{id}
    [AllowAnonymous]
    [HttpGet("{id}")]
    public IActionResult GetAssignMember(Guid id)
    {
      string token = _jwt.yieldToken(id);
      var member = _memberService.GetAssignMember(id);
      if (member == null)
      {
        return NotFound(new { message = "找不到該使用者" });
      }
      return Ok(new { member, token });
    }

    [HttpPost]
    public async Task<IActionResult> RegisterMember([FromBody] RegisterRepository registerRepository)
    {
      try
      {
        await _memberService.RegisterMember(registerRepository);
        return Ok(new { message = "註冊帳號成功" });
      }
      catch (System.Exception)
      {
        return BadRequest(new { message = "註冊帳號失敗" });
      }
    }
  }
}
