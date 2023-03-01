using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Payments;

namespace InteractiveSeven.Core.Commands.Admin
{
    public class UnlockCommand : BaseCommand
    {
        private readonly PaymentProcessor _paymentProcessor;
        private readonly GilBank _gilBank;

        public UnlockCommand(PaymentProcessor paymentProcessor, GilBank gilBank)
            : base(new[] { "BrendanUnlock", "I7Unlock", "HelloI7" }, x => true)
        {
            _paymentProcessor = paymentProcessor;
            _gilBank = gilBank;
        }

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.DisplayOnly;

        public override Task Execute(CommandData commandData, IChatClient chatClient)
        {
            if (commandData.User.IsDev)
            {
                _paymentProcessor.Unlock();
                _gilBank.Unlock();
            }
            return Task.CompletedTask;
        }
    }
}
