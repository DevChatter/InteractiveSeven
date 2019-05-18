using DynamicData;
using InteractiveSeven.Core;
using InteractiveSeven.Core.Bidding;
using InteractiveSeven.Core.Events;
using ReactiveUI;
using System;
using System.Linq;

namespace InteractiveSeven.UI.ViewModels
{
    public class NameBidsViewModel : ReactiveObject
    {
        SourceList<CharacterNameBid> _nameBids = new SourceList<CharacterNameBid>();
        public SourceList<CharacterNameBid> NameBids
        {
            get => _nameBids;
            set => this.RaiseAndSetIfChanged(ref _nameBids, value);
        }

        private string _defaultName;

        public string DefaultName
        {
            get => _defaultName;
            set => this.RaiseAndSetIfChanged(ref _defaultName, value);
        }

        public string LeadingName => NameBids.Items
                                         .OrderByDescending(x => x.TotalBits)
                                         .FirstOrDefault()?.Name ?? DefaultName;


        public NameBidsViewModel SetName(string name)
        {
            DefaultName = name;
            return this;
        }

        public void AddBid(string name, BidRecord bidRecord)
        {
            string currentName = LeadingName;
            // TODO: Locking
            CharacterNameBid nameBid = _nameBids.Items.SingleOrDefault(x => x.Name == name);
            if (nameBid == null)
            {
                nameBid = new CharacterNameBid {Name = name};
                _nameBids.Add(nameBid);
            }

            nameBid.AddRecord(bidRecord);

            if (LeadingName != currentName)
            {
                var topNameChanged = new TopNameChanged(DefaultName, LeadingName);
                DomainEvents.Raise(topNameChanged);
            }
        }

        public void HandleNameVote(NameVoteReceived e)
        {
            try
            {
                if (DefaultName.EqualsIns(e.CharName))
                {
                    AddBid(e.BidName, e.BidRecord);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

    }
}