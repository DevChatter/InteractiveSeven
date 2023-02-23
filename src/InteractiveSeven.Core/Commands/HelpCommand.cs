using System.Linq;
using System.Threading.Tasks;
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

        public override async Task Execute(CommandData commandData)
        {
            string[] commandWords = Settings.CommandSettings.AllWordSets.Select(wordSet => wordSet.Words().First()).ToArray();
            await _chatClient.SendMessage(commandData.Channel,
                $"These are the available commands: {string.Join(", ", commandWords)}");
        }
    }
}
