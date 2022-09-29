using Microsoft.AspNetCore.Mvc;

namespace Overlays.InteractiveSeven.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public object Get()
        {
            return new
            {
                IsRunning = true,
                IsGameRunning = false,
                IsGameControlled = false,
                IsChatConnected = false,
            };
        }
    }
}
