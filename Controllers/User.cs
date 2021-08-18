using System.Threading.Tasks;
using dotnetApp.Context;
using dotnetApp.Repositories.User;
using dotnetApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnetApp.Controllers
{
  public class UserController : ControllerBase
  {
    private readonly UserService _userService;

    public UserController(DatabaseContext databaseContext)
    {
      _userService = new UserService(databaseContext);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRepository registerRepository)
    {
      bool isCreate;
      isCreate = await _userService.RegisterUser(registerRepository);
      if (!isCreate)
      {
        return BadRequest(new { message = "註冊帳號失敗" });
      }
      return Ok(new { message = "註冊帳號成功" });
    }
  }
}