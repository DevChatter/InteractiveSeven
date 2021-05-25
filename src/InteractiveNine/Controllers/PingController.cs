using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InteractiveNine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : ControllerBase
    {
        private readonly ILogger<PingController> _logger;

        public PingController(ILogger<PingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("Ping Controller Called");
            return "Pong";
        }
    }
}
