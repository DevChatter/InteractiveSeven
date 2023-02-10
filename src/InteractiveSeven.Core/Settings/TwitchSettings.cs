using System;
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

        public static TwitchSettings Instance { get; private set; }
        static TwitchSettings()
        {
            Instance = new TwitchSettings();
        }

        private TwitchSettings()
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

        public static void LoadFromJson(string json, Action<Exception> errorLogging = null)
        {
            try
            {
                JsonConvert.PopulateObject(json, Instance);
            }
            catch (Exception ex)
            {
                Instance = new TwitchSettings();
                errorLogging?.Invoke(ex);
            }
        }
    }
}