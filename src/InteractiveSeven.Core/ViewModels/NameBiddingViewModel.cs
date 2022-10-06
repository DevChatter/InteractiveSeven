using InteractiveSeven.Core.Bidding.Naming;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.MvvmCommands;
using InteractiveSeven.Core.Services;
using InteractiveSeven.Core.Settings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Core.ViewModels
{
    public class NameBiddingViewModel
    {
        private readonly INameAccessor _nameAccessor;
        private readonly ITwitchClient _twitchClient;
        private readonly IDataStore<CharacterNameBid> _dataStore;
        private readonly IDialogService _dialogService;
        //private readonly IStatusHubEmitter _statusHubEmitter;
        private readonly ILogger<NameBiddingViewModel> _logger;
        private readonly ILogger<CharacterNameBidding> _charNameBiddingLogger;

        public ThreadedObservableCollection<CharacterNameBidding> CharacterNameBiddings { get; set; }
            = new ThreadedObservableCollection<CharacterNameBidding>();

        public TwitchSettings TwitchSettings => TwitchSettings.Instance;

        public NameBiddingViewModel(INameAccessor nameAccessor, ITwitchClient twitchClient,
            IDataStore<CharacterNameBid> dataStore, IDialogService dialogService,
            //IStatusHubEmitter statusHubEmitter,
            ILogger<NameBiddingViewModel> logger,
            ILogger<CharacterNameBidding> charNameBiddingLogger)
        {
            DomainEvents.Register<RemovingName>(HandleNameRemoval);
            DomainEvents.Register<NameVoteReceived>(HandleNameVote);
            DomainEvents.Register<TopNameChanged>(HandleTopNameChange);
            DomainEvents.Register<RefreshEvent>(HandleNameRefresh);

            _nameAccessor = nameAccessor;
            _twitchClient = twitchClient;
            _dataStore = dataStore;
            _dialogService = dialogService;
            //_statusHubEmitter = statusHubEmitter;
            _logger = logger;
            _charNameBiddingLogger = charNameBiddingLogger;

            ResetDataCommand = new SimpleCommand(x =>
            {
                if (_dialogService.ConfirmDialog("Are you sure you want to reset all name bids?"))
                {
                    Reset();
                }
            });

            foreach (CharNames charName in CharNames.Core)
            {
                CharacterNameBiddings.Add(new CharacterNameBidding(charName, _charNameBiddingLogger));
            }
        }

        public ICommand ResetDataCommand { get; }

        public void Load(List<CharacterNameBid> nameBids)
        {
            CharacterNameBiddings.Clear();
            foreach (CharNames charName in CharNames.Core)
            {
                var nameBidding = new CharacterNameBidding(charName, _charNameBiddingLogger, false);

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
                CharacterNameBiddings.Add(new CharacterNameBidding(charName, _charNameBiddingLogger));
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
                _logger.LogError(exception, "Failed to refresh names.");
            }
        }

        private void HandleTopNameChange(TopNameChanged e)
        {
            try
            {
                _nameAccessor.SetCharacterName(e.CharName, e.NewName);
                string message = $"{e.CharName.DefaultName}'s name is now {e.NewName}.";
                _twitchClient.SendMessage(TwitchSettings.Channel, message);
                //_statusHubEmitter.ShowEvent(message);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to update top name.");
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
                _logger.LogError(exception, "Failed to record name vote and save.");
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
                _logger.LogError(exception, "Failed to remove Name.");
            }
        }
    }
}
