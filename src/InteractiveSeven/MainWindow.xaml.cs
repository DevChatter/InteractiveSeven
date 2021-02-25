using InteractiveSeven.Core.ViewModels;
using MahApps.Metro.Controls;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace InteractiveSeven
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        public MainWindow(MainWindowViewModel viewModel)
        {
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

        private MetroWindow _settingsWindow;
        private void ChangeSettingsButtonClick(object sender, RoutedEventArgs e)
        {
            if (_settingsWindow != null)
            {
                _settingsWindow.Activate();
                return;
            }

            _settingsWindow = new SettingsWindow(ViewModel.SettingsViewModel);
            _settingsWindow.Owner = this;
            _settingsWindow.Closed += (o, args) => _settingsWindow = null;
            _settingsWindow.Left = Left + ActualWidth / 6.0;
            _settingsWindow.Top = Top + ActualHeight / 6.0;
            _settingsWindow.Show();
        }

        private MetroWindow _accentThemeTestWindow;
        private void ChangeAppStyleButtonClick(object sender, RoutedEventArgs e)
        {
            if (_accentThemeTestWindow != null)
            {
                _accentThemeTestWindow.Activate();
                return;
            }

            _accentThemeTestWindow = new AccentStyleWindow(ViewModel.ThemeViewModel);
            _accentThemeTestWindow.Owner = this;
            _accentThemeTestWindow.Closed += (o, args) => _accentThemeTestWindow = null;
            _accentThemeTestWindow.Left = Left + ActualWidth / 4.0;
            _accentThemeTestWindow.Top = Top + ActualHeight / 4.0;
            _accentThemeTestWindow.Show();
        }
    }
}
