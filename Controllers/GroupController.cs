using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnetApp.Context;
using dotnetApp.Dtos.Group;
using dotnetApp.Helpers;
using dotnetApp.Models;
using dotnetApp.Services;
using dotnetApp.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnetApp.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [Consumes("application/json")]
  [ApiController]
  public class GroupController : ControllerBase
  {
    private readonly DatabaseContext _databaseContext;
    private readonly IMapper _mapper;
    private readonly ILogger<GroupController> _logger;
    private readonly FileService _fileService;
    private readonly ImageService _imageService;
    private readonly GroupService _groupService;
    private readonly string _program = "團體";
    private string api;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GroupController(
      DatabaseContext databaseContext,
      IMapper mapper,
      ILogger<GroupController> logger,
      GroupService groupService,
      ImageService imageService,
      FileService fileService,
      IHttpContextAccessor httpContextAccessor
    )
    {
      _databaseContext = databaseContext;
      _mapper = mapper;
      _logger = logger;
      _groupService = groupService;
      _fileService = fileService;
      _imageService = imageService;
      _httpContextAccessor = httpContextAccessor;
      api = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/api/image";
    }

    // GET api/group
    /// <summary>
    /// 查詢所有團體
    /// </summary>
    /// <returns>所有團體</returns>
    /// <response code="200">所有團體資訊</response>
    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetGroup()
    {
      List<Group> data = _groupService.GetGroup();
      List<GroupRead> group = _mapper.Map<List<GroupRead>>(data);
      return Ok(new { group });
    }

    // Get api/group/{id}
    /// <summary>
    /// 查詢特定團體
    /// </summary>
    /// <param name="id">團體編號</param>
    /// <returns>團體資訊</returns>
    /// <response code="200">團體資訊</response>
    /// <response code="404">找不到該團體</response>
    [HttpGet("{id}")]
    public IActionResult GetAssignGroup(string id)
    {
      Group group = _groupService.GetAssignGroup(Guid.Parse(id));
      if (group == null) throw new NotFoundException("找不到該團體");
      return Ok(new { group });
    }

    // Post api/group
    /// <summary>
    /// 新增團體
    /// </summary>
    /// <returns>新增團體</returns>
    /// <response code="200">新增團體成功</response>
    /// <response code="400">新增團體失敗</response>
    [Consumes("multipart/form-data")]
    [HttpPost]
    public async Task<IActionResult> PostGroup([FromForm] GroupCreate groupCreate)
    {
      string _method = "新增團體";
      string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      _logger.LogInformation(LogEvent.process, $"用戶：{memberId}，執行{this._program}新增");
      try
      {
        Image image = await _fileService.UploadImage("group", groupCreate.avatar);
        await _imageService.PostImage(image);
        Group group = _mapper.Map<Group>(groupCreate);
        group.avatar = Path.Combine(api, image.id.ToString());
        await _groupService.PostGroup(group);
        return Ok(new { message = $"{_method}成功" });
      }
      catch (Exception)
      {
        _logger.LogError(LogEvent.error, $"執行{_method} 出現輸入的內容有誤");
        throw new AppException($"{_method}失敗");
      }
    }

    // Put api/group/{id}
    /// <summary>
    /// 更新特定團體
    /// </summary>
    /// <param name="id">團體編號</param>
    /// <param name="groupUpdate"></param>
    /// <returns>更新資訊</returns>
    /// <response code="200">更新團體成功</response>
    /// <response code="400">更新團體失敗</response>
    /// <response code="404">找不到該團體</response>
    [HttpPut("id")]
    public async Task<IActionResult> UpdateGroup(string id, [FromBody] GroupUpdate groupUpdate)
    {
      string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      _logger.LogInformation(LogEvent.process, $"用戶：{memberId}，執行{this._program}更新");
      try
      {
        Group data = _groupService.GetAssignGroup(Guid.Parse(id));
        if (data == null)
        {
          _logger.LogInformation(LogEvent.NotFound, $"用戶：{memberId}，出現找不到團體錯誤");
          throw new NotFoundException("找不到該團體");
        }
        await _groupService.UpdateGroup(data, groupUpdate);
        _logger.LogInformation(LogEvent.update, $"用戶：{memberId}，更新編號為{id}的團體成功");
        return Ok(new { message = "更新團體成功" });
      }
      catch (Exception)
      {
        _logger.LogError(LogEvent.BadRequest, $"用戶：{memberId}，更新團體失敗");
        throw new AppException("更新團體失敗");
      }
    }

    // Delete api/group/{id}
    /// <summary>
    /// 刪除團體
    /// </summary>
    /// <param name="id">團體編號</param>
    /// <response code="200">刪除團體成功</response>
    /// <response code="400">刪除團體失敗</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGroup(string id)
    {
      string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      try
      {
        // 關聯需要先刪除
        Group data = _groupService.GetAssignGroup(Guid.Parse(id));
        if (data == null)
        {
          _logger.LogInformation(LogEvent.NotFound, $"用戶：{memberId}，出現找不到團體錯誤");
          throw new NotFoundException("找不到該團體");
        }
        await _groupService.DeleteGroup(id);
        _logger.LogInformation(LogEvent.success, $"用戶：{memberId}，刪除團體{id}成功");
        return Ok(new { message = "刪除團體成功" });
      }
      catch (Exception)
      {
        _logger.LogError(LogEvent.error, $"用戶：{memberId}，刪除團體{id}失敗");
        throw new AppException("刪除團體失敗");
      }
    }
  }
}
