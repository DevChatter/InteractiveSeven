using InteractiveSeven.Core.Model;
using InteractiveSeven.Core.Moods;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Settings;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Bidding.Moods
{
    public class MoodBidding
    {
        private readonly GilBank _gilBank;
        private readonly ILogger<MoodBidding> _logger;

        public ThreadedObservableCollection<MoodBid> MoodBids { get; }
            = new ThreadedObservableCollection<MoodBid>();

        public int GetTopMoodId() => MoodBids.OrderByDescending(x => x.TotalBits).First().MoodId;

        private MoodSettings Settings => ApplicationSettings.Instance.MoodSettings;

        public MoodBidding(GilBank gilBank, ILogger<MoodBidding> logger)
        {
            _gilBank = gilBank;
            _logger = logger;

            AddDefaultRecords();
        }

        private void AddDefaultRecords()
        {
            MoodBids.Add(new MoodBid(NormalMood.DefaultId, Settings.DefaultNormalBid));
            MoodBids.Add(new MoodBid(DangerMood.DefaultId, 0));
            MoodBids.Add(new MoodBid(PeacefulMood.DefaultId, 0));
        }

        public void ResetBids(int topMoodId)
        {
            List<BidRecord> refunds = new List<BidRecord>();
            foreach (MoodBid nonWinner in MoodBids.Where(x => x.MoodId != topMoodId))
            {
                refunds.AddRange(nonWinner.BidRecords);
            }
            MoodBids.Clear();
            AddDefaultRecords();

            SendRefunds(refunds);
        }

        private void SendRefunds(List<BidRecord> refunds)
        {
            var userTotals = refunds.GroupBy(x => x.UserId,
                (userId, records) => (UserId: userId, Total: records.Sum(x => x.Bits)));

            foreach ((string userId, int total) in userTotals)
            {
                _gilBank.Deposit(new ChatUser(null, userId), total);
            }
        }

        public int AddBid(int moodId, BidRecord bid)
        {
            return MoodBids.Single(x => x.MoodId == moodId).AddRecord(bid);
        }
    }
}
