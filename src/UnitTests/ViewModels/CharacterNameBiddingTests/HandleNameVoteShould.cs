using FluentAssertions;
using InteractiveSeven.Core.Bidding;
using InteractiveSeven.Core.Bidding.Naming;
using InteractiveSeven.Core.Events;
using System.Linq;
using Xunit;

namespace UnitTests.ViewModels.CharacterNameBiddingTests
{
    public class HandleNameVoteShould
    {
        [Fact]
        public void UpdateLeadingName_GivenFirstBidAboveDefault()
        {
            var bidding = new CharacterNameBidding("Cloud");

            BidRecord bidRecord = new BidRecord("", "", bidding.NameBids.Single().TotalBits + 1);
            bidding.HandleNameVote(new NameVoteReceived("Cloud", "StabMan", bidRecord));

            bidding.LeadingName.Should().Be("StabMan");
        }
    }
}
