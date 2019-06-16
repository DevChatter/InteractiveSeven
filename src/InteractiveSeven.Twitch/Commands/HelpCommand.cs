using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Model;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class HelpCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;

        private CommandSettings CommandSettings => ApplicationSettings.Instance.CommandSettings;

        public HelpCommand(ITwitchClient twitchClient)
            : base(x => x.HelpCommandWords, x => true)
        {
            _twitchClient = twitchClient;
        }

        public override void Execute(CommandData commandData)
        {
            string[] commandWords = CommandSettings.AllWordSets.Select(wordSet => wordSet.Words().First()).ToArray();
            _twitchClient.SendMessage(commandData.Channel,
                $"These are the available commands: {string.Join(", ", commandWords)}");
        }
    }
}