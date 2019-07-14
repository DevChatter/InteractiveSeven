using InteractiveSeven.Core.Events;
using InteractiveSeven.Twitch.Model;
using InteractiveSeven.Twitch.Payments;

namespace InteractiveSeven.Twitch.Commands
{
    public class MakoCommand : BaseCommand
    {
        private readonly PaymentProcessor _paymentProcessor;

        public MakoCommand(PaymentProcessor paymentProcessor)
            : base(x => x.MakoCommandWords, x => x.MenuSettings.EnableMakoCommand)
        {
            _paymentProcessor = paymentProcessor;
        }

        public override void Execute(in CommandData commandData)
        {
            GilTransaction gilTransaction = _paymentProcessor.ProcessPayment(
                commandData, Settings.MenuSettings.MakoModeCost, Settings.MenuSettings.AllowModOverride);

            if (!gilTransaction.Paid)
            {
                return;
            }


            DomainEvents.Raise(new MakoModeStarted());
        }
    }
}