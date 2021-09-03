using System.Threading.Tasks;
using dotnetApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnetApp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class SoundController : ControllerBase
  {
    private readonly SoundService _soundService;
    private readonly MemberService _memberService;
    public SoundController(
      SoundService soundService,
      MemberService memberService
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
