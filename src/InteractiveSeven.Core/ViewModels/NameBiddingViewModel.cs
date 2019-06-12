using InteractiveSeven.Core.Bidding.Naming;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Core.ViewModels
{
    public class NameBiddingViewModel
    {
        private readonly INameAccessor _nameAccessor;
        private readonly ITwitchClient _twitchClient;
        private readonly Dictionary<string, CharacterNameBidding> _characterNameBiddings
            = new Dictionary<string, CharacterNameBidding>
            {
                [CharNames.Cloud] = new CharacterNameBidding(CharNames.Cloud),
                [CharNames.Barret] = new CharacterNameBidding(CharNames.Barret),
                [CharNames.Tifa] = new CharacterNameBidding(CharNames.Tifa),
                [CharNames.Aeris] = new CharacterNameBidding(CharNames.Aeris),
                [CharNames.Red] = new CharacterNameBidding(CharNames.Red),
                [CharNames.CaitSith] = new CharacterNameBidding(CharNames.CaitSith),
                [CharNames.Cid] = new CharacterNameBidding(CharNames.Cid),
                [CharNames.Vincent] = new CharacterNameBidding(CharNames.Vincent),
                [CharNames.Yuffie] = new CharacterNameBidding(CharNames.Yuffie),
            };

        public List<CharacterNameBidding> CharacterNameBiddings => _characterNameBiddings.Values.ToList();

        public TwitchSettings TwitchSettings => ApplicationSettings.Instance.TwitchSettings;

        public NameBiddingViewModel(INameAccessor nameAccessor, ITwitchClient twitchClient)
        {
            DomainEvents.Register<RemovingName>(HandleNameRemoval);
            DomainEvents.Register<NameVoteReceived>(HandleNameVote);
            DomainEvents.Register<TopNameChanged>(HandleTopNameChange);
            DomainEvents.Register<RefreshEvent>(HandleNameRefresh);

            _nameAccessor = nameAccessor;
            _twitchClient = twitchClient;
        }

        private void HandleNameRefresh(RefreshEvent e)
        {
            try
            {
                foreach (var nameBidding in CharacterNameBiddings)
                {
                    _nameAccessor.SetCharacterName(nameBidding.DefaultName, nameBidding.LeadingName);
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
                _twitchClient.SendMessage(TwitchSettings.Channel, $"Interactive7: {e.CharName}'s name is now {e.NewName}.");
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
                _characterNameBiddings[e.CharName].HandleNameVote(e);
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
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
