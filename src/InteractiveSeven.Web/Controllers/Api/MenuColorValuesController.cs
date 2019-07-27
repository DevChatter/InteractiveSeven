using InteractiveSeven.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InteractiveSeven.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuColorValuesController : ControllerBase
    {
        private readonly MenuColorViewModel _menuColorViewModel;

        public MenuColorValuesController(MenuColorViewModel menuColorViewModel)
        {
            _menuColorViewModel = menuColorViewModel;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[]
            {
                _menuColorViewModel.TopLeft.ToString(),
                _menuColorViewModel.TopRight.ToString(),
                _menuColorViewModel.BotLeft.ToString(),
                _menuColorViewModel.BotRight.ToString()
            };
        }

    }
}