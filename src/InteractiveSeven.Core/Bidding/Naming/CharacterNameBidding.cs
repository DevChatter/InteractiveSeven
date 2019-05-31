using InteractiveSeven.Core.Events;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace InteractiveSeven.Core.Bidding.Naming
{
    public class CharacterNameBidding : INotifyPropertyChanged
    {
        public string DefaultName { get; }

        public ThreadedObservableCollection<CharacterNameBid> NameBids { get; }
            = new ThreadedObservableCollection<CharacterNameBid>();
        public string LeadingName => NameBids.OrderByDescending(x => x.TotalBits)
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
                CharacterNameBid nameBid = NameBids.SingleOrDefault(bid => bid.Name == e.BidName);
                if (nameBid == null)
                {
                    nameBid = new CharacterNameBid { Name = e.BidName };
                    NameBids.Add(nameBid);
                }

                nameBid.AddRecord(e.BidRecord);

                if (LeadingName != currentName)
                {
                    var topNameChanged = new TopNameChanged(DefaultName, LeadingName);
                    DomainEvents.Raise(topNameChanged);
                    OnPropertyChanged(nameof(LeadingName));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
