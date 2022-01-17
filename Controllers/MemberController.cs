using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnetApp.Context;
using dotnetApp.Dtos.Member;
using dotnetApp.Filters;
using dotnetApp.Helpers;
using dotnetApp.Models;
using dotnetApp.Services;
using dotnetApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    private readonly MemberService _memberService;
    private readonly JwtHelpers _jwt;
    private readonly IMapper _mapper;
    private readonly MailService _mailService;
    private readonly ImageService _imageService;
    private readonly FileService _fileService;
    private readonly PasswordService _passwordService;
    private readonly ILogger<MemberController> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private string api;
    public MemberController(
    DatabaseContext databaseContext,
    IMapper mapper,
    JwtHelpers jwt,
    MemberService memberService,
    FileService fileService,
    ImageService imageService,
    MailService mailService,
    IHttpContextAccessor httpContextAccessor,
    PasswordService passwordService,
    ILogger<MemberController> logger
    )
    {
      _memberService = memberService;
      _jwt = jwt;
      _mapper = mapper;
      _mailService = mailService;
      _fileService = fileService;
      _imageService = imageService;
      _passwordService = passwordService;
      _logger = logger;
      _httpContextAccessor = httpContextAccessor;
      api = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/api/image";
    }

    // GET api/private/member
    /// <summary>
    /// 查詢所有使用者
    /// </summary>
    /// <returns>所有使用者</returns>
    /// <response code="200">所有使用者資訊</response>
    // 使用 ServiceFilter 需先在DI 容器註冊後，才可以從 DI 容器中取得實例
    // 使用 TypeFilter 可以自動取得 DI 容器的實例
    // [ServiceFilter(typeof(CustomAuthorization))]
    [TypeFilter(typeof(CustomAuthorization))]
    [HttpGet("~/api/private/member")]
    public IActionResult _GetMember()
    {
      string _method = "查看所有使用者";
      try
      {
        string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
        List<Member> items = _memberService.GetMember();
        List<MemberRead> member = _mapper.Map<List<MemberRead>>(items);
        return Ok(new { member });
      }
      catch (Exception)
      {
        _logger.LogError(LogEvent.error, $"執行{_method} 發生例外錯誤");
        throw new AppException("執行發生例外錯誤");
      }
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
    public IActionResult GetAssignMember(string id)
    {
      string _method = "取得個人資訊";
      try
      {
        Member data = _memberService.GetAssignMemberById(Guid.Parse(id));
        if (data == null) return NotFound(new { message = "找不到該使用者" });
        MemberRead member = _mapper.Map<MemberRead>(data);
        return Ok(new { member });
      }
      catch (Exception)
      {
        _logger.LogError(LogEvent.error, $"用戶:[{id.ToString()}]執行{_method}，發生錯誤");
        throw new AppException("執行發生例外錯誤");
      }
    }


    // POST api/member
    /// <summary>
    /// 註冊使用者
    /// </summary>
    /// <response code="200">註冊帳號成功</response>
    /// <response code="400">輸入的內容有誤</response>
    [AllowAnonymous]
    [Consumes("multipart/form-data")]
    [HttpPost]
    public async Task<IActionResult> RegisterMember([FromForm] MemberRegister memberRegister)
    {
      string _method = "使用者註冊";
      try
      {
        Image image = await _fileService.UploadImage("avatar", memberRegister.avatar);
        await _imageService.PostImage(image);
        Member member = _mapper.Map<Member>(memberRegister);
        member.avatar = Path.Combine(api, image.id.ToString());
        member.password = _passwordService.HashPassword(memberRegister.password);
        await _memberService.RegisterMember(member);
        return Ok(new { message = $"{_method}成功" });
      }
      catch (Exception)
      {
        _logger.LogError(LogEvent.error, $"執行{_method} 出現輸入的內容有誤");
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
      string _method = "使用者登入";
      Member member = _memberService.GetAssignMemberByEmail(memberLogin.email);
      try
      {
        if (member == null)
        {
          _logger.LogError(LogEvent.NotFound, $"尚未註冊的電子郵件[{memberLogin.email}]嘗試登入");
          return NotFound(new { message = "找不到該使用者" });
        }
        _logger.LogInformation(LogEvent.process, $"用戶:[{member.id}]，執行[Post api/member/login]");
        bool verified = _passwordService.CheckPassword(memberLogin.password, member.password);
        if (!verified)
        {
          _logger.LogError(LogEvent.error, $"用戶[{member.id}]，輸入密碼錯誤");
          throw new AppException("輸入的密碼有誤");
        }
        string token = _jwt.yieldToken(member.id.ToString());
        _logger.LogInformation(LogEvent.success, $"用戶[{member.id}]，執行{_method}成功");
        return Ok(new { message = "登入成功", token });
      }
      catch (System.Exception)
      {

        _logger.LogError(LogEvent.error, $"用戶[{member.id}]，執行{_method}失敗");
        throw new AppException($"執行{_method}失敗");
      }

    }

    //PUT api/member
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
      string _method = "修改個人資訊";
      string id = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      try
      {
        _logger.LogInformation(LogEvent.process, $"用戶[{id}]，執行{_method}");
        Member member = _memberService.GetAssignMemberById(Guid.Parse(id));
        if (member == null)
        {
          _logger.LogError(LogEvent.NotFound, $"用戶[{id}]，找不到該使用者錯誤");
          throw new NotFoundException("找不到該使用者");
        }
        string avatar = member.avatar;
        if (memberUpdate.avatar.Length > 0)
        {
          Guid replace = Guid.Parse(member.avatar.Replace($"{api}/", ""));
          Image previousImage = _imageService.GetAssignImageById(replace);
          System.IO.File.Delete(previousImage.path);
          Image image = await _fileService.UploadImage("avatar", memberUpdate.avatar);
          await _imageService.PostImage(image);
          // consider whether remove old image
          avatar = Path.Combine(api, image.id.ToString());
        }
        // 更新資料的兩種方法
        // 需要把要更新的資料補滿
        _mapper.Map(memberUpdate, member);
        member.avatar = avatar;
        await _memberService.UpdateMember();
        // await _memberService.UpdateMember(data, memberUpdate);
        _logger.LogInformation(LogEvent.update, $"用戶[{id}]，{_method}成功");
        return Ok(new { message = $"{_method}成功" });
      }
      catch (Exception)
      {
        _logger.LogError(LogEvent.BadRequest, $"用戶[{id}]，{_method}失敗");
        throw new AppException($"{_method}失敗");
      }
    }

    public async Task<IActionResult> UpdataPassword([FromBody] MemberUpdatePassword memberUpdatePassword)
    {
      string id = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      string _method = "變更使用者密碼";
      try
      {
        Member member = _memberService.GetAssignMemberById(Guid.Parse(id));
        _mapper.Map(memberUpdatePassword, member);
        await _memberService.UpdateMember();
        _logger.LogInformation(LogEvent.update, $"用戶[{id}]，{_method}成功");
        return Ok(new { message = $"{_method}成功" });
      }
      catch (System.Exception)
      {
        _logger.LogError(LogEvent.BadRequest, $"用戶[{id}]，{_method}失敗");
        throw new AppException($"{_method}失敗");
      }
      throw new NotImplementedException();
    }
  }
}
