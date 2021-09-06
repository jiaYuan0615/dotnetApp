using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnetApp.Context;
using dotnetApp.Dtos;
using dotnetApp.Dtos.Member;
using dotnetApp.Helpers;
using dotnetApp.Models;
using dotnetApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetApp.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [Produces("application/json")]
  [ApiController]
  public class MemberController : ControllerBase
  {
    private readonly IMemberService _memberService;
    private readonly JwtHelpers _jwt;
    private readonly IMapper _mapper;
    private readonly IMailService _mailService;
    private readonly IPasswordService _passwordService;

    public MemberController(
      DatabaseContext databaseContext,
      IMapper mapper,
      JwtHelpers jwt,
      IMemberService memberService,
      IMailService mailService,
      IPasswordService passwordService
      )
    {
      _memberService = memberService;
      _jwt = jwt;
      _mapper = mapper;
      _mailService = mailService;
      _passwordService = passwordService;
    }

    // GET api/member
    /// <summary>
    /// 查詢所有使用者
    /// </summary>
    /// <returns>所有使用者</returns>
    /// <response code="200">所有使用者資訊</response>
    [HttpGet]
    // Filter
    public IActionResult GetMockMember()
    {
      // string[] data = new[] { "1", "2" };
      // List<string> datas = new List<string> { "2", "4" };
      string userId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      // var members = _memberService.GetAssignMember(Guid.Parse(userId));
      // var data = _memberService.GetMember();
      // var useDto = _mapper.Map<IEnumerable<MemberRead>>(data);
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
      throw new AppException("輸入的內容有誤");
      // return Ok(new { member, userId });
    }

    // GET api/member/{id}
    /// <summary>
    /// 查詢特定使用者
    /// </summary>
    /// <param name="id">查詢使用者編號</param>
    /// <returns>特定使用者</returns>
    /// <response code="200">使用者資訊</response>
    /// <response code="404">找不到該使用者</response>
    [AllowAnonymous]
    [HttpGet("{id}")]
    public IActionResult GetAssignMember(Guid id)
    {
      string token = _jwt.yieldToken(id.ToString());
      var member = _memberService.GetAssignMemberById(id);
      if (member == null)
      {
        return NotFound(new { message = "找不到該使用者" });
      }
      return Ok(new { member, token });
    }


    // POST api/member
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> RegisterMember([FromBody] MemberRegister memberRegister)
    {
      var member = _mapper.Map<Member>(memberRegister);
      try
      {
        await _memberService.RegisterMember(member);
        return Ok(new { message = "註冊帳號成功" });
      }
      catch (Exception)
      {
        throw new AppException("輸入的內容有誤");
      }
    }

    // POST api/member/login
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] MemberLogin memberLogin)
    {
      var member = _memberService.GetAssignMemberByEmail(memberLogin.email);
      if (member == null) return NotFound(new { message = "找不到該使用者" });
      bool verified = _passwordService.CheckPassword(member.password, memberLogin.password);
      if (!verified) throw new AppException("輸入的密碼錯誤");
      string token = _jwt.yieldToken(member.id.ToString());
      return Ok(new { token });
    }
  }
}
