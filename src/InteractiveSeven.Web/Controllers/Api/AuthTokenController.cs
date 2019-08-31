using InteractiveSeven.Core.Events;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveSeven.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthTokenController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(AccessTokenReceived accessTokenReceived)
        {
            DomainEvents.Raise(accessTokenReceived);

            return Ok();
        }
    }
}
