using System.Linq;
using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;

namespace InteractiveSeven.Core.Commands
{
    public class HelpCommand : BaseCommand
    {
        public HelpCommand()
            : base(x => x.HelpCommandWords, x => true)
        {
        }

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.DisplayOnly;

        public override async Task Execute(CommandData commandData, IChatClient chatClient)
        {
            string[] commandWords = Settings.CommandSettings.AllWordSets.Select(wordSet => wordSet.Words().First()).ToArray();
            string message = $"These are the available commands: {string.Join(", ", commandWords)}";
            await chatClient.SendMessage(commandData.Channel, message);
        }
    }
}
