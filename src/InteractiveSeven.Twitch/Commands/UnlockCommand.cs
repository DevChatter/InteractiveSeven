using InteractiveSeven.Twitch.Model;
using InteractiveSeven.Twitch.Payments;

namespace InteractiveSeven.Twitch.Commands
{
    public class UnlockCommand : BaseCommand
    {
        private readonly PaymentProcessor _paymentProcessor;

        public UnlockCommand(PaymentProcessor paymentProcessor)
            : base(new[] { "BrendanUnlock" }, x => true)
        {
            _paymentProcessor = paymentProcessor;
        }

        public override void Execute(in CommandData commandData)
        {
            if (commandData.User.IsDevChatter)
            {
                _paymentProcessor.Unlock();
            }
        }
    }
}