using System.Threading.Tasks;
using dotnetApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetApp.Controllers
{
  [ApiController]
  [Authorize]
  [Route("api/[controller]")]
  public class SoundController : ControllerBase
  {
    private readonly ISoundService _soundService;
    private readonly IMemberService _memberService;
    public SoundController(
      ISoundService soundService,
      IMemberService memberService
    )
    {
      _soundService = soundService;
      _memberService = memberService;
    }

    [HttpGet]
    public IActionResult getSound()
    {
      return Ok(new { message = "sounds" });
    }

    [HttpGet("{id}")]
    public IActionResult getAssignSound(string id)
    {
      return Ok(new { id });
    }

    [HttpPost]
    public IActionResult postSound()
    {
      string data = "Yo";
      return Ok(new { data });
    }
  }
}
