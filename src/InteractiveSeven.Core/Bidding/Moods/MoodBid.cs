using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InteractiveSeven.Core.Bidding.Moods
{
    public class MoodBid : INotifyPropertyChanged
    {
        public int MoodId { get; }
        private int _totalBits;

        private readonly object _padlock = new object();

        public MoodBid(int moodId, int amount)
        {
            MoodId = moodId;
            _totalBits = amount;
        }

        public int TotalBits
        {
            get => _totalBits;
            set
            {
                _totalBits = value;
                OnPropertyChanged();
            }
        }

        public List<BidRecord> BidRecords { get; set; } = new List<BidRecord>();

        public int AddRecord(BidRecord record)
        {
            lock (_padlock)
            {
                BidRecords.Add(record);
                TotalBits += record.Bits;
                return TotalBits;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Reset()
        {
            lock (_padlock)
            {
                TotalBits = 0;
                BidRecords.Clear();
            }
        }
    }
}
