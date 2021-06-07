using InteractiveSeven.Core.ViewModels;
using MahApps.Metro.Controls;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using InteractiveSeven.Core.Windows;

namespace InteractiveSeven
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private MetroWindow _accentStyleWindow;
        private MetroWindow _settingsWindow;

        public MainWindow(MainWindowViewModel viewModel,
            ISettingsWindow settingsWindow,
            IAccentStyleWindow accentStyleWindow)
        {
            _accentStyleWindow = accentStyleWindow as MetroWindow;
            _settingsWindow = settingsWindow as MetroWindow;
            InitializeComponent();
            DataContext = viewModel;
            ViewModel = viewModel;
        }

        public MainWindowViewModel ViewModel { get; }

        private void PatreonLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            };
            Process.Start(psi);
            e.Handled = true;
        }

        private void ChangeSettingsButtonClick(object sender, RoutedEventArgs e)
        {
            if (_settingsWindow != null)
            {
                _settingsWindow.Activate();
                return;
            }

            _settingsWindow.Owner = this;
            _settingsWindow.Closed += (o, args) => _settingsWindow = null;
            _settingsWindow.Left = Left + ActualWidth / 6.0;
            _settingsWindow.Top = Top + ActualHeight / 6.0;
            _settingsWindow.Show();
        }

        private void ChangeAppStyleButtonClick(object sender, RoutedEventArgs e)
        {
            if (_accentStyleWindow != null)
            {
                _accentStyleWindow.Activate();
                return;
            }

            _accentStyleWindow.Owner = this;
            _accentStyleWindow.Closed += (o, args) => _accentStyleWindow = null;
            _accentStyleWindow.Left = Left + ActualWidth / 4.0;
            _accentStyleWindow.Top = Top + ActualHeight / 4.0;
            _accentStyleWindow.Show();
        }
    }
}
