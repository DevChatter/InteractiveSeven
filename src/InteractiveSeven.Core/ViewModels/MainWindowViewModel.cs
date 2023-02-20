using CommunityToolkit.Mvvm.Input;

namespace InteractiveSeven.Core.ViewModels
{
    public partial class MainWindowViewModel
    {
        public MainWindowViewModel(MenuColorViewModel menuColorViewModel,
            StreamOverlayViewModel streamOverlayViewModel,
            NameBiddingViewModel nameBiddingViewModel,
            SettingsViewModel settingsViewModel,
            ThemeViewModel themeViewModel,
            MonitorViewModel monitorViewModel,
            ChatBot chatBot)
        {
            MenuColorViewModel = menuColorViewModel;
            NameBiddingViewModel = nameBiddingViewModel;
            SettingsViewModel = settingsViewModel;
            ThemeViewModel = themeViewModel;
            MonitorViewModel = monitorViewModel;
            StreamOverlayViewModel = streamOverlayViewModel;
            ChatBot = chatBot;
        }

        [RelayCommand]
        public void ConnectBot() => ChatBot.Connect();

        [RelayCommand]
        public void DisconnectBot() => ChatBot.Disconnect();

        public MenuColorViewModel MenuColorViewModel { get; }
        public NameBiddingViewModel NameBiddingViewModel { get; }
        public StreamOverlayViewModel StreamOverlayViewModel { get; }
        public SettingsViewModel SettingsViewModel { get; }
        public ThemeViewModel ThemeViewModel { get; }
        public MonitorViewModel MonitorViewModel { get; }
        public ChatBot ChatBot { get; }
    }
}
