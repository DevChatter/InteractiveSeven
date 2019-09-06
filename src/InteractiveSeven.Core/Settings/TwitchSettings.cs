using System.Linq;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.ViewModels;
using Newtonsoft.Json;

namespace InteractiveSeven.Core.Settings
{
    public class TwitchSettings : ObservableSettingsBase
    {
        private string _accessToken;
        private string _username;
        private string _channel;
        private bool _updatedFromTwitch;

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

                UpdatedFromTwitch = true;

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

        [JsonIgnore]
        public bool UpdatedFromTwitch
        {
            get => _updatedFromTwitch;
            set
            {
                _updatedFromTwitch = value;
                OnPropertyChanged();
            }
        }
    }
}