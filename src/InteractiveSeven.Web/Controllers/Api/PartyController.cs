using InteractiveSeven.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InteractiveSeven.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartyController : ControllerBase
    {
        private readonly PartyStatusViewModel _viewModel;

        public PartyController(PartyStatusViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        [HttpGet]
        public PartyStatusViewModel Get()
        {
            return _viewModel;
        }
    }
}