using System.Collections.Generic;
using System.Linq;
using InteractiveSeven.Core.Bidding;
using InteractiveSeven.Core.Bidding.Naming;
using InteractiveSeven.Core.Events;
using ReactiveUI;

namespace InteractiveSeven.UI.ViewModels
{
    public class NameBidsViewModel : ReactiveObject
    {
        private readonly Dictionary<string, CharacterNameBid> _nameBids 
            = new Dictionary<string, CharacterNameBid>();

        private string _default;

        public string LeadingName => NameBids.FirstOrDefault()?.Name ?? _default;

        public IReadOnlyList<CharacterNameBid> NameBids
            => _nameBids.Values.OrderByDescending(x => x.TotalBits).ToList();

        public NameBidsViewModel SetName(string name)
        {
            _default = name;
            return this;
        }

        public void AddBid(string name, BidRecord bidRecord)
        {
            string currentName = LeadingName;
            // TODO: Locking
            if (!_nameBids.TryGetValue(name, out CharacterNameBid nameBid))
            {
                nameBid = new CharacterNameBid {Name = name};
                _nameBids[name] = nameBid;
            }

            nameBid.AddRecord(bidRecord);

            if (LeadingName != currentName)
            {
                var topNameChanged = new TopNameChanged(_default, LeadingName);
                DomainEvents.Raise(topNameChanged);
            }
        }
    }
}