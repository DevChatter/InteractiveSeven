using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Payments;

namespace InteractiveSeven.Core.Commands.Admin
{
    public class UnlockCommand : BaseCommand
    {
        private readonly PaymentProcessor _paymentProcessor;

        public UnlockCommand(PaymentProcessor paymentProcessor)
            : base(new[] { "BrendanUnlock", "I7Unlock", "HelloI7" }, x => true)
        {
            _paymentProcessor = paymentProcessor;
        }

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.DisplayOnly;

        public override Task Execute(CommandData commandData, IChatClient chatClient)
        {
            if (commandData.User.IsDevChatter || commandData.User.IsShojy)
            {
                _paymentProcessor.Unlock();
            }
            return Task.CompletedTask;
        }
    }
}
