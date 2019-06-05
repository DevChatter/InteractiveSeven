using InteractiveSeven.Core;
using InteractiveSeven.Core.ViewModels;
using InteractiveSeven.Twitch.Model;
using System;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class NameBidsCommand : BaseCommand
    {
        private readonly NameBiddingViewModel _biddingVm;
        private readonly ITwitchClient _twitchClient;

        public NameBidsCommand(NameBiddingViewModel nameBiddingViewModel, ITwitchClient twitchClient)
            : base(new[] { "NameBids" })
        {
            _biddingVm = nameBiddingViewModel;
            _twitchClient = twitchClient;
        }

        public override void Execute(CommandData commandData)
        {
            var requested = commandData.Arguments.FirstOrDefault();

            var bidding = _biddingVm.CharacterNameBiddings.SingleOrDefault(x => x.DefaultName.EqualsIns(requested));

            if (bidding == null)
            {
                _twitchClient.SendMessage(commandData.Channel, "Specify a character to see the name bids.");
                return;
            }

            var values = bidding.NameBids.OrderByDescending(x => x.TotalBits).Take(5).Select(x => $"({x.Name} {x.TotalBits})");
            string message = string.Join(", ", values);

            _twitchClient.SendMessage(commandData.Channel, $"{bidding.DefaultName} Bids: {message}");
        }
    }
}