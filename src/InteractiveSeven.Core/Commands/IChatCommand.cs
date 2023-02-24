using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;

namespace InteractiveSeven.Core.Commands
{
    public interface IChatCommand
    {
        GamePlayEffects GamePlayEffects { get; }
        bool ShouldExecute(string commandWord);
        Task Execute(CommandData commandData, IChatClient chatClient);
    }
}
