using InteractiveSeven.Core.Chat;

namespace InteractiveSeven.Core.Commands
{
    public interface IChatCommand
    {
        GamePlayEffects GamePlayEffects { get; }
        bool ShouldExecute(string commandWord);
        void Execute(in CommandData commandData);
    }
}
