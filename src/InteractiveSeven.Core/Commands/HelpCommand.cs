using System.Linq;
using InteractiveSeven.Core.Models;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Core.Commands
{
    public class HelpCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;

        public HelpCommand(ITwitchClient twitchClient)
            : base(x => x.HelpCommandWords, x => true)
        {
            _twitchClient = twitchClient;
        }

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.DisplayOnly;

        public override void Execute(in CommandData commandData)
        {
            string[] commandWords = Settings.CommandSettings.AllWordSets.Select(wordSet => wordSet.Words().First()).ToArray();
            _twitchClient.SendMessage(commandData.Channel,
                $"These are the available commands: {string.Join(", ", commandWords)}");
        }
    }
}
