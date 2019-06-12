using InteractiveSeven.Core;
using InteractiveSeven.Core.Bidding;
using InteractiveSeven.Core.Events;
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
        private static readonly string[] CaitWords = { "caitsith" };
        private static readonly string[] CidWords = { "cid" };
        private static readonly string[] RedWords = { "red", "redxiii", "nanaki", "redxii", "redxiiii", "red13" };
        private static readonly string[] VincentWords = { "vincent", "vince" };
        private static readonly string[] YuffieWords = { "yuffie" };
        private readonly ITwitchClient _twitchClient;

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

        public NameCommand(ITwitchClient twitchClient)
            : base(AllWords, x => x.NameBiddingSettings.Enabled)
        {
            _twitchClient = twitchClient;
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
            if (data.Bits == 0 && Settings.AllowModBits && (data.User.IsMod || data.User.IsMe || data.User.IsBroadcaster))
            {
                int number = data.Arguments.Max(arg => arg.SafeIntParse());
                if (number > 0)
                {
                    data.Bits = number;
                }
            }

            if (data.Bits < 1)
            {
                _twitchClient.SendMessage(data.Channel, $"Be sure to include bits in your name bid, {data.User.Username}");
                return;
            }

            string newName = data.Arguments.FirstOrDefault() ?? "";

            var bidRecord = new BidRecord(data.User.Username, data.User.UserId, data.Bits);
            var domainEvent = new NameVoteReceived(charName, newName, bidRecord);
            DomainEvents.Raise(domainEvent);
        }
    }
}