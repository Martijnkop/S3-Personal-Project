using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace barboek.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        private readonly ILogger<HelloWorldController> _logger;

        public HelloWorldController(ILogger<HelloWorldController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            Guid userId = Guid.Parse(User.FindFirst("userId").Value);
            var response = new { message = $"Hello {userId}!" };
            return Ok(response);
        }
    }
}