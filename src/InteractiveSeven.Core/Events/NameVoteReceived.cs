using InteractiveSeven.Core.Bidding;

namespace InteractiveSeven.Core.Events
{
    public class NameVoteReceived : BaseDomainEvent
    {
        public BidRecord BidRecord { get; set; }
        public string CharName { get; set; }
        public string BidName { get; set; }

        public NameVoteReceived(string charName, string bidName, BidRecord bidRecord)
        {
            CharName = charName;
            BidName = bidName;
            BidRecord = bidRecord;
        }
    }
}