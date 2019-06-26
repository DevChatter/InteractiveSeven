using InteractiveSeven.Core.Bidding.Naming;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using InteractiveSeven.Core.MvvmCommands;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Core.ViewModels
{
    public class NameBiddingViewModel
    {
        private readonly INameAccessor _nameAccessor;
        private readonly ITwitchClient _twitchClient;
        private readonly IDataStore _dataStore;

        public ThreadedObservableCollection<CharacterNameBidding> CharacterNameBiddings { get; set; }
            = new ThreadedObservableCollection<CharacterNameBidding>();

        public TwitchSettings TwitchSettings => ApplicationSettings.Instance.TwitchSettings;

        public NameBiddingViewModel(INameAccessor nameAccessor, ITwitchClient twitchClient, IDataStore dataStore)
        {
            DomainEvents.Register<RemovingName>(HandleNameRemoval);
            DomainEvents.Register<NameVoteReceived>(HandleNameVote);
            DomainEvents.Register<TopNameChanged>(HandleTopNameChange);
            DomainEvents.Register<RefreshEvent>(HandleNameRefresh);

            _nameAccessor = nameAccessor;
            _twitchClient = twitchClient;
            _dataStore = dataStore;

            ResetDataCommand = new SimpleCommand(x => Reset());

            foreach (CharNames charName in CharNames.All)
            {
                CharacterNameBiddings.Add(new CharacterNameBidding(charName));
            }
        }

        public ICommand ResetDataCommand { get; }

        public void Load(List<CharacterNameBid> nameBids)
        {
            CharacterNameBiddings.Clear();
            foreach (CharNames charName in CharNames.All)
            {
                var nameBidding = new CharacterNameBidding(charName, false);

                foreach (var nameBid in nameBids.Where(x => x.CharNameId == charName.Id))
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
            foreach (CharNames charName in CharNames.All)
            {
                CharacterNameBiddings.Add(new CharacterNameBidding(charName));
            }
            _dataStore.SaveData(CharacterNameBiddings.SelectMany(cnb => cnb.NameBids).ToList());
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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void HandleTopNameChange(TopNameChanged e)
        {
            try
            {
                _nameAccessor.SetCharacterName(e.CharName, e.NewName);
                _twitchClient.SendMessage(TwitchSettings.Channel,
                    $"I7: {e.CharName.DefaultName}'s name is now {e.NewName}.");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
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
                Console.WriteLine(exception);
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
                Console.WriteLine(exception);
            }
        }
    }
}
