using InteractiveSeven.Core.Bidding;
using InteractiveSeven.Core.Data;

namespace InteractiveSeven.Core.Events
{
    public class NameVoteReceived : BaseDomainEvent
    {
        public BidRecord BidRecord { get; set; }
        public CharNames CharName { get; set; }
        public string BidName { get; set; }

        public NameVoteReceived(CharNames charName, string bidName, BidRecord bidRecord)
        {
            CharName = charName;
            BidName = bidName;
            BidRecord = bidRecord;
        }
    }
}