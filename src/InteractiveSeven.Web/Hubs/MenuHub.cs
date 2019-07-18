using Microsoft.AspNetCore.SignalR;
using System.Drawing;
using System.Threading.Tasks;

namespace InteractiveSeven.Web.Hubs
{
    public class MenuHub : Hub<IMenuNotification>
    {
    }

    public interface IMenuNotification
    {
        Task ColorChanged(string topLeft, string topRight, string botLeft, string botRight);
    }
}
