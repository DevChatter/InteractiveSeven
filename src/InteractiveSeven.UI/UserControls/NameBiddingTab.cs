using InteractiveSeven.Core.Events;
using InteractiveSeven.UI.ViewModels;
using ReactiveUI.Winforms;
using System;

namespace InteractiveSeven.UI.UserControls
{
    public partial class NameBiddingTab : ReactiveUserControl<NameBiddingViewModel>
    {
        public NameBiddingTab(NameBiddingViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();

            DomainEvents.Register<NameVoteReceived>(x
                => Invoke(new Action(() => ViewModel.HandleNameVote(x))));

            DomainEvents.Register<TopNameChanged>(de
                => Invoke(new Action(() => ViewModel.HandleNameChange(de))));

            //this.Bind(ViewModel, vm => vm.NameBids, v => v.nameBidding1);
        }
    }
}
