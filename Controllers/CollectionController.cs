using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnetApp.Dtos.Collection;
using dotnetApp.Dvos.Collection;
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
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  [Consumes("application/json")]
  public class CollectionController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly ILogger<CollectionController> _logger;
    private readonly ICollectionService _collectionService;

    public CollectionController(
      IMapper mapper,
      ILogger<CollectionController> logger,
      ICollectionService collectionService
      )
    {
      _mapper = mapper;
      _logger = logger;
      _collectionService = collectionService;
    }

    // GET api/collection
    /// <summary>
    /// 查詢個人的收藏項目
    /// </summary>
    /// <returns>個人的收藏項目</returns>
    /// <response code="200">個人的收藏項目</response>
    [HttpGet]
    public IActionResult GetCollection()
    {
      try
      {
        string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
        List<Collection> data = _collectionService.GetCollection(Guid.Parse(memberId));
        IEnumerable<CollectionRead> collection = _mapper.Map<IEnumerable<CollectionRead>>(data);
        return Ok(new { data });
      }
      catch (Exception)
      {
        _logger.LogError(LogEvent.error, "執行[GET /api/collection] 發生例外錯誤");
        throw new AppException("執行發生例外錯誤");
      }
    }

    // GET api/collection/{id}
    /// <summary>
    /// 查詢使用者的收藏子項目
    /// </summary>
    /// <param name="id">收藏編號</param>
    /// <returns>收藏子項目</returns>
    /// <response code="200">收藏子項目資訊</response>
    [HttpGet("{id}")]
    public IActionResult GetCollectionSound(string id)
    {
      try
      {
        string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
        List<CollectionSound> data = _collectionService.GetCollectionSound(id, memberId);
        CollectionSounds collection = data
        .GroupBy(x => x.id)
        .Select(x => _mapper.Map<CollectionSounds>(x))
        .FirstOrDefault();
        return Ok(new { collection });
      }
      catch (Exception)
      {
        _logger.LogError(LogEvent.error, $"執行[GET /api/collection/{id}] 發生例外錯誤");
        throw new AppException("執行發生例外錯誤");
      }
    }


    // POST api/collection
    /// <summary>
    /// 新增收藏資料夾
    /// </summary>
    /// <response code="200">新增收藏資料夾成功</response>
    /// <response code="400">輸入的內容有誤</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostCollection([FromBody] CollectionCreate collectionCreate)
    {
      string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      _logger.LogInformation(LogEvent.process, $"用戶[{memberId}]，執行新增收藏資料夾");
      try
      {
        Collection collection = _mapper.Map<Collection>(collectionCreate);
        collection.memberId = Guid.Parse(memberId);
        string collectionId = await _collectionService.PostCollection(collection);
        _logger.LogInformation(LogEvent.success, $"用戶[{memberId}]，新增收藏資料夾成功，收藏編號[{collectionId}]");
        return Ok(new { message = "新增收藏資料夾成功" });
      }
      catch (Exception)
      {
        _logger.LogError(LogEvent.error, $"用戶[{memberId}]，新增收藏資料夾失敗");
        throw new AppException("新增收藏資料夾失敗");
      }
    }
  }
}
