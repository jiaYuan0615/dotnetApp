using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnetApp.Dtos.Collection;
using dotnetApp.Dtos.Member;
using dotnetApp.Dvos.Collection;
using dotnetApp.Dvos.Member;
using dotnetApp.Filters;
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
    private readonly MemberService _memberService;
    private readonly CollectionService _collectionService;

    public CollectionController(
      IMapper mapper,
      MemberService memberService,
      ILogger<CollectionController> logger,
      CollectionService collectionService
      )
    {
      _mapper = mapper;
      _logger = logger;
      _memberService = memberService;
      _collectionService = collectionService;
    }


    // GET api/private/collection
    /// <summary>
    /// 查詢使用者的收藏項目
    /// </summary>
    /// <returns>所有使用者的收藏項目</returns>
    /// <response code="200">所有使用者的收藏項目</response>
    [TypeFilter(typeof(CustomAuthorization))]
    [HttpGet("~/api/private/collection")]
    public IActionResult _GetMemberCollection()
    {
      List<MemberCollection> data = _memberService.GetMemberCollection();

      MemberCollection item = data.FirstOrDefault();
      IEnumerable<string> objKey = CommonHelpers.objectKeys(item);

      IEnumerable<MemberCollections> member = data
      .GroupBy(x => x.id)
      .Select(x => _mapper.Map<MemberCollections>(x));


      IEnumerable<MemberCollections> members = data
      .GroupBy(x => x.id)
      .Select(x =>
      {
        string id = x.FirstOrDefault().id.ToString();
        string email = x.FirstOrDefault().email;
        string name = x.FirstOrDefault().name;
        string gender = x.FirstOrDefault().gender;
        List<CollectionRead> collections = x.Select(o =>
        {
          return new CollectionRead
          {
            id = o.collectionId.ToString(),
            name = o.collectionName
          };
        }).ToList();
        return new MemberCollections
        {
          id = id,
          email = email,
          name = name,
          gender = gender,
          collections = collections,
        };
      });
      return Ok(new { member });
    }

    // GET api/collection/item
    /// <summary>
    /// 查詢個人的收藏項目
    /// </summary>
    /// <returns>個人的收藏項目</returns>
    /// <response code="200">個人的收藏項目</response>
    // [TypeFilter(typeof(CustomAuthorization))]
    // [HttpGet("item")]
    // public IActionResult GetPersonalCollection()
    // {
    //   string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
    //   List<MemberCollection> data = _memberService.GetMemberCollection(memberId);
    //   var member = data
    //   .GroupBy(x => x.id)
    //   .Select(x => _mapper.Map<MemberCollections>(x))
    //   .FirstOrDefault();
    //   return Ok(new { member });
    // }

    // GET api/collection
    /// <summary>
    /// 查詢個人的收藏項目
    /// </summary>
    /// <returns>個人的收藏項目</returns>
    /// <response code="200">個人的收藏項目</response>
    [HttpGet]
    public IActionResult GetCollection()
    {
      string _method = "查詢個人收藏項目";
      try
      {
        string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
        List<Collection> items = _collectionService.GetCollection(Guid.Parse(memberId));
        IEnumerable<CollectionRead> collection = _mapper.Map<IEnumerable<CollectionRead>>(items);
        return Ok(new { collection });
      }
      catch (Exception)
      {
        _logger.LogError(LogEvent.error, $"執行{_method}發生錯誤");
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
    public IActionResult GetCollectionItems(string id)
    {
      string _method = "查詢個人特定收藏資料夾";
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
        _logger.LogError(LogEvent.error, $"執行{_method}發生錯誤");
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
      string _method = "新增收藏資料夾";
      string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      _logger.LogInformation(LogEvent.process, $"用戶[{memberId}]，執行{_method}");
      try
      {
        Collection collection = _mapper.Map<Collection>(collectionCreate);
        collection.memberId = Guid.Parse(memberId);
        string collectionId = await _collectionService.PostCollection(collection);
        _logger.LogInformation(LogEvent.success, $"用戶[{memberId}]，{_method}成功，收藏編號:{collectionId}");
        return Ok(new { message = $"{_method}成功" });
      }
      catch (Exception)
      {
        _logger.LogError(LogEvent.error, $"用戶[{memberId}]，{_method}失敗");
        throw new AppException($"{_method}失敗");
      }
    }
  }
}
