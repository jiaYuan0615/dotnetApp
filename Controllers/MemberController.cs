using System.Threading.Tasks;
using dotnetApp.Context;
using dotnetApp.Repositories.User;
using dotnetApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnetApp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class MemberController : ControllerBase
  {
    private readonly MemberService _memberService;

    public MemberController(DatabaseContext databaseContext)
    {
      _memberService = new MemberService(databaseContext);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterMember([FromBody] RegisterRepository registerRepository)
    {
      bool isCreate;
      isCreate = await _memberService.RegisterMember(registerRepository);
      if (!isCreate)
      {
        return BadRequest(new { message = "註冊帳號失敗" });
      }
      return Ok(new { message = "註冊帳號成功" });
    }
  }
}
