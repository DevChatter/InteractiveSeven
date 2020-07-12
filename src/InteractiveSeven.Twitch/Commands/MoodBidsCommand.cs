using InteractiveSeven.Core.Bidding.Moods;
using InteractiveSeven.Core.Moods;
using InteractiveSeven.Twitch.Model;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class MoodBidsCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;
        private readonly MoodBidding _moodBidding;
        private readonly IList<Mood> _moods;

        public MoodBidsCommand(ITwitchClient twitchClient, MoodBidding moodBidding, IList<Mood> moods)
            : base(x => x.MoodBidsCommandWords, x => x.MoodSettings.Enabled)
        {
            _twitchClient = twitchClient;
            _moodBidding = moodBidding;
            _moods = moods;
        }

        public override void Execute(in CommandData commandData)
        {
            var bidTotals = _moodBidding.MoodBids
                .Join(_moods, b => b.MoodId, m => m.Id, (bid, mood) => (bid, mood));

            var bidMessages = bidTotals.OrderByDescending(x => x.bid.TotalBits)
                .Select((pair, i) => $"{i + 1}: {pair.mood.Name}({pair.bid.TotalBits})");

            string message = $"Current Bids ({string.Join(", ", bidMessages)})";

            _twitchClient.SendMessage(commandData.Channel, message);
        }
    }
}