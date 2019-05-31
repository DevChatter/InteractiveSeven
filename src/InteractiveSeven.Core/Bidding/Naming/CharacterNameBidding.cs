using InteractiveSeven.Core.Events;
using System;
using System.Collections.Specialized;
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

        private string GetHighestBid() => NameBids.OrderByDescending(x => x.TotalBits)
                  .FirstOrDefault()?.Name ?? DefaultName;

        private string _leadingName;
        public string LeadingName
        {
            get => _leadingName;
            set
            {
                _leadingName = value;
                OnPropertyChanged();
                DomainEvents.Raise(new TopNameChanged(DefaultName, LeadingName));
            }
        }

        public CharacterNameBidding(string defaultName)
        {
            DefaultName = defaultName;
            _leadingName = defaultName;

            NameBids.CollectionChanged += NameBids_CollectionChanged;
        }

        private void NameBids_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
                foreach (INotifyPropertyChanged oldItem in e.OldItems)
                    oldItem.PropertyChanged -= CharacterNameBidding_PropertyChanged;

            if (e.NewItems != null)
                foreach (INotifyPropertyChanged newItem in e.NewItems)
                    newItem.PropertyChanged += CharacterNameBidding_PropertyChanged;

            CheckNameChange();
        }

        private void CharacterNameBidding_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CheckNameChange();
        }

        private void CheckNameChange()
        {
            string highestBid = GetHighestBid();
            if (LeadingName != highestBid)
            {
                LeadingName = highestBid;
            }
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
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void TryRemove(string nameToRemove)
        {
            var nameBidToRemove = NameBids.SingleOrDefault(x => x.Name == nameToRemove);
            if (nameBidToRemove != null)
            {
                NameBids.Remove(nameBidToRemove);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
