using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Payments;

namespace InteractiveSeven.Core.Commands.Currency
{
    public class BalanceCommand : BaseCommand
    {
        private readonly IChatClient _chatClient;
        private readonly GilBank _gilBank;

        public BalanceCommand(IChatClient chatClient, GilBank gilBank)
            : base(x => x.BalanceCommandWords, x => true)
        {
            _chatClient = chatClient;
            _gilBank = gilBank;
        }

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.DisplayOnly;

        public override void Execute(in CommandData data)
        {
            var balance = _gilBank.CheckBalance(data.User);

            _chatClient.SendMessage(data.Channel, $"You have {balance} gil, {data.User.Username}.");
        }
    }
}
