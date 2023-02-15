using InteractiveSeven.Core;
using InteractiveSeven.Core.Commands;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Twitch.Payments;

namespace InteractiveSeven.Twitch.Commands
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

        public override void Execute(in CommandData commandData)
        {
            GilTransaction gilTransaction = _paymentProcessor.ProcessPayment(
                commandData, Settings.MenuSettings.RainbowModeCost, Settings.MenuSettings.AllowModOverride);

            if (!gilTransaction.Paid)
            {
                return;
            }

            DomainEvents.Raise(new RainbowModeStarted());
        }
    }
}
