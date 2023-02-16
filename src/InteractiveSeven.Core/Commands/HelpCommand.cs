using System.Linq;
using InteractiveSeven.Core.Chat;

namespace InteractiveSeven.Core.Commands
{
    public class HelpCommand : BaseCommand
    {
        private readonly IChatClient _chatClient;

        public HelpCommand(IChatClient chatClient)
            : base(x => x.HelpCommandWords, x => true)
        {
            _chatClient = chatClient;
        }

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.DisplayOnly;

        public override void Execute(in CommandData commandData)
        {
            string[] commandWords = Settings.CommandSettings.AllWordSets.Select(wordSet => wordSet.Words().First()).ToArray();
            _chatClient.SendMessage(commandData.Channel,
                $"These are the available commands: {string.Join(", ", commandWords)}");
        }
    }
}
