using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Moods;
using InteractiveSeven.Core.Settings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Bidding.Moods
{
    public class MoodBidding
    {
        private readonly ILogger<MoodBidding> _logger;
        public List<Mood> Moods { get; }
        public int DefaultMoodId { get; }

        public ThreadedObservableCollection<MoodBid> MoodBids { get; }
            = new ThreadedObservableCollection<MoodBid>();

        private readonly object _padlock = new object();

        private int GetHighestBid() => MoodBids.OrderByDescending(x => x.TotalBits)
                  .FirstOrDefault()?.MoodId ?? DefaultMoodId;

        private NameBiddingSettings Settings => ApplicationSettings.Instance.NameBiddingSettings;

        public MoodBidding(List<Mood> moods, ILogger<MoodBidding> logger)
        {
            _logger = logger;

            AddDefaultRecord();
        }

        private void AddDefaultRecord()
        {
            MoodBids.Add(new MoodBid(NormalMood.DefaultId));
        }

        public void AddBid(MoodBid moodBid)
        {
            MoodBids.Add(moodBid);
        }
    }
}
