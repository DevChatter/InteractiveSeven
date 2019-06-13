using InteractiveSeven.Core;
using InteractiveSeven.Core.Bidding;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Model;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Model;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class NameCommand : BaseCommand
    {
        private static readonly string[] CloudWords = { "cloud", "cluod", "clodu" };
        private static readonly string[] BarretWords = { "barret", "baret", "barett", "barrett" };
        private static readonly string[] TifaWords = { "tifa", "tiaf", "tfia" };
        private static readonly string[] AerisWords = { "aeris", "aerith" };
        private static readonly string[] CaitWords = { "caitsith", "cait" };
        private static readonly string[] CidWords = { "cid" };
        private static readonly string[] RedWords = { "red", "redxiii", "nanaki", "redxii", "redxiiii", "red13" };
        private static readonly string[] VincentWords = { "vincent", "vince" };
        private static readonly string[] YuffieWords = { "yuffie" };
        private readonly ITwitchClient _twitchClient;
        private readonly GilBank _gilBank;

        private static string[] AllWords
            => CloudWords
                .Union(BarretWords)
                .Union(TifaWords)
                .Union(AerisWords)
                .Union(CaitWords)
                .Union(CidWords)
                .Union(RedWords)
                .Union(VincentWords)
                .Union(YuffieWords)
                .ToArray();

        public NameBiddingSettings Settings => ApplicationSettings.Instance.NameBiddingSettings;

        public NameCommand(ITwitchClient twitchClient, GilBank gilBank)
            : base(AllWords, x => x.NameBiddingSettings.Enabled)
        {
            _twitchClient = twitchClient;
            _gilBank = gilBank;
        }

        public override void Execute(CommandData data)
        {
            if (ShouldTriggerFor(data, CloudWords, Settings.NamingCloudEnabled))
            {
                TriggerDomainEvent(CharNames.Cloud, data);
            }
            else if (ShouldTriggerFor(data, BarretWords, Settings.NamingBarretEnabled))
            {
                TriggerDomainEvent(CharNames.Barret, data);
            }
            else if (ShouldTriggerFor(data, TifaWords, Settings.NamingTifaEnabled))
            {
                TriggerDomainEvent(CharNames.Tifa, data);
            }
            else if (ShouldTriggerFor(data, AerisWords, Settings.NamingAerisEnabled))
            {
                TriggerDomainEvent(CharNames.Aeris, data);
            }
            else if (ShouldTriggerFor(data, CaitWords, Settings.NamingCaitSithEnabled))
            {
                TriggerDomainEvent(CharNames.CaitSith, data);
            }
            else if (ShouldTriggerFor(data, CidWords, Settings.NamingCidEnabled))
            {
                TriggerDomainEvent(CharNames.Cid, data);
            }
            else if (ShouldTriggerFor(data, RedWords, Settings.NamingRedEnabled))
            {
                TriggerDomainEvent(CharNames.Red, data);
            }
            else if (ShouldTriggerFor(data, VincentWords, Settings.NamingVincentEnabled))
            {
                TriggerDomainEvent(CharNames.Vincent, data);
            }
            else if (ShouldTriggerFor(data, YuffieWords, Settings.NamingYuffieEnabled))
            {
                TriggerDomainEvent(CharNames.Yuffie, data);
            }
        }

        private static bool ShouldTriggerFor(CommandData commandData, string[] words, bool enabled)
            => words.Any(word => word.EqualsIns(commandData.CommandText)) && enabled;

        private void TriggerDomainEvent(string charName, CommandData data)
        {
            int gil = GetGilFromCommandData(data);
            if (!CanOverrideBitRestriction(data.User))
            {
                (int balance, int withdrawn) = _gilBank.Withdraw(data.User, gil, true);
                if (withdrawn == 0)
                {
                    string message = $"You don't have {gil} gil, {data.User.Username}. You have {balance} gil.";
                    _twitchClient.SendMessage(data.Channel, message);
                    return;
                }
            }

            if (gil < 1)
            {
                _twitchClient.SendMessage(data.Channel, $"Be sure to include a gil amount in your name bid, {data.User.Username}");
                return;
            }

            string newName = data.Arguments.FirstOrDefault() ?? "";

            var bidRecord = new BidRecord(data.User.Username, data.User.UserId, gil);
            var domainEvent = new NameVoteReceived(charName, newName, bidRecord);
            DomainEvents.Raise(domainEvent);
        }

        private static int GetGilFromCommandData(CommandData data)
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

        private bool CanOverrideBitRestriction(ChatUser user)
            => (Settings.AllowModBits && user.IsMod) || user.IsMe || user.IsBroadcaster;

    }
}