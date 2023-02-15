using InteractiveSeven.Core.Commands;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Payments;

namespace InteractiveSeven.Twitch.Commands
{
    public class LockCommand : BaseCommand
    {
        private readonly PaymentProcessor _paymentProcessor;

        public LockCommand(PaymentProcessor paymentProcessor)
            : base(new[] { "BrendanLock", "I7Lock" }, x => true)
        {
            _paymentProcessor = paymentProcessor;
        }

        public override void Execute(in CommandData commandData)
        {
            if (commandData.User.IsDevChatter || commandData.User.IsShojy)
            {
                _paymentProcessor.Lock();
            }
        }
    }
}
