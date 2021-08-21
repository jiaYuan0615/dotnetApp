using Microsoft.AspNetCore.Mvc;

namespace dotnetApp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class SoundController : ControllerBase
  {
    public SoundController()
    {

    }

    [HttpGet]
    public IActionResult getSound()
    {
      return Ok(new {message = "sounds"});
    }

    [HttpGet("{id}")]
    public IActionResult getAssignSound(string id)
    {
      return Ok(new {id});
    }

  }
}
