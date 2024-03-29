﻿using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Settings;
using Serilog;

namespace InteractiveSeven.Core.Commands.Bidding.Naming
{
    public class CharacterNameBidding : INotifyPropertyChanged
    {
        public CharNames CharName { get; }
        public string DefaultName { get; }

        public ThreadedObservableCollection<CharacterNameBid> NameBids { get; } = new();

        private readonly object _padlock = new object();

        private string GetHighestBid() => NameBids.MaxBy(x => x.TotalBits)?.Name ?? DefaultName;

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

        public CharacterNameBidding(CharNames charNames, bool withDefault = true)
        {
            CharName = charNames;
            DefaultName = charNames.DefaultName;
            _leadingName = charNames.DefaultName;

            NameBids.CollectionChanged += NameBids_CollectionChanged;

            if (withDefault)
            {
                AddDefaultRecord();
            }
        }

        public void AddDefaultRecord()
        {
            NameBids.Add(new CharacterNameBid(CharName.Id) { Name = DefaultName, TotalBits = Settings.DefaultStartBits });
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
            var highestBid = GetHighestBid();
            if (LeadingName != highestBid)
            {
                LeadingName = highestBid;
            }
        }

        public void HandleNameVote(NameVoteReceived e)
        {
            try
            {
                var nameBid = NameBids.SingleOrDefault(bid => bid.Name == e.BidName);
                if (nameBid == null)
                {
                    lock (_padlock)
                    {
                        nameBid = NameBids.SingleOrDefault(bid => bid.Name == e.BidName);
                        if (nameBid == null)
                        {
                            nameBid = new CharacterNameBid(e.CharName.Id) { Name = e.BidName };
                            NameBids.Add(nameBid);
                        }
                    }
                }

                nameBid.AddRecord(e.BidRecord);
            }
            catch (Exception exception)
            {
                Log.Logger.Error(exception, "Failed to record name bid.");
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
