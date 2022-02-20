using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnetApp.Dtos;
using dotnetApp.Dtos.Collection;
using dotnetApp.Dtos.Member;
using dotnetApp.Dtos.Sound;
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


      // IEnumerable<MemberCollections> members = data
      // .GroupBy(x => x.id)
      // .Select(x =>
      // {
      //   string id = x.FirstOrDefault().id.ToString();
      //   string email = x.FirstOrDefault().email;
      //   string name = x.FirstOrDefault().name;
      //   string gender = x.FirstOrDefault().gender;
      //   List<CollectionRead> collections = x.Select(o =>
      //   {
      //     return new CollectionRead
      //     {
      //       id = o.collectionId.ToString(),
      //       name = o.collectionName
      //     };
      //   }).ToList();
      //   return new MemberCollections
      //   {
      //     id = id,
      //     email = email,
      //     name = name,
      //     gender = gender,
      //     collections = collections,
      //   };
      // });
      return Ok(new { member });
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
      string _method = "查詢個人收藏資料夾";
      try
      {
        string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
        List<CollectionItem> items = _collectionService.GetCollection(Guid.Parse(memberId));
        List<CollectionItems> collection = items
                                            .GroupBy(x => x.id)
                                            .Select(x => _mapper.Map<CollectionItems>(x))
                                            .ToList();
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

    // Put api/collection/{id}
    /// <summary>
    /// 更新收藏資料夾
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCollection(string id, [FromBody] CollectionUpdate collectionUpdate)
    {

      string _method = "更新收藏資料夾";
      try
      {
        Guid collectionId = Guid.Parse(id);
        Collection collection = _collectionService.GetAssignCollectionById(collectionId);
        _mapper.Map(collectionUpdate, collection);
        await _collectionService.UpdateCollection();
        return Ok(new { message = $"{_method}成功" });
      }
      catch (System.Exception)
      {
        _logger.LogError(LogEvent.error, $"執行{_method}發生錯誤");
        throw new AppException("執行發生例外錯誤");
      }

    }

    // Post api/collection/item/{id}
    /// <summary>
    /// 新增歌曲至指定資料夾
    /// </summary>
    [HttpPost("item/{id}")]
    public async Task<IActionResult> PostItemToCollection(string id, [FromBody] SoundItem soundItem)
    {
      string _method = "新增歌曲至資料夾";
      string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      try
      {
        Guid collectionId = Guid.Parse(id);
        Collection collection = _collectionService.GetAssignCollectionById(collectionId);
        if (collection == null) return NotFound(new { message = "找不到收藏" });
        Collection_Sound cs = new Collection_Sound()
        {
          collectionId = collectionId,
          soundId = soundItem.id
        };
        await _collectionService.PostItemToCollection(cs);
        return Ok(new { message = $"{_method}成功" });
      }
      catch (System.Exception)
      {
        _logger.LogError(LogEvent.error, $"執行{_method}發生錯誤");
        throw new AppException("執行發生例外錯誤");
      }
    }


    // Put api/collection/item/{id}
    /// <summary>
    /// 更新收藏資料夾內的項目
    /// </summary>
    [HttpPut("item/{id}")]
    public async Task<IActionResult> UpdateCollectionItem(string id, [FromBody] SoundItem[] soundItems)
    {
      string _method = "更新收藏資料夾內的項目";
      string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      try
      {
        List<CollectionSound> cs = _collectionService.GetCollectionSound(id, memberId);
        if (cs.Count() < 1) return NotFound(new { message = "找不到收藏" });
        List<string> origin = cs.Select(x => x.soundId.ToString()).ToList();
        List<string> update = soundItems.Select(x => x.id.ToString()).ToList();
        UpdateItem updateItem = CommonHelpers.updateItem(origin, update);
        if (updateItem.shoudUpdate)
        {
          if (updateItem.insertItem.Count() > 0)
          {
            List<Collection_Sound> insertItem = updateItem.insertItem.Select(x => new Collection_Sound
            {
              collectionId = Guid.Parse(id),
              soundId = Guid.Parse(x)
            }).ToList();
            await _collectionService.PostMultiItemToCollection(insertItem);
          }
          if (updateItem.deleteItem.Count() > 0)
          {
            List<Collection_Sound> deleteItem = updateItem.deleteItem.Select(x => new Collection_Sound
            {
              collectionId = Guid.Parse(id),
              soundId = Guid.Parse(x)
            }).ToList();
            await _collectionService.DeleteMultiItemToCollection(deleteItem);
          }
        }
        return Ok(new { message = $"{_method}成功" });
      }
      catch (System.Exception)
      {
        _logger.LogError(LogEvent.error, $"執行{_method}發生錯誤");
        throw new AppException("執行發生例外錯誤");
      }
    }

    // Delete api/collection/{id}
    /// <summary>
    /// 刪除收藏資料夾
    /// </summary>
    [HttpDelete("id")]
    public async Task<IActionResult> DeleteCollection(string id)
    {
      string _method = "刪除收藏";
      try
      {
        Guid targetId = Guid.Parse(id);
        Collection collection = _collectionService.GetAssignCollectionById(targetId);
        if (collection == null) return NotFound(new { message = "找不到收藏" });
        await _collectionService.DeleteCollection(targetId);
        return Ok(new { message = $"{_method}成功" });
      }
      catch (System.Exception)
      {
        _logger.LogError(LogEvent.error, $"執行{_method} 出現例外錯誤");
        throw new AppException($"{_method}失敗");
      }
    }
  }
}
