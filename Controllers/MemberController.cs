using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnetApp.Context;
using dotnetApp.Dtos.Collection;
using dotnetApp.Dtos.Member;
using dotnetApp.Dvos.Member;
using dotnetApp.Filters;
using dotnetApp.Helpers;
using dotnetApp.Models;
using dotnetApp.Models.MemberJoin;
using dotnetApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// Controller
// 處理商業邏輯
// 整理資料結構
namespace dotnetApp.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [Consumes("application/json")]
  [ApiController]
  public class MemberController : ControllerBase
  {
    private readonly IMemberService _memberService;
    private readonly JwtHelpers _jwt;
    private readonly IMapper _mapper;
    private readonly IMailService _mailService;
    private readonly IPasswordService _passwordService;
    private readonly ILogger<MemberController> _logger;

    public MemberController(
      DatabaseContext databaseContext,
      IMapper mapper,
      JwtHelpers jwt,
      IMemberService memberService,
      IMailService mailService,
      IPasswordService passwordService,
      ILogger<MemberController> logger
      )
    {
      _memberService = memberService;
      _jwt = jwt;
      _mapper = mapper;
      _mailService = mailService;
      _passwordService = passwordService;
      _logger = logger;
    }

    // GET api/member
    /// <summary>
    /// 查詢所有使用者
    /// </summary>
    /// <returns>所有使用者</returns>
    /// <response code="200">所有使用者資訊</response>
    // 使用 ServiceFilter 需先在DI 容器註冊後，才可以從 DI 容器中取得實例
    // 使用 TypeFilter 可以自動取得 DI 容器的實例
    // [ServiceFilter(typeof(CustomAuthorization))]
    [TypeFilter(typeof(CustomAuthorization))]
    [HttpGet]
    public IActionResult GetMember()
    {
      string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      List<Member> data = _memberService.GetMember();
      var member = _mapper.Map<List<MemberRead>>(data);
      return Ok(new { member });
    }

    // GET api/member/collection
    [TypeFilter(typeof(CustomAuthorization))]
    [HttpGet("collection")]
    public IActionResult GetMemberCollection()
    {
      List<MemberCollection> data = _memberService.GetMemberCollection();

      // Same as js object.Keys method
      // Need pass object data to function that work normally
      MemberCollection item = data.FirstOrDefault();
      var objKey = CommonHelpers.objectKeys(item);

      var member = data
      .GroupBy(x => x.id)
      // using automapper
      .Select(x => _mapper.Map<MemberCollections>(x));
      return Ok(new { member });
    }

    // GET api/member/personal
    [TypeFilter(typeof(CustomAuthorization))]
    [HttpGet("personal")]
    public IActionResult GetPersonalCollection()
    {
      string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      List<MemberCollection> data = _memberService.GetMemberCollection(memberId);
      var member = data
      .GroupBy(x => x.id)
      // manual transfer
      .Select(x =>
      {
        MemberCollection member = x.FirstOrDefault();
        List<CollectionRead> item = x.Select(v => new CollectionRead { id = v.collectionId.ToString(), name = v.collectionName }).ToList();
        return new MemberCollections
        {
          id = member.id.ToString(),
          email = member.email,
          name = member.name,
          gender = member.gender,
          collections = item
        };
      }).FirstOrDefault();
      return Ok(new { member });
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
      var data = _memberService.GetAssignMemberById(id);
      if (data == null) return NotFound(new { message = "找不到該使用者" });
      var member = _mapper.Map<MemberRead>(data);
      return Ok(new { member });
    }


    // POST api/member
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> RegisterMember([FromBody] MemberRegister memberRegister)
    {
      try
      {
        var member = _mapper.Map<Member>(memberRegister);
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
      bool verified = _passwordService.CheckPassword(memberLogin.password, member.password);
      if (!verified) throw new AppException("輸入的密碼有誤");
      string token = _jwt.yieldToken(member.id.ToString());
      return Ok(new { message = "登入成功", token });
    }

    [HttpPut]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateMember([FromForm] MemberUpdate memberUpdate)
    {
      try
      {
        string id = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
        var data = _memberService.GetAssignMemberById(Guid.Parse(id));
        if (data == null) throw new NotFoundException("找不到該使用者");
        // 更新資料的兩種方法
        // 需要把要更新的資料補滿
        // _mapper.Map(memberUpdate, data);
        // await _memberService.UpdateMember();
        await _memberService.UpdateMember(data, memberUpdate);
        return Ok(new { message = "更新使用者成功" });
      }
      catch (Exception)
      {
        throw new AppException("更新使用者失敗");
      }
    }
  }
}
