using InteractiveSeven.Twitch;
using InteractiveSeven.ViewModels;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace InteractiveSeven
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ChatBot _chatBot;

        public MainWindow(MenuColorViewModel menuColorViewModel, SettingsViewModel settingsViewModel, ChatBot chatBot)
        {
            InitializeComponent();
            MenuColorGrid.DataContext = menuColorViewModel;
            MenuColorGroup.DataContext = menuColorViewModel;
            SettingsTab.DataContext = settingsViewModel;
            _chatBot = chatBot;
        }

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
    }
}
