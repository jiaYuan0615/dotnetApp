using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnetApp.Context;
using dotnetApp.Dtos.Member;
using dotnetApp.Dvos.Member;
using dotnetApp.Filters;
using dotnetApp.Helpers;
using dotnetApp.Models;
using dotnetApp.Services;
using dotnetApp.Utils;
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

    private readonly string _program = "使用者";

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
      _logger.LogInformation("Method : GetMember");
      return Ok(new { member });
    }

    // GET api/member/collection
    /// <summary>
    /// 查詢所有使用者的收藏項目
    /// </summary>
    /// <returns>所有使用者的收藏項目</returns>
    /// <response code="200">所有使用者的收藏項目</response>
    [TypeFilter(typeof(CustomAuthorization))]
    [HttpGet("collection")]
    public IActionResult GetMemberCollection()
    {
      List<MemberCollection> data = _memberService.GetMemberCollection();

      MemberCollection item = data.FirstOrDefault();
      IEnumerable<string> objKey = CommonHelpers.objectKeys(item);

      IEnumerable<MemberCollections> member = data
      .GroupBy(x => x.id)
      // using automapper
      .Select(x => _mapper.Map<MemberCollections>(x));
      return Ok(new { member });
    }

    // GET api/member/self
    /// <summary>
    /// 查詢個人的收藏項目
    /// </summary>
    /// <returns>個人的收藏項目</returns>
    /// <response code="200">個人的收藏項目</response>
    [TypeFilter(typeof(CustomAuthorization))]
    [HttpGet("self")]
    public IActionResult GetPersonalCollection()
    {
      string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      List<MemberCollection> data = _memberService.GetMemberCollection(memberId);
      var member = data
      .GroupBy(x => x.id)
      .Select(x => _mapper.Map<MemberCollections>(x))
      .FirstOrDefault();
      return Ok(new { member });
    }

    // GET api/member/{id}
    /// <summary>
    /// 查詢特定使用者
    /// </summary>
    /// <param name="id">使用者編號</param>
    /// <returns>使用者資訊</returns>
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
    /// <summary>
    /// 註冊使用者
    /// </summary>
    /// <response code="200">註冊帳號成功</response>
    /// <response code="400">輸入的內容有誤</response>
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
    /// <summary>
    /// 使用者登入
    /// </summary>
    /// <response code="200">登入成功</response>
    /// <response code="400">輸入的密碼有誤</response>
    /// <response code="404">找不到該使用者</response>
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] MemberLogin memberLogin)
    {
      Member member = _memberService.GetAssignMemberByEmail(memberLogin.email);
      if (member == null)
      {
        _logger.LogError(LogEvent.NotFound, $"尚未註冊的電子郵件{memberLogin.email}嘗試登入");
        return NotFound(new { message = "找不到該使用者" });
      }
      _logger.LogInformation(LogEvent.process, $"用戶：{member.id}，執行{this._program}登入");
      bool verified = _passwordService.CheckPassword(memberLogin.password, member.password);
      if (!verified)
      {
        _logger.LogError(LogEvent.error, $"用戶：{member.id}，輸入密碼錯誤");
        throw new AppException("輸入的密碼有誤");
      }
      string token = _jwt.yieldToken(member.id.ToString());
      _logger.LogInformation(LogEvent.success, $"用戶：{member.id}，登入系統");
      return Ok(new { message = "登入成功", token });
    }

    //PUT apii/member
    /// <summary>
    /// 修改使用者資訊
    /// </summary>
    /// <response code="200">更新使用者成功</response>
    /// <response code="400">更新使用者失敗</response>
    /// <response code="404">找不到該使用者</response>
    [HttpPut]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateMember([FromForm] MemberUpdate memberUpdate)
    {
      string id = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      try
      {
        _logger.LogInformation(LogEvent.process, $"用戶：{id}，執行{this._program}更新");
        Member data = _memberService.GetAssignMemberById(Guid.Parse(id));
        if (data == null)
        {
          _logger.LogError(LogEvent.NotFound, $"用戶：{id}，找不到該使用者錯誤");
          throw new NotFoundException("找不到該使用者");
        }
        // 更新資料的兩種方法
        // 需要把要更新的資料補滿
        // _mapper.Map(memberUpdate, data);
        // await _memberService.UpdateMember();
        await _memberService.UpdateMember(data, memberUpdate);
        _logger.LogInformation(LogEvent.update, $"用戶：{id}，更新個人資訊成功");
        return Ok(new { message = "更新個人資訊成功" });
      }
      catch (Exception)
      {
        _logger.LogError(LogEvent.BadRequest, $"用戶:{id}，更新個人資訊失敗");
        throw new AppException("更新個人資訊失敗");
      }
    }
  }
}
