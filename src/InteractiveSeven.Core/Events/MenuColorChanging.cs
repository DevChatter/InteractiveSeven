using InteractiveSeven.Core.Models;

namespace InteractiveSeven.Core.Events
{
    public class MenuColorChanging : BaseDomainEvent
    {
        public MenuColors MenuColors { get; set; }

        public MenuColorChanging(MenuColors menuColors)
        {
            MenuColors = menuColors;
        }
    }
}
