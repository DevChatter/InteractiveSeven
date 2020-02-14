using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.ViewModels;
using Microsoft.AspNetCore.SignalR;
using System.Drawing;
using System.Threading.Tasks;

namespace InteractiveSeven.Web.Hubs
{
    public class StatusHubEmitter : IStatusHubEmitter
    {
        private readonly IHubContext<StatusHub, IStatusNotification> _statusHubContext;

        public StatusHubEmitter(IHubContext<StatusHub, IStatusNotification> statusHubContext)
        {
            _statusHubContext = statusHubContext;
        }

        public Task ShowNewPartyStatus(PartyStatusViewModel partyStatus)
        {
            return _statusHubContext.Clients.All.PartyStatusChanged(partyStatus);
        }

        public Task ShowNewColors(MenuColors menuColors)
        {
            return _statusHubContext.Clients.All.ColorChanged(
                ColorTranslator.ToHtml(menuColors.TopLeft),
                ColorTranslator.ToHtml(menuColors.TopRight),
                ColorTranslator.ToHtml(menuColors.BotLeft),
                ColorTranslator.ToHtml(menuColors.BotRight)
            );
        }

        public Task ShowEvent(string eventText, string subText, string soundFile = null)
        {
            return _statusHubContext.Clients.All.ShowEvent(eventText, subText, soundFile);
        }
    }
}