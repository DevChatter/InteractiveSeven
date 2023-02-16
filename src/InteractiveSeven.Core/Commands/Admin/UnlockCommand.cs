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

        public override void Execute(in CommandData commandData)
        {
            if (commandData.User.IsDevChatter || commandData.User.IsShojy)
            {
                _paymentProcessor.Unlock();
            }
        }
    }
}
