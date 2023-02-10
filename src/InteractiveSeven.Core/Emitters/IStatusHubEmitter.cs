using System.Threading.Tasks;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.ViewModels;

namespace InteractiveSeven.Core.Emitters
{
    public interface IStatusHubEmitter
    {
        Task ShowNewPartyStatus(PartyStatusViewModel partyStatus);
        Task ShowNewColors(MenuColors menuColors);
        Task ShowEvent(string eventText, string subText = null, string soundFile = null);
    }
}