using InteractiveSeven.Core.MvvmCommands;
using System.Windows.Input;

namespace InteractiveSeven.Core.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(MenuColorViewModel menuColorViewModel,
            StreamOverlayViewModel streamOverlayViewModel,
            NameBiddingViewModel nameBiddingViewModel,
            SettingsViewModel settingsViewModel,
            ThemeViewModel themeViewModel,
            IChatBot chatBot)
        {
            MenuColorViewModel = menuColorViewModel;
            NameBiddingViewModel = nameBiddingViewModel;
            SettingsViewModel = settingsViewModel;
            ThemeViewModel = themeViewModel;
            StreamOverlayViewModel = streamOverlayViewModel;
            ChatBot = chatBot;
            ConnectBotCommand = new SimpleCommand(x => ChatBot.Connect());
            DisconnectBotCommand = new SimpleCommand(x => ChatBot.Disconnect());
        }

        public ICommand ConnectBotCommand { get; }
        public ICommand DisconnectBotCommand { get; }

        public MenuColorViewModel MenuColorViewModel { get; }
        public NameBiddingViewModel NameBiddingViewModel { get; }
        public StreamOverlayViewModel StreamOverlayViewModel { get; }
        public SettingsViewModel SettingsViewModel { get; }
        public ThemeViewModel ThemeViewModel { get; }
        public IChatBot ChatBot { get; }
    }
}
