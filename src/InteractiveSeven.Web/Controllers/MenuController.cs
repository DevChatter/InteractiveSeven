using Microsoft.AspNetCore.Mvc;

namespace InteractiveSeven.Web.Controllers
{
    public class MenuController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}