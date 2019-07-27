using InteractiveSeven.Core.ViewModels;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace InteractiveSeven.Web.Hubs
{
    public class StatusHub : Hub<IStatusNotification>
    {
    }

    public interface IStatusNotification
    {
        Task PartyStatusChanged(PartyStatusViewModel partyStatus);
        Task ColorChanged(string topLeft, string topRight, string botLeft, string botRight);
    }
}
