using Microsoft.AspNetCore.Mvc;

namespace InteractiveSeven.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public GameStatus Data()
        //{
        //    return Program.PartyStatus;
        //}
    }
}