using System.Linq;
using FluentAssertions;
using InteractiveSeven.Core.Commands.Bidding;
using InteractiveSeven.Core.Commands.Bidding.Naming;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Events;
using Xunit;

namespace UnitTests.ViewModels.CharacterNameBiddingTests
{
    public class HandleNameVoteShould
    {
        [Fact]
        public void UpdateLeadingName_GivenFirstBidAboveDefault()
        {
            var bidding = new CharacterNameBidding(CharNames.Cloud, null);

            BidRecord bidRecord = new BidRecord("", "", bidding.NameBids.Single().TotalBits + 1);
            bidding.HandleNameVote(new NameVoteReceived(CharNames.Cloud, "StabMan", bidRecord));

            bidding.LeadingName.Should().Be("StabMan");
        }
    }
}
