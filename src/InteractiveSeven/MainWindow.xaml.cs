using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.ViewModels;
using InteractiveSeven.Startup;
using MahApps.Metro.Controls;

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
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ConfigureLogging.WithRichTextOutput(OutputBox);
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

        private MetroWindow _youTubeWindow;
        private void YouTubeButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_youTubeWindow != null)
            {
                _youTubeWindow.Activate();
                return;
            }

            _youTubeWindow = new YouTubeWindow(YouTubeSettings.Instance);
            _youTubeWindow.Owner = this;
            _youTubeWindow.Closed += (o, args) => _youTubeWindow = null;
            _youTubeWindow.Left = Left + ActualWidth / 4.0;
            _youTubeWindow.Top = Top + ActualHeight / 4.0;
            _youTubeWindow.Show();
        }

        private void TwitchButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
