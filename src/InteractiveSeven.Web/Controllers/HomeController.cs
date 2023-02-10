using Microsoft.AspNetCore.Mvc;

namespace InteractiveSeven.Web.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}