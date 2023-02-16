using System.Windows.Input;
using InteractiveSeven.Core.MvvmCommands;

namespace InteractiveSeven.Core.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(MenuColorViewModel menuColorViewModel,
            StreamOverlayViewModel streamOverlayViewModel,
            NameBiddingViewModel nameBiddingViewModel,
            SettingsViewModel settingsViewModel,
            ThemeViewModel themeViewModel,
            ChatBot chatBot)
        {
            MenuColorViewModel = menuColorViewModel;
            NameBiddingViewModel = nameBiddingViewModel;
            SettingsViewModel = settingsViewModel;
            ThemeViewModel = themeViewModel;
            StreamOverlayViewModel = streamOverlayViewModel;
            ChatBot = chatBot;
            ConnectBotCommand = new BotConnectCommand(chatBot);
            DisconnectBotCommand = new BotDisconnectCommand(chatBot);
        }

        public ICommand ConnectBotCommand { get; }
        public ICommand DisconnectBotCommand { get; }

        public MenuColorViewModel MenuColorViewModel { get; }
        public NameBiddingViewModel NameBiddingViewModel { get; }
        public StreamOverlayViewModel StreamOverlayViewModel { get; }
        public SettingsViewModel SettingsViewModel { get; }
        public ThemeViewModel ThemeViewModel { get; }
        public ChatBot ChatBot { get; }
    }
}
