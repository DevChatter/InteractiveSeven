using System.Collections.Generic;
using System.Linq;
using InteractiveSeven.Core.Bidding;
using InteractiveSeven.Core.Bidding.Naming;
using ReactiveUI;

namespace InteractiveSeven.UI.ViewModels
{
    public class NameBidsViewModel : ReactiveObject
    {
        private readonly Dictionary<string, CharacterNameBid> _nameBids 
            = new Dictionary<string, CharacterNameBid>();

        public IReadOnlyList<CharacterNameBid> NameBids
            => _nameBids.Values.OrderByDescending(x => x.TotalBits).ToList();

        public void AddBid(string name, BidRecord bidRecord)
        {
            // TODO: Locking
            if (!_nameBids.TryGetValue(name, out CharacterNameBid nameBid))
            {
                nameBid = new CharacterNameBid {Name = name};
                _nameBids[name] = nameBid;
            }

            nameBid.AddRecord(bidRecord);
        }
    }
}