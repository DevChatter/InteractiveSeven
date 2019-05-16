using System;
using InteractiveSeven.Core;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Memory;
using ReactiveUI;
using System.Collections.Generic;

namespace InteractiveSeven.UI.ViewModels
{
    public class NameBiddingViewModel : ReactiveObject
    {
        private readonly INameAccessor _nameAccessor;
        public Dictionary<string,NameBidsViewModel> BidsViewModels { get; }

        public NameBiddingViewModel(NameBidsViewModel cloudBids,
            NameBidsViewModel barretBids,
            NameBidsViewModel tifaBids,
            NameBidsViewModel aerisBids,
            NameBidsViewModel redBids,
            NameBidsViewModel caitSithBids,
            NameBidsViewModel vincentBids,
            NameBidsViewModel cidBids,
            NameBidsViewModel yuffieBids,
            INameAccessor nameAccessor
            )
        {
            _nameAccessor = nameAccessor;
            BidsViewModels = new Dictionary<string, NameBidsViewModel>
            {
                [Constants.Cloud] = cloudBids,
                [Constants.Barret] = barretBids,
                [Constants.Tifa] = tifaBids,
                [Constants.Aeris] = aerisBids,
                [Constants.Red] = redBids,
                [Constants.CaitSith] = caitSithBids,
                [Constants.Vincent] = vincentBids,
                [Constants.Cid] = cidBids,
                [Constants.Yuffie] = yuffieBids,
            };
        }

        public void HandleNameVote(NameVoteReceived e)
        {
            try
            {
                BidsViewModels[e.CharName].AddBid(e.BidName, e.BidRecord);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void HandleNameChange(TopNameChanged e)
        {
            try
            {
                _nameAccessor.SetCharacterName(e.CharName, e.NewName);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
