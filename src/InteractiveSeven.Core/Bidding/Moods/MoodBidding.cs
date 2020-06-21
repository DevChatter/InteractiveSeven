using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Moods;
using InteractiveSeven.Core.Settings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace InteractiveSeven.Core.Bidding.Moods
{
    public class MoodBidding : INotifyPropertyChanged
    {
        private readonly ILogger<MoodBidding> _logger;
        public List<Mood> Moods { get; }
        public int DefaultMoodId { get; }

        public ThreadedObservableCollection<MoodBid> MoodBids { get; }
            = new ThreadedObservableCollection<MoodBid>();

        private readonly object _padlock = new object();

        private int GetHighestBid() => MoodBids.OrderByDescending(x => x.TotalBits)
                  .FirstOrDefault()?.MoodId ?? DefaultMoodId;

        private string _leadingName;
        public string LeadingName
        {
            get => _leadingName;
            set
            {
                _leadingName = value;
                OnPropertyChanged();
                DomainEvents.Raise(new TopNameChanged(CharName, LeadingName));
            }
        }

        private NameBiddingSettings Settings => ApplicationSettings.Instance.NameBiddingSettings;

        public MoodBidding(List<Mood> moods, ILogger<MoodBidding> logger)
        {
            _logger = logger;

            MoodBids.CollectionChanged += NameBids_CollectionChanged;

            AddDefaultRecord();
        }

        private void AddDefaultRecord()
        {
            MoodBids.Add(new MoodBid(NormalMood.DefaultId));
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
            int moodId = GetHighestBid();
            if (LeadingName != highestBid)
            {
                LeadingName = highestBid;
            }
        }

        public void HandleNameVote(MoodVoteReceived e)
        {
            try
            {
                MoodBid nameBid = MoodBids.SingleOrDefault(bid => bid.MoodId == e.MoodId);
                if (nameBid == null)
                {
                    lock (_padlock)
                    {
                        nameBid = MoodBids.SingleOrDefault(bid => bid.MoodId == e.MoodId);
                        if (nameBid == null)
                        {
                            nameBid = new MoodBid(e.MoodId);
                            MoodBids.Add(nameBid);
                        }
                    }
                }

                nameBid.AddRecord(e.BidRecord);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to record name bid.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
