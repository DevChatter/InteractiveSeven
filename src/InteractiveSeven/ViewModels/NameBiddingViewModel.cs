using InteractiveSeven.Core;
using InteractiveSeven.Core.Bidding.Naming;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InteractiveSeven.ViewModels
{
    public class NameBiddingViewModel : INotifyPropertyChanged
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

        public NameBiddingViewModel(INameAccessor nameAccessor)
        {
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
