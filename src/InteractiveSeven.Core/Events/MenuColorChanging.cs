using InteractiveSeven.Core.Models;

namespace InteractiveSeven.Core.Events
{
    public class MenuColorChanging : BaseDomainEvent
    {
        public MenuColors MenuColors { get; }
        public string Username { get; }

        public MenuColorChanging(MenuColors menuColors, string username)
        {
            MenuColors = menuColors;
            Username = username;
        }
    }
}
