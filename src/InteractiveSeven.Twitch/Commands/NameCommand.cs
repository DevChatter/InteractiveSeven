using System;
using InteractiveSeven.Core;
using InteractiveSeven.Twitch.Model;
using System.Linq;
using InteractiveSeven.Core.Bidding;
using InteractiveSeven.Core.Events;

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

        public NameCommand()
            : base(AllWords)
        {
        }

        public override void Execute(CommandData commandData)
        {
            if (CloudWords.Any(word => word.EqualsIns(commandData.CommandText)))
            {
                TriggerDomainEvent(Constants.Cloud, commandData);
            }
            else if (BarretWords.Any(word => word.EqualsIns(commandData.CommandText)))
            {
                TriggerDomainEvent(Constants.Barret, commandData);
            }
            else if (TifaWords.Any(word => word.EqualsIns(commandData.CommandText)))
            {
                TriggerDomainEvent(Constants.Tifa, commandData);
            }
            else if (AerisWords.Any(word => word.EqualsIns(commandData.CommandText)))
            {
                TriggerDomainEvent(Constants.Aeris, commandData);
            }
            else if (CaitWords.Any(word => word.EqualsIns(commandData.CommandText)))
            {
                TriggerDomainEvent(Constants.CaitSith, commandData);
            }
            else if (CidWords.Any(word => word.EqualsIns(commandData.CommandText)))
            {
                TriggerDomainEvent(Constants.Cid, commandData);
            }
            else if (RedWords.Any(word => word.EqualsIns(commandData.CommandText)))
            {
                TriggerDomainEvent(Constants.Red, commandData);
            }
            else if (VincentWords.Any(word => word.EqualsIns(commandData.CommandText)))
            {
                TriggerDomainEvent(Constants.Vincent, commandData);
            }
            else if (YuffieWords.Any(word => word.EqualsIns(commandData.CommandText)))
            {
                TriggerDomainEvent(Constants.Yuffie, commandData);
            }
            else
            {
                // TODO: Handle Unknown Character Vote
            }
        }

        private void TriggerDomainEvent(string charName, CommandData data)
        {
            // TODO: Remove this!!!
            // Temp Code!!!
            data.Bits = Math.Max(data.Bits, 1);

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