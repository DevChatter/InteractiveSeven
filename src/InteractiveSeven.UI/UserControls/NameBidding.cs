using InteractiveSeven.UI.ViewModels;
using ReactiveUI.Winforms;
using System.ComponentModel;
using ReactiveUI;

namespace InteractiveSeven.UI.UserControls
{
    public partial class NameBidding : ReactiveUserControl<NameBidsViewModel>
    {
        public NameBidding(NameBidsViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();

            this.Bind(ViewModel, vm => vm.DefaultName, v => v.characterNameLabel.Text);
            this.Bind(ViewModel, vm => vm.NameBids, v => v.dataGridView1.DataSource);
        }
    }
}
