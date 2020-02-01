using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.ViewModels;
using System.Threading.Tasks;

namespace InteractiveSeven.Core.Emitters
{
    public interface IStatusHubEmitter
    {
        Task ShowNewPartyStatus(PartyStatusViewModel partyStatus);
        Task ShowNewColors(MenuColors menuColors);
        Task ShowEvent(string eventText, string soundFile = null);
    }
}