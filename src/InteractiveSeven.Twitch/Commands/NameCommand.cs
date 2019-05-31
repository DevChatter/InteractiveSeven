using System;
using InteractiveSeven.Core;
using InteractiveSeven.Twitch.Model;
using System.Linq;
using InteractiveSeven.Core.Bidding;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Twitch.Commands
{
    public class NameCommand : BaseCommand
    {
        private static readonly string[] CloudWords = {"cloud", "cluod", "clodu"};
        private static readonly string[] BarretWords = {"barret", "baret", "barett", "barrett"};
        private static readonly string[] TifaWords = {"tifa", "tiaf", "tfia"};
        private static readonly string[] AerisWords = {"aeris", "aerith"};
        private static readonly string[] CaitWords = {"caitsith"};
        private static readonly string[] CidWords = {"cid"};
        private static readonly string[] RedWords = {"red", "redxiii", "nanaki", "redxii", "redxiiii"};
        private static readonly string[] VincentWords = {"vincent", "vince"};
        private static readonly string[] YuffieWords = {"yuffie"};
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

        public NameCommand()
            : base(AllWords)
        {
        }

        public override void Execute(CommandData commandData)
        {
            if (!Settings.Enabled) return;

            if (CloudWords.Any(word => word.EqualsIns(commandData.CommandText)) && Settings.NamingCloudEnabled)
            {
                TriggerDomainEvent(Constants.Cloud, commandData);
            }
            else if (BarretWords.Any(word => word.EqualsIns(commandData.CommandText)) && Settings.NamingBarretEnabled)
            {
                TriggerDomainEvent(Constants.Barret, commandData);
            }
            else if (TifaWords.Any(word => word.EqualsIns(commandData.CommandText)) && Settings.NamingTifaEnabled)
            {
                TriggerDomainEvent(Constants.Tifa, commandData);
            }
            else if (AerisWords.Any(word => word.EqualsIns(commandData.CommandText)) && Settings.NamingAerisEnabled)
            {
                TriggerDomainEvent(Constants.Aeris, commandData);
            }
            else if (CaitWords.Any(word => word.EqualsIns(commandData.CommandText)) && Settings.NamingCaitSithEnabled)
            {
                TriggerDomainEvent(Constants.CaitSith, commandData);
            }
            else if (CidWords.Any(word => word.EqualsIns(commandData.CommandText)) && Settings.NamingCidEnabled)
            {
                TriggerDomainEvent(Constants.Cid, commandData);
            }
            else if (RedWords.Any(word => word.EqualsIns(commandData.CommandText)) && Settings.NamingRedEnabled)
            {
                TriggerDomainEvent(Constants.Red, commandData);
            }
            else if (VincentWords.Any(word => word.EqualsIns(commandData.CommandText)) && Settings.NamingVincentEnabled)
            {
                TriggerDomainEvent(Constants.Vincent, commandData);
            }
            else if (YuffieWords.Any(word => word.EqualsIns(commandData.CommandText)) && Settings.NamingYuffieEnabled)
            {
                TriggerDomainEvent(Constants.Yuffie, commandData);
            }
        }

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
                // TODO: Send Message
                return;
            }

            string newName = data.Arguments.FirstOrDefault() ?? "";

            var bidRecord = new BidRecord(data.Username, data.UserId, data.Bits);
            var domainEvent = new NameVoteReceived(charName, newName, bidRecord);
            DomainEvents.Raise(domainEvent);
        }
    }
}