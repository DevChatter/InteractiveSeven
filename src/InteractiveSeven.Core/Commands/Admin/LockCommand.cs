using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Payments;

namespace InteractiveSeven.Core.Commands.Admin
{
    public class LockCommand : BaseCommand
    {
        private readonly PaymentProcessor _paymentProcessor;
        private readonly GilBank _gilBank;

        public LockCommand(PaymentProcessor paymentProcessor, GilBank gilBank)
            : base(new[] { "BrendanLock", "I7Lock", "ByeI7" }, x => true)
        {
            _paymentProcessor = paymentProcessor;
            _gilBank = gilBank;
        }

        public override Task Execute(CommandData commandData, IChatClient chatClient)
        {
            if (commandData.User.IsDev)
            {
                _paymentProcessor.Lock();
                _gilBank.Lock();
            }
            return Task.CompletedTask;
        }
    }
}
