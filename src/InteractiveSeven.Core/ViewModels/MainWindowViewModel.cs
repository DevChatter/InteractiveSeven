using InteractiveSeven.Core.Bidding.Naming;
using InteractiveSeven.Core.Data;
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
            IShowTwitchAuthCommand showTwitchAuthCommand,
            IChatBot chatBot,
            IDataStore<CharacterNameBid> dataStore)
        {
            MenuColorViewModel = menuColorViewModel;
            NameBiddingViewModel = nameBiddingViewModel;
            SettingsViewModel = settingsViewModel;
            StreamOverlayViewModel = streamOverlayViewModel;
            ChatBot = chatBot;
            ConnectBotCommand = new SimpleCommand(x => ChatBot.Connect());
            DisconnectBotCommand = new SimpleCommand(x => ChatBot.Disconnect());
            OpenTwitchAuthWindow = showTwitchAuthCommand;
        }

        public ICommand ConnectBotCommand { get; }
        public ICommand DisconnectBotCommand { get; }
        public ICommand OpenTwitchAuthWindow { get; }

        public MenuColorViewModel MenuColorViewModel { get; }
        public NameBiddingViewModel NameBiddingViewModel { get; }
        public StreamOverlayViewModel StreamOverlayViewModel { get; }
        public SettingsViewModel SettingsViewModel { get; }
        public IChatBot ChatBot { get; }
    }
}
