using InteractiveSeven.Core.Bidding;

namespace InteractiveSeven.Core.Events
{
    public class MoodVoteReceived : BaseDomainEvent
    {
        public BidRecord BidRecord { get; set; }
        public int MoodId { get; set; }

        public MoodVoteReceived(int moodId, BidRecord bidRecord)
        {
            MoodId = moodId;
            BidRecord = bidRecord;
        }
    }
}