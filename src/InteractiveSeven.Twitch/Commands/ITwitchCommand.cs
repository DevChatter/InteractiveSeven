using InteractiveSeven.Core;
using InteractiveSeven.Twitch.Model;

namespace InteractiveSeven.Twitch.Commands
{
    public interface ITwitchCommand
    {
        GamePlayEffects GamePlayEffects { get; }
        bool ShouldExecute(string commandWord);
        void Execute(in CommandData commandData);
    }
}