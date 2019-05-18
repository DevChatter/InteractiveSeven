using InteractiveSeven.Core.Bidding;
using ReactiveUI;
using System.Collections.Generic;

namespace InteractiveSeven.UI.ViewModels
{
    public class CharacterNameBid : ReactiveObject
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private int _totalBits;
        public int TotalBits
        {
            get => _totalBits;
            set => this.RaiseAndSetIfChanged(ref _totalBits, value);
        }

        public List<BidRecord> BidRecords { get; set; } = new List<BidRecord>();

        public void AddRecord(BidRecord record)
        {
            // TODO: Locking
            BidRecords.Add(record);
            TotalBits += record.Bits;
        }
    }
}