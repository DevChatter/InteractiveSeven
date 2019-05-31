using InteractiveSeven.Core.Bidding;
using InteractiveSeven.Core.Bidding.Naming;
using InteractiveSeven.Core.Events;
using Xunit;

namespace UnitTests.ViewModels.CharacterNameBiddingTests
{
    public class HandleNameVoteShould
    {
        [Fact]
        public void UpdateLeadingName_GivenFirstBid()
        {
            var bidding = new CharacterNameBidding("Cloud");

            bidding.HandleNameVote(new NameVoteReceived("Cloud", "StabMan", new BidRecord("", "", 10)));

            Assert.Equal("StabMan", bidding.LeadingName);
        }
    }
}
