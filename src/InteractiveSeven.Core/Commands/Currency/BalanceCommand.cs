using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Payments;

namespace InteractiveSeven.Core.Commands.Currency
{
    public class BalanceCommand : BaseCommand
    {
        private readonly GilBank _gilBank;

        public BalanceCommand(GilBank gilBank)
            : base(x => x.BalanceCommandWords, x => true)
        {
            _gilBank = gilBank;
        }

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.DisplayOnly;

        public override async Task Execute(CommandData data, IChatClient chatClient)
        {
            var balance = _gilBank.CheckBalance(data.User);

            await chatClient.SendMessage(data.Channel, $"You have {balance} gil, {data.User.Username}.");
        }
    }
}
