using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace InteractiveSeven.Web.Hubs
{
    public class MenuHubEmitter : IMenuHubEmitter
    {
        private readonly IHubContext<MenuHub, IMenuNotification> _menuHubContext;

        public MenuHubEmitter(IHubContext<MenuHub, IMenuNotification> menuHubContext)
        {
            _menuHubContext = menuHubContext;
        }

        public Task ShowNewColors(MenuColors menuColors)
        {
            return _menuHubContext.Clients.All.ColorChanged(
                menuColors.TopLeft,
                menuColors.TopRight,
                menuColors.BotLeft,
                menuColors.BotRight
            );
        }
    }
}