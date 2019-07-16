using System.Drawing;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace InteractiveSeven.Web.Hubs
{
    public class MenuHub : Hub<IMenuNotification>
    {
    }

    public interface IMenuNotification
    {
        Task ColorChanged(Color topLeft, Color topRight, Color botLeft, Color botRight);
    }

    struct SimpleColor
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

        public SimpleColor(Color color)
        {
            Red = color.R;
            Green = color.G;
            Blue = color.B;
        }
    }
}