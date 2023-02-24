using System.Linq;
using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.ViewModels;

namespace InteractiveSeven.Core.Commands.Bidding
{
    public class NameBidsCommand : BaseCommand
    {
        private readonly NameBiddingViewModel _biddingVm;

        public NameBidsCommand(NameBiddingViewModel nameBiddingViewModel)
            : base(x => x.NameBidsCommandWords, x => x.NameBiddingSettings.Enabled)
        {
            _biddingVm = nameBiddingViewModel;
        }

        public override async Task Execute(CommandData commandData, IChatClient chatClient)
        {
            var requested = commandData.Arguments.FirstOrDefault();

            var (exists, name) = CharNames.GetByName(requested);

            if (!exists)
            {
                await chatClient.SendMessage(commandData.Channel, "Specify a character to see the name bids.");
                return;
            }

            var bidding = _biddingVm.CharacterNameBiddings.SingleOrDefault(x => x.CharName == name);

            if (bidding == null)
            {
                await chatClient.SendMessage(commandData.Channel, "Specify a character to see the name bids.");
                return;
            }

            var values = bidding.NameBids.OrderByDescending(x => x.TotalBits).Take(5).Select(x => $"({x.Name} {x.TotalBits})");
            string message = string.Join(", ", values);

            await chatClient.SendMessage(commandData.Channel, $"{bidding.DefaultName} Bids: {message}");
        }
    }
}
