using System.Linq;

namespace InteractiveSeven.Core.Settings
{
    public class TwitchSettings : ObservableSettingsBase
    {
        private string _accessToken;
        private string _username;
        private string _channel;

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
                if (!value.All(x => x == '●'))
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