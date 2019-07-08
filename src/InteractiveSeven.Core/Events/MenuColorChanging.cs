using InteractiveSeven.Core.Model;
using InteractiveSeven.Core.Models;

namespace InteractiveSeven.Core.Events
{
    public class MenuColorChanging : BaseDomainEvent
    {
        public MenuColors MenuColors { get; }
        public ChatUser User { get; }
        public int Gil { get; set; }

        public MenuColorChanging(MenuColors menuColors, in ChatUser user, int gil)
        {
            MenuColors = menuColors;
            User = user;
            Gil = gil;
        }
    }
}
