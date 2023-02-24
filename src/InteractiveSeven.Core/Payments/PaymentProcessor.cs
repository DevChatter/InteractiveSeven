using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;

namespace InteractiveSeven.Core.Payments
{
    public class PaymentProcessor
    {
        private readonly GilBank _gilBank;
        private bool _unlocked;

        public PaymentProcessor(GilBank gilBank)
        {
            _gilBank = gilBank;
        }

        public async Task<GilTransaction> ProcessPayment(CommandData commandData, int amount, bool canModsOverride, IChatClient chatClient)
        {
            if (_unlocked && commandData.User.IsDevChatter) return new GilTransaction(true, 0);

            int gilSpent = 0;
            bool requiresBits = !commandData.User.IsBroadcaster
                                && !commandData.User.IsMe
                                && (!canModsOverride || !commandData.User.IsMod);
            if (requiresBits)
            {
                (_, gilSpent) = _gilBank.Withdraw(commandData.User, amount, true);
                if (gilSpent < amount)
                {
                    await chatClient.SendMessage(commandData.Channel,
                        $"Sorry, '!{commandData.CommandText}' has a minimum gil cost of {amount}. Cheer for gil.");
                    return new GilTransaction(false, gilSpent);
                }
            }

            return new GilTransaction(true, gilSpent);
        }

        public void Lock() => _unlocked = false;
        public void Unlock() => _unlocked = true;
    }
}
