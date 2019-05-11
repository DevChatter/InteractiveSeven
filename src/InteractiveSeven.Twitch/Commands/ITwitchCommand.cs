using TwitchLib.Client.Models;

namespace InteractiveSeven.Twitch.Commands
{
    public interface ITwitchCommand
    {
        bool ShouldExecute(string commandWord);
        void Execute(ChatCommand chatCommand);
    }
}