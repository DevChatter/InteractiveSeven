using InteractiveSeven.Core.ViewModels;
using System;
using System.Windows;
using Xceed.Wpf.Toolkit;

namespace InteractiveSeven
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow
    {
        public SettingsWindow(SettingsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            ViewModel = viewModel;
        }

        public SettingsViewModel ViewModel { get; }

        private void AccessTokenTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is WatermarkPasswordBox passwordBox
                && passwordBox.Password.StartsWith("oauth:", StringComparison.OrdinalIgnoreCase))
            {
                ViewModel.TwitchSettings.AccessToken = passwordBox.Password;
            }
        }

    }
}
