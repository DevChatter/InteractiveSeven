using System.Linq;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.ViewModels;

namespace InteractiveSeven.Core.Settings
{
    public class TwitchSettings : ObservableSettingsBase
    {
        private string _accessToken;
        private string _username;
        private string _channel;

        public TwitchSettings()
        {
            DomainEvents.Register<AccessTokenReceived>(ReceivedAccessToken);
        }

        private void ReceivedAccessToken(AccessTokenReceived e)
        {
            if (e.State == TwitchAuthViewModel.State
                && e.TokenType == "bearer")
            {
                _accessToken = $"oauth:{e.AccessToken}";

                // TODO: Message box asking if they want to save?
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string AccessToken
        {
            get => _accessToken;
            set
            {
                if (value != null && !value.All(x => x == '●'))
                {
                    _accessToken = value;
                }
            }
        }

        public string Channel
        {
            get => _channel;
            set
            {
                _channel = value;
                OnPropertyChanged();
            }
        }
    }
}