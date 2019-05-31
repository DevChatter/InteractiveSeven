using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InteractiveSeven.Core.Bidding.Naming
{
    public class CharacterNameBid : INotifyPropertyChanged
    {
        private string name;
        private int totalBits;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public int TotalBits
        {
            get => totalBits;
            set
            {
                totalBits = value;
                OnPropertyChanged();
            }
        }
        public List<BidRecord> BidRecords { get; set; } = new List<BidRecord>();

        public void AddRecord(BidRecord record)
        {
            // TODO: Locking
            BidRecords.Add(record);
            TotalBits += record.Bits;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
