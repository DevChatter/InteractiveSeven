using System.Collections.Generic;

namespace InteractiveSeven.Core.Bidding.Naming
{
    public class CharacterNameBid
    {
        public string Name { get; set; }
        public int TotalBits { get; set; }
        public List<BidRecord> BidRecords { get; set; } = new List<BidRecord>();

        public void AddRecord(BidRecord record)
        {
            // TODO: Locking
            BidRecords.Add(record);
            TotalBits += record.Bits;
        }

    }
}
