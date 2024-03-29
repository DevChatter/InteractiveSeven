﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Payments;

namespace InteractiveSeven.Core.Commands.Currency
{
    public class GiveGilCommand : BaseCommand
    {
        private readonly GilBank _gilBank;
        private readonly IChatClient _chatClient;
        private readonly IChatApi _api;

        public GiveGilCommand(GilBank gilBank, IChatClient chatClient, IChatApi api)
            : base(x => x.GiveGilCommandWords, x => true)
        {
            _gilBank = gilBank;
            _chatClient = chatClient;
            _api = api;
        }

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.DisplayOnly;

        public override async Task Execute(CommandData commandData)
        {
            var (isValid, amount, recipient) = ParseArgs(commandData.Arguments);
            if (!isValid)
            {
                await _chatClient.SendMessage(commandData.Channel,
                    $"Invalid Request - Example usage: !{DefaultCommandWord} DevChatter 100");
                return;
            }

            await AttemptTransfer(commandData, recipient, amount);
        }

        private async Task AttemptTransfer(CommandData commandData, string recipient, int amount)
        {
            bool isBonus = false;
            int withdrawn;
            if (CanSendBonusBits(commandData.User))
            {
                withdrawn = amount;
                isBonus = true;
            }
            else
            {
                int balance;
                (balance, withdrawn) = _gilBank.Withdraw(commandData.User, amount, true);
                if (withdrawn == 0)
                {
                    await _chatClient.SendMessage(commandData.Channel,
                        $"Insufficient funds, {commandData.User.Username}. You have only {balance} gil.");
                    return;
                }
            }

            string fromMessage = isBonus ? "" : $" from {commandData.User.Username}'s account";
            _gilBank.Deposit(new ChatUser { Username = recipient }, withdrawn);
            await _chatClient.SendMessage(commandData.Channel,
                $"Deposited {withdrawn} gil in {recipient}'s account{fromMessage}.");
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

            string recipient = args
                .Except(new[] { amountArg })
                .FirstOrDefault();

            if (recipient == null || amount < 1 || !RecipientIsValid(recipient))
            {
                isValid = false;
            }

            return (isValid, amount, recipient);
        }

        private bool RecipientIsValid(string recipient)
        {
            if (_gilBank.HasAccount(new ChatUser(recipient, null)))
            {
                return true;
            }

            return _api.IsValidUsername(recipient);
        }
    }
}
