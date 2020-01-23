using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.ViewModels;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace InteractiveSeven.Web.Hubs
{
    public class StatusHub : Hub<IStatusNotification>
    {
        private readonly MenuColorViewModel _menuColorViewModel;
        private readonly IStatusHubEmitter _statusHubEmitter;

        public StatusHub(MenuColorViewModel menuColorViewModel, IStatusHubEmitter statusHubEmitter)
        {
            _menuColorViewModel = menuColorViewModel;
            _statusHubEmitter = statusHubEmitter;
        }

        public override Task OnConnectedAsync()
        {
            _statusHubEmitter.ShowNewColors(_menuColorViewModel.PreviewImage);
            return base.OnConnectedAsync();
        }
    }

    public interface IStatusNotification
    {
        Task PartyStatusChanged(PartyStatusViewModel partyStatus);
        Task ColorChanged(string topLeft, string topRight, string botLeft, string botRight);
        Task ShowEvent(string eventText);
    }
}
