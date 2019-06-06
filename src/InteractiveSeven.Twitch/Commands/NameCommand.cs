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
        private static readonly string[] RedWords = { "red", "redxiii", "nanaki", "redxii", "redxiiii" };
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
            : base(AllWords)
        {
            _twitchClient = twitchClient;
        }

        public override void Execute(CommandData data)
        {
            if (!Settings.Enabled) return;

            if (ShouldTriggerFor(data, CloudWords, Settings.NamingCloudEnabled))
            {
                TriggerDomainEvent(Constants.Cloud, data);
            }
            else if (ShouldTriggerFor(data, BarretWords, Settings.NamingBarretEnabled))
            {
                TriggerDomainEvent(Constants.Barret, data);
            }
            else if (ShouldTriggerFor(data, TifaWords, Settings.NamingTifaEnabled))
            {
                TriggerDomainEvent(Constants.Tifa, data);
            }
            else if (ShouldTriggerFor(data, AerisWords, Settings.NamingAerisEnabled))
            {
                TriggerDomainEvent(Constants.Aeris, data);
            }
            else if (ShouldTriggerFor(data, CaitWords, Settings.NamingCaitSithEnabled))
            {
                TriggerDomainEvent(Constants.CaitSith, data);
            }
            else if (ShouldTriggerFor(data, CidWords, Settings.NamingCidEnabled))
            {
                TriggerDomainEvent(Constants.Cid, data);
            }
            else if (ShouldTriggerFor(data, RedWords, Settings.NamingRedEnabled))
            {
                TriggerDomainEvent(Constants.Red, data);
            }
            else if (ShouldTriggerFor(data, VincentWords, Settings.NamingVincentEnabled))
            {
                TriggerDomainEvent(Constants.Vincent, data);
            }
            else if (ShouldTriggerFor(data, YuffieWords, Settings.NamingYuffieEnabled))
            {
                TriggerDomainEvent(Constants.Yuffie, data);
            }
        }

        private static bool ShouldTriggerFor(CommandData commandData, string[] words, bool enabled) 
            => words.Any(word => word.EqualsIns(commandData.CommandText)) && enabled;

        private void TriggerDomainEvent(string charName, CommandData data)
        {
            if (data.Bits == 0 && Settings.AllowModBits && (data.IsMod || data.IsMe || data.IsBroadcaster))
            {
                int number = data.Arguments.Max(arg => arg.SafeIntParse());
                if (number > 0)
                {
                    data.Bits = number;
                }
            }

            if (data.Bits < 1)
            {
                _twitchClient.SendMessage(data.Channel, $"Be sure to include bits in your name bid, {data.Username}");
                return;
            }

            string newName = data.Arguments.FirstOrDefault() ?? "";

            var bidRecord = new BidRecord(data.Username, data.UserId, data.Bits);
            var domainEvent = new NameVoteReceived(charName, newName, bidRecord);
            DomainEvents.Raise(domainEvent);
        }
    }
}