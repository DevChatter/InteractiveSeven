using InteractiveSeven.Core.ViewModels;
using MahApps.Metro.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using Xceed.Wpf.Toolkit;

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

        private void AccessTokenTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is WatermarkPasswordBox passwordBox
                && passwordBox.Password.StartsWith("oauth:", StringComparison.OrdinalIgnoreCase))
            {
                ViewModel.SettingsViewModel.TwitchSettings.AccessToken = passwordBox.Password;
            }
        }

        private MetroWindow _accentThemeTestWindow;
        private void ChangeAppStyleButtonClick(object sender, RoutedEventArgs e)
        {
            if (_accentThemeTestWindow != null)
            {
                _accentThemeTestWindow.Activate();
                return;
            }

            _accentThemeTestWindow = new AccentStyleWindow();
            _accentThemeTestWindow.Owner = this;
            _accentThemeTestWindow.Closed += (o, args) => _accentThemeTestWindow = null;
            _accentThemeTestWindow.Left = this.Left + this.ActualWidth / 2.0;
            _accentThemeTestWindow.Top = this.Top + this.ActualHeight / 2.0;
            _accentThemeTestWindow.Show();
        }
    }
}
