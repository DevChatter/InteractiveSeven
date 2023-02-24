using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Payments;

namespace InteractiveSeven.Core.Commands.Admin
{
    public class LockCommand : BaseCommand
    {
        private readonly PaymentProcessor _paymentProcessor;

        public LockCommand(PaymentProcessor paymentProcessor)
            : base(new[] { "BrendanLock", "I7Lock", "ByeI7" }, x => true)
        {
            _paymentProcessor = paymentProcessor;
        }

        public override Task Execute(CommandData commandData, IChatClient chatClient)
        {
            if (commandData.User.IsDevChatter || commandData.User.IsShojy)
            {
                _paymentProcessor.Lock();
            }
            return Task.CompletedTask;
        }
    }
}
