using Microsoft.AspNetCore.Mvc;

namespace InteractiveSeven.Web.Controllers
{
    [Route("[controller]")]
    public class MenuController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}