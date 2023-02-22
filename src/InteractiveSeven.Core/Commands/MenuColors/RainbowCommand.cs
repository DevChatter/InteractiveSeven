using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Payments;

namespace InteractiveSeven.Core.Commands.MenuColors
{
    public class RainbowCommand : BaseCommand
    {
        private readonly PaymentProcessor _paymentProcessor;

        public RainbowCommand(PaymentProcessor paymentProcessor)
            : base(x => x.RainbowCommandWords, x => x.MenuSettings.EnableRainbowCommand)
        {
            _paymentProcessor = paymentProcessor;
        }

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.DisplayOnly;

        public override async Task Execute(CommandData commandData)
        {
            GilTransaction gilTransaction = await _paymentProcessor.ProcessPayment(
                commandData, Settings.MenuSettings.RainbowModeCost, Settings.MenuSettings.AllowModOverride);

            if (gilTransaction.Paid)
            {
                DomainEvents.Raise(new RainbowModeStarted());
            }
        }
    }
}
