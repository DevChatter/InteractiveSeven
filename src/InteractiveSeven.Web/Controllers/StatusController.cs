using Microsoft.AspNetCore.Mvc;

namespace InteractiveSeven.Web.Controllers
{
    [Route("[controller]")]
    public class StatusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}