using System.Windows;
using DevChatter.InteractiveGames.Core.Nine;
using DevChatter.InteractiveGames.Core.Seven;
using InteractiveSeven.Core.General;
using InteractiveSeven.Core.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace InteractiveSeven
{
    public partial class GameSelectWindow
    {
        private readonly AppViewModel _viewModel;
        private readonly IServiceCollection _services;

        public GameSelectWindow(AppViewModel appViewModel, IServiceCollection services)
        {
            _viewModel = appViewModel;
            _services = services;
            DataContext = _viewModel;

            InitializeComponent();
        }

        private void FF7Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.GameSelection = GameSelection.FF7;
            _viewModel.GameRunner = new FF7Runner(_services);
            _viewModel.GameRunner.Start();
            Close();
        }

        private void FF8Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.GameSelection = GameSelection.FF8;
            MessageBox.Show("FF8 not implemented... yet. Game over, man!");
            Close();
        }

        private void FF9Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.GameSelection = GameSelection.FF9;
            MessageBox.Show("FF9 not implemented... yet. Game over, man!");
            Close();
        }
    }
}
