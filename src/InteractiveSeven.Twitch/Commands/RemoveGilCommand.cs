using System.Collections.Generic;
using System.Linq;
using InteractiveSeven.Core;
using InteractiveSeven.Core.Model;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Twitch.Model;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class RemoveGilCommand : BaseCommand
    {
        private readonly GilBank _gilBank;
        private readonly ITwitchClient _twitchClient;

        public RemoveGilCommand(GilBank gilBank, ITwitchClient twitchClient)
            : base(x => x.RemoveGilCommandWords, x => true)
        {
            _gilBank = gilBank;
            _twitchClient = twitchClient;
        }

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.DisplayOnly;

        public override void Execute(in CommandData commandData)
        {
            if (!CanSendBonusBits(commandData.User)) return;

            var (isValid, amount, target) = ParseArgs(commandData.Arguments);
            if (!isValid)
            {
                _twitchClient.SendMessage(commandData.Channel,
                    $"Invalid Request - Example usage: !{DefaultCommandWord} DevChatter 100");
                return;
            }

            AttemptRemoval(commandData, target, amount);
        }

        private void AttemptRemoval(in CommandData commandData, string target, int amount)
        {
            var (balance, withdrawn) = _gilBank.Withdraw(new ChatUser { Username = target }, amount);
            string status = withdrawn != amount ? "Insufficient funds:" : "Success:";

            string message = $"{status} Removed {withdrawn} gil from {target}'s account.";
            _twitchClient.SendMessage(commandData.Channel, message);
        }

        private bool CanSendBonusBits(in ChatUser user)
        {
            return user.IsBroadcaster
                   || user.IsMe
                   || (user.IsMod && Settings.ModsGiveBonusBits);
        }

        private (bool isValid, int amount, string recipient) ParseArgs(IList<string> args)
        {
            bool isValid = true;

            (string amountArg, int amount) = args
                .Select(x => (Arg: x, Gil: x.SafeIntParse()))
                .FirstOrDefault(x => x.Gil > 0);

            string target = args
                .Except(new[] { amountArg })
                .FirstOrDefault(x => _gilBank.HasAccount(new ChatUser(x, null)));

            if (target == null || amount < 1)
            {
                isValid = false;
            }

            return (isValid, amount, target);
        }
    }
}