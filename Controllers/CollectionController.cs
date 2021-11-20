using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnetApp.Dtos.Collection;
using dotnetApp.Helpers;
using dotnetApp.Models;
using dotnetApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  [Consumes("application/json")]
  public class CollectionController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly ICollectionService _collectionService;

    public CollectionController(
      IMapper mapper,
      ICollectionService collectionService
      )
    {
      _mapper = mapper;
      _collectionService = collectionService;
    }

    [HttpGet]
    public IActionResult GetCollection()
    {
      string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      var data = _collectionService.GetCollection(Guid.Parse(memberId));
      var collection = _mapper.Map<IEnumerable<CollectionRead>>(data);
      return Ok(new { data });
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostCollection([FromBody] CollectionCreate collectionCreate)
    {
      try
      {
        string memberId = User.Claims.FirstOrDefault(x => x.Type == "id").Value;
        Collection collection = _mapper.Map<Collection>(collectionCreate);
        collection.memberId = Guid.Parse(memberId);
        await _collectionService.PostCollection(collection);
        return Ok(new { message = "新增收藏資料夾成功" });
      }
      catch (Exception)
      {
        throw new AppException("輸入的內容有誤");
      }
    }
  }
}
