using InteractiveSeven.Core.Payments;
using InteractiveSeven.Twitch.Model;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class BalanceCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;
        private readonly GilBank _gilBank;

        public BalanceCommand(ITwitchClient twitchClient, GilBank gilBank)
            : base(x => x.BalanceCommandWords, x => true)
        {
            _twitchClient = twitchClient;
            _gilBank = gilBank;
        }

        public override void Execute(in CommandData data)
        {
            int balance = _gilBank.CheckBalance(data.User);

            _twitchClient.SendMessage(data.Channel, $"You have {balance} gil, {data.User.Username}.");
        }
    }
}