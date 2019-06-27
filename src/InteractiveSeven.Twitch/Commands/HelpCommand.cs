using InteractiveSeven.Twitch.Model;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class HelpCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;

        public HelpCommand(ITwitchClient twitchClient)
            : base(x => x.HelpCommandWords, x => true)
        {
            _twitchClient = twitchClient;
        }

        public override void Execute(CommandData commandData)
        {
            string[] commandWords = Settings.CommandSettings.AllWordSets.Select(wordSet => wordSet.Words().First()).ToArray();
            _twitchClient.SendMessage(commandData.Channel,
                $"These are the available commands: {string.Join(", ", commandWords)}");
        }
    }
}