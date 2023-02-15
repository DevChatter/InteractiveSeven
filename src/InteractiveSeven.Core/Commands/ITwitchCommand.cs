using InteractiveSeven.Core.Models;

namespace InteractiveSeven.Core.Commands
{
    public interface ITwitchCommand
    {
        GamePlayEffects GamePlayEffects { get; }
        bool ShouldExecute(string commandWord);
        void Execute(in CommandData commandData);
    }
}
