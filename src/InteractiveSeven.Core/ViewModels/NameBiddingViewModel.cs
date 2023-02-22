using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Commands.Bidding.Naming;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Services;
using InteractiveSeven.Core.Settings;
using Serilog;

namespace InteractiveSeven.Core.ViewModels
{
    public partial class NameBiddingViewModel
    {
        private readonly INameAccessor _nameAccessor;
        private readonly IChatClient _chatClient;
        private readonly IDataStore<CharacterNameBid> _dataStore;
        private readonly IDialogService _dialogService;
        private readonly IStatusHubEmitter _statusHubEmitter;

        public ThreadedObservableCollection<CharacterNameBidding> CharacterNameBiddings { get; set; } = new ();

        public TwitchSettings TwitchSettings => TwitchSettings.Instance;

        public NameBiddingViewModel(INameAccessor nameAccessor, IChatClient chatClient,
            IDataStore<CharacterNameBid> dataStore, IDialogService dialogService,
            IStatusHubEmitter statusHubEmitter)
        {
            DomainEvents.Register<RemovingName>(HandleNameRemoval);
            DomainEvents.Register<NameVoteReceived>(HandleNameVote);
            DomainEvents.Register<TopNameChanged>(HandleTopNameChange);
            DomainEvents.Register<RefreshEvent>(HandleNameRefresh);

            _nameAccessor = nameAccessor;
            _chatClient = chatClient;
            _dataStore = dataStore;
            _dialogService = dialogService;
            _statusHubEmitter = statusHubEmitter;

            foreach (CharNames charName in CharNames.Core)
            {
                CharacterNameBiddings.Add(new CharacterNameBidding(charName));
            }
        }

        [RelayCommand]
        public void ResetData()
        {
            if (_dialogService.ConfirmDialog("Are you sure you want to reset all name bids?"))
            {
                Reset();
            }
        }

        public void Load(List<CharacterNameBid> nameBids)
        {
            CharacterNameBiddings.Clear();
            foreach (CharNames charName in CharNames.Core)
            {
                var nameBidding = new CharacterNameBidding(charName, false);

                var namesForChar = nameBids.Where(x => x.CharNameId == charName.Id)
                    .OrderByDescending(x => x.TotalBits);
                foreach (var nameBid in namesForChar)
                {
                    nameBidding.NameBids.Add(nameBid);
                }

                if (nameBidding.NameBids.Count == 0)
                {
                    nameBidding.AddDefaultRecord();
                }

                CharacterNameBiddings.Add(nameBidding);
            }
        }

        public void Reset()
        {
            CharacterNameBiddings.Clear();
            foreach (CharNames charName in CharNames.Core)
            {
                CharacterNameBiddings.Add(new CharacterNameBidding(charName));
            }
            _dataStore.SaveData(CharacterNameBiddings.SelectMany(cnb => cnb.NameBids).ToList());
            HandleNameRefresh(new RefreshEvent());
        }

        private void HandleNameRefresh(RefreshEvent e)
        {
            try
            {
                foreach (var nameBidding in CharacterNameBiddings)
                {
                    _nameAccessor.SetCharacterName(nameBidding.CharName, nameBidding.LeadingName);
                }
            }
            catch (Exception exception)
            {
                Log.Logger.Error(exception, "Failed to refresh names.");
            }
        }

        private async void HandleTopNameChange(TopNameChanged e)
        {
            try
            {
                _nameAccessor.SetCharacterName(e.CharName, e.NewName);
                string message = $"{e.CharName.DefaultName}'s name is now {e.NewName}.";
                await _chatClient.SendMessage(TwitchSettings.Channel, message);
                await _statusHubEmitter.ShowEvent(message);
            }
            catch (Exception exception)
            {
                Log.Logger.Error(exception, "Failed to update top name.");
            }
        }

        private void HandleNameVote(NameVoteReceived e)
        {
            try
            {
                CharacterNameBiddings.SingleOrDefault(x => x.CharName.Id == e.CharName.Id)?.HandleNameVote(e);
                _dataStore.SaveData(CharacterNameBiddings.SelectMany(cnb => cnb.NameBids).ToList());
            }
            catch (Exception exception)
            {
                Log.Logger.Error(exception, "Failed to record name vote and save.");
            }
        }

        private void HandleNameRemoval(RemovingName e)
        {
            try
            {
                foreach (var charBidding in CharacterNameBiddings)
                {
                    charBidding.TryRemove(e.NameToRemove);
                }
                _dataStore.SaveData(CharacterNameBiddings.SelectMany(cnb => cnb.NameBids).ToList());
            }
            catch (Exception exception)
            {
                Log.Logger.Error(exception, "Failed to remove Name.");
            }
        }
    }
}
