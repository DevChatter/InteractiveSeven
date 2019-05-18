using InteractiveSeven.Core.Events;
using InteractiveSeven.UI.ViewModels;
using ReactiveUI.Winforms;
using System;
using InteractiveSeven.Core;
using ReactiveUI;

namespace InteractiveSeven.UI.UserControls
{
    public partial class NameBiddingTab : ReactiveUserControl<NameBiddingViewModel>
    {
        private NameBidding _cloudBidding;
        private NameBidding _barretBidding;
        private NameBidding _tifaBidding;
        private NameBidding _aerisBidding;
        private NameBidding _caitSithBidding;
        private NameBidding _cidBidding;
        private NameBidding _redBidding;
        private NameBidding _vincentBidding;
        private NameBidding _yuffieBidding;

        public NameBiddingTab(NameBiddingViewModel viewModel,
            NameBidding cloudBidding,
            NameBidding barretBidding,
            NameBidding tifaBidding,
            NameBidding aerisBidding,
            NameBidding caitSithBidding,
            NameBidding cidBidding,
            NameBidding redBidding,
            NameBidding vincentBidding,
            NameBidding yuffieBidding
            )
        {
            ViewModel = viewModel;

            InitializeComponent();

            _cloudBidding = SetUp(cloudBidding, Constants.Cloud, 0);
            _barretBidding = SetUp(barretBidding, Constants.Barret, 1);
            _tifaBidding = SetUp(tifaBidding, Constants.Tifa, 2);
            _aerisBidding = SetUp(aerisBidding, Constants.Aeris, 3);
            _redBidding = SetUp(redBidding, Constants.Red, 4);
            _caitSithBidding = SetUp(caitSithBidding, Constants.CaitSith, 5);
            _cidBidding = SetUp(cidBidding, Constants.Cid, 6);
            _yuffieBidding = SetUp(yuffieBidding, Constants.Yuffie, 7);
            _vincentBidding = SetUp(vincentBidding, Constants.Vincent, 8);


            DomainEvents.Register<NameVoteReceived>(x
                => Invoke(new Action(() => ViewModel.HandleNameVote(x))));

            DomainEvents.Register<TopNameChanged>(de
                => Invoke(new Action(() => ViewModel.HandleNameChange(de))));
        }

        private NameBidding SetUp(NameBidding nameBiding, string name, int order)
        {
            flowLayoutPanel3.Controls.Add(nameBiding);
            nameBiding.AutoScroll = true;
            nameBiding.Location = new System.Drawing.Point(3, 3);
            nameBiding.Size = new System.Drawing.Size(237, 179);
            nameBiding.TabIndex = order;

            nameBiding.ViewModel.SetName(name);

            return nameBiding;
        }
    }
}
