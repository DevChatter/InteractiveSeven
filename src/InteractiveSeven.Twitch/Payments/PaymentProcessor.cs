using InteractiveSeven.Core.Payments;
using InteractiveSeven.Twitch.Model;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Payments
{
    public class PaymentProcessor
    {
        private readonly GilBank _gilBank;
        private readonly ITwitchClient _twitchClient;

        public PaymentProcessor(GilBank gilBank, ITwitchClient twitchClient)
        {
            _gilBank = gilBank;
            _twitchClient = twitchClient;
        }

        public GilTransaction ProcessPayment(CommandData commandData, string failMessage, int amount, bool canModsOverride)
        {
            int gilSpent = 0;
            bool requiresBits = !commandData.User.IsBroadcaster
                                && !commandData.User.IsMe
                                && (!canModsOverride || !commandData.User.IsMod);
            if (requiresBits)
            {
                (_, gilSpent) = _gilBank.Withdraw(commandData.User, amount, true);
                if (gilSpent < amount)
                {
                    _twitchClient.SendMessage(commandData.Channel, failMessage);
                    return new GilTransaction(false, gilSpent);
                }
            }

            return new GilTransaction(true, gilSpent);
        }
    }
}