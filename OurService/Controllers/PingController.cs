using Microsoft.AspNetCore.Mvc;

namespace OurService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Ping()
        {
            return StatusCode(500);
        }
    }
}
