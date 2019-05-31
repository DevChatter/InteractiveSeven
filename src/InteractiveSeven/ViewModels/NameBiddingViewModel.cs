using InteractiveSeven.Core;
using InteractiveSeven.Core.Bidding.Naming;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.ViewModels
{
    public class NameBiddingViewModel
    {
        private readonly INameAccessor _nameAccessor;
        private readonly Dictionary<string, CharacterNameBidding> _characterNameBiddings 
            = new Dictionary<string, CharacterNameBidding>
            {
                [Constants.Cloud] = new CharacterNameBidding(Constants.Cloud),
                [Constants.Barret] = new CharacterNameBidding(Constants.Barret),
                [Constants.Tifa] = new CharacterNameBidding(Constants.Tifa),
                [Constants.Aeris] = new CharacterNameBidding(Constants.Aeris),
                [Constants.Red] = new CharacterNameBidding(Constants.Red),
                [Constants.CaitSith] = new CharacterNameBidding(Constants.CaitSith),
                [Constants.Cid] = new CharacterNameBidding(Constants.Cid),
                [Constants.Vincent] = new CharacterNameBidding(Constants.Vincent),
                [Constants.Yuffie] = new CharacterNameBidding(Constants.Yuffie),
            };

        public List<CharacterNameBidding> CharacterNameBiddings => _characterNameBiddings.Values.ToList();

        public NameBiddingViewModel(INameAccessor nameAccessor)
        {
            DomainEvents.Register<RemovingName>(HandleNameRemoval);
            DomainEvents.Register<NameVoteReceived>(HandleNameVote);

            DomainEvents.Register<TopNameChanged>(HandleTopNameChange);
            _nameAccessor = nameAccessor;
        }

        private void HandleTopNameChange(TopNameChanged e)
        {
            try
            {
                _nameAccessor.SetCharacterName(e.CharName, e.NewName);
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
