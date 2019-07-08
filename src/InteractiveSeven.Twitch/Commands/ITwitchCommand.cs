using InteractiveSeven.Twitch.Model;

namespace InteractiveSeven.Twitch.Commands
{
    public interface ITwitchCommand
    {
        bool ShouldExecute(string commandWord);
        void Execute(in CommandData commandData);
    }
}