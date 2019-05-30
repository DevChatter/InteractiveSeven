using InteractiveSeven.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Bidding.Naming
{
    public class CharacterNameBidding
    {
        public string DefaultName { get; }

        private readonly Dictionary<string, CharacterNameBid> _nameBids = new Dictionary<string, CharacterNameBid>();
        public string LeadingName => _nameBids.Values
                                         .OrderByDescending(x => x.TotalBits)
                                         .FirstOrDefault()?.Name ?? DefaultName;

        public CharacterNameBidding(string defaultName)
        {
            DefaultName = defaultName;
        }

        public void HandleNameVote(NameVoteReceived e)
        {
            try
            {
                string currentName = LeadingName;
                // TODO: Locking
                if (!_nameBids.TryGetValue(e.BidName, out CharacterNameBid nameBid))
                {
                    nameBid = new CharacterNameBid { Name = e.BidName };
                    _nameBids.Add(e.BidName, nameBid);
                }

                nameBid.AddRecord(e.BidRecord);

                if (LeadingName != currentName)
                {
                    var topNameChanged = new TopNameChanged(DefaultName, LeadingName);
                    DomainEvents.Raise(topNameChanged);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }
    }
}
