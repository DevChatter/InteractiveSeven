using InteractiveSeven.Core.Moods;
using InteractiveSeven.Core.Settings;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace InteractiveSeven.Core.Bidding.Moods
{
    public class MoodBidding
    {
        private readonly ILogger<MoodBidding> _logger;

        public ThreadedObservableCollection<MoodBid> MoodBids { get; }
            = new ThreadedObservableCollection<MoodBid>();

        private int GetTopMoodId() => MoodBids.OrderByDescending(x => x.TotalBits).First().MoodId;

        private MoodSettings Settings => ApplicationSettings.Instance.MoodSettings;

        public MoodBidding(ILogger<MoodBidding> logger)
        {
            _logger = logger;

            AddDefaultRecords();
        }

        private void AddDefaultRecords()
        {
            MoodBids.Add(new MoodBid(NormalMood.DefaultId, Settings.DefaultNormalBid));
            MoodBids.Add(new MoodBid(DangerMood.DefaultId, 0));
            MoodBids.Add(new MoodBid(PeacefulMood.DefaultId, 0));
        }

        public void ResetBids()
        {
            MoodBids.Clear();
            AddDefaultRecords();
        }

        public int AddBid(int moodId, BidRecord bid)
        {
            return MoodBids.Single(x => x.MoodId == moodId).AddRecord(bid);
        }
    }
}
