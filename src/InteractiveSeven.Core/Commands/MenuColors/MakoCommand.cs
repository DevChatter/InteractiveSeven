using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Payments;

namespace InteractiveSeven.Core.Commands.MenuColors
{
    public class MakoCommand : BaseCommand
    {
        private readonly PaymentProcessor _paymentProcessor;

        public MakoCommand(PaymentProcessor paymentProcessor)
            : base(x => x.MakoCommandWords, x => x.MenuSettings.EnableMakoCommand)
        {
            _paymentProcessor = paymentProcessor;
        }

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.DisplayOnly;

        public override async Task Execute(CommandData commandData)
        {
            GilTransaction gilTransaction = await _paymentProcessor.ProcessPayment(
                commandData, Settings.MenuSettings.MakoModeCost, Settings.MenuSettings.AllowModOverride);

            if (gilTransaction.Paid)
            {
                DomainEvents.Raise(new MakoModeStarted());
            }
        }
    }
}
