﻿using System.Linq;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Commands.Bidding
{
    public class NameCommand : BaseCommand
    {
        private readonly IChatClient _chatClient;
        private readonly GilBank _gilBank;

        private static CommandSettings CmdSettings => ApplicationSettings.Instance.CommandSettings;

        private static string[] AllWords(CommandSettings settings) =>
            settings.CloudCommandWords
                .Union(settings.BarretCommandWords)
                .Union(settings.TifaCommandWords)
                .Union(settings.AerisCommandWords)
                .Union(settings.CaitCommandWords)
                .Union(settings.CidCommandWords)
                .Union(settings.RedCommandWords)
                .Union(settings.VincentCommandWords)
                .Union(settings.YuffieCommandWords)
                .ToArray();

        public NameBiddingSettings NameBidSettings => ApplicationSettings.Instance.NameBiddingSettings;

        public NameCommand(IChatClient chatClient, GilBank gilBank)
            : base(AllWords, x => x.NameBiddingSettings.Enabled)
        {
            _chatClient = chatClient;
            _gilBank = gilBank;
        }

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.MildEffect;

        public override void Execute(in CommandData data)
        {
            if (ShouldTriggerFor(data, CmdSettings.CloudCommandWords, NameBidSettings.NamingCloudEnabled))
            {
                TriggerDomainEvent(CharNames.Cloud, data);
            }
            else if (ShouldTriggerFor(data, CmdSettings.BarretCommandWords, NameBidSettings.NamingBarretEnabled))
            {
                TriggerDomainEvent(CharNames.Barret, data);
            }
            else if (ShouldTriggerFor(data, CmdSettings.TifaCommandWords, NameBidSettings.NamingTifaEnabled))
            {
                TriggerDomainEvent(CharNames.Tifa, data);
            }
            else if (ShouldTriggerFor(data, CmdSettings.AerisCommandWords, NameBidSettings.NamingAerisEnabled))
            {
                TriggerDomainEvent(CharNames.Aeris, data);
            }
            else if (ShouldTriggerFor(data, CmdSettings.CaitCommandWords, NameBidSettings.NamingCaitSithEnabled))
            {
                TriggerDomainEvent(CharNames.CaitSith, data);
            }
            else if (ShouldTriggerFor(data, CmdSettings.CidCommandWords, NameBidSettings.NamingCidEnabled))
            {
                TriggerDomainEvent(CharNames.Cid, data);
            }
            else if (ShouldTriggerFor(data, CmdSettings.RedCommandWords, NameBidSettings.NamingRedEnabled))
            {
                TriggerDomainEvent(CharNames.Red, data);
            }
            else if (ShouldTriggerFor(data, CmdSettings.VincentCommandWords, NameBidSettings.NamingVincentEnabled))
            {
                TriggerDomainEvent(CharNames.Vincent, data);
            }
            else if (ShouldTriggerFor(data, CmdSettings.YuffieCommandWords, NameBidSettings.NamingYuffieEnabled))
            {
                TriggerDomainEvent(CharNames.Yuffie, data);
            }
        }

        private static bool ShouldTriggerFor(in CommandData commandData, string[] words, bool enabled)
        {
            string commandText = commandData.CommandText;
            return words.Any(word => word.EqualsIns(commandText)) && enabled;
        }

        private void TriggerDomainEvent(in CharNames charName, in CommandData data)
        {
            int gil = GetGilFromCommandData(data);

            if (gil < 1)
            {
                _chatClient.SendMessage(data.Channel, $"Be sure to include a gil amount in your name bid, {data.User.Username}");
                return;
            }

            if (!CanOverrideBitRestriction(data.User))
            {
                (int balance, int withdrawn) = _gilBank.Withdraw(data.User, gil, true);
                if (withdrawn == 0)
                {
                    string message = $"You don't have {gil} gil, {data.User.Username}. You have {balance} gil.";
                    _chatClient.SendMessage(data.Channel, message);
                    return;
                }
            }

            string newName = data.Arguments.FirstOrDefault() ?? "";

            var bidRecord = new BidRecord(data.User.Username, data.User.UserId, gil);
            var domainEvent = new NameVoteReceived(charName, newName, bidRecord);
            DomainEvents.Raise(domainEvent);
        }

        private static int GetGilFromCommandData(in CommandData data)
        {
            int gil = data.Arguments.Count > 1
                ? data.Arguments.Skip(1).Max(arg => arg.SafeIntParse())
                : 0;
            if (gil == 0 && data.Bits > 0) // If their cheer is the bits amount
            {
                gil = data.Bits;
            }

            return gil;
        }

        private bool CanOverrideBitRestriction(in ChatUser user)
            => (NameBidSettings.AllowModBits && user.IsMod) || user.IsMe || user.IsBroadcaster;

    }
}