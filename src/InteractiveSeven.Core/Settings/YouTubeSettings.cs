using System;
using System.Runtime.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace InteractiveSeven.Core.Settings
{
    public partial class YouTubeSettings : ObservableObject
    {
        [DataMember]
        [ObservableProperty]
        private string _clientId;

        [DataMember]
        [ObservableProperty]
        private string _clientSecret;

        [DataMember]
        [ObservableProperty]
        private string _authorizationCode;

        [JsonProperty("refresh_token")]
        [ObservableProperty]
        private string _refreshToken;

        [JsonProperty("access_token")]
        [ObservableProperty]
        private string _accessToken;

        [JsonProperty("expires_in")]
        [ObservableProperty]
        private long _expiresIn;

        [ObservableProperty]
        private long _expiresTimeStamp;

        [DataMember]
        [ObservableProperty]
        private string _redirectUrl;

        [DataMember]
        [ObservableProperty]
        private DateTimeOffset _acquiredDateTime;

        [JsonIgnore]
        public DateTimeOffset ExpirationDateTime =>
            ExpiresTimeStamp <= 0L
            ? AcquiredDateTime.AddSeconds((double)ExpiresIn)
            : DateTimeOffset.FromUnixTimeSeconds(ExpiresTimeStamp);

        [JsonIgnore]
        [ObservableProperty]
        private bool _isDisconnected;

        public static YouTubeSettings Instance { get; private set; }

        static YouTubeSettings()
        {
            Instance = new YouTubeSettings();
        }

        public static void LoadFromJson(string json, Action<Exception> errorLogging = null)
        {
            try
            {
                JsonConvert.PopulateObject(json, Instance);
            }
            catch (Exception ex)
            {
                Instance = new YouTubeSettings();
                errorLogging?.Invoke(ex);
            }
        }

        public void Reset()
        {
            AccessToken = "";
            AcquiredDateTime = DateTimeOffset.MinValue;
            AuthorizationCode = "";
            ClientId = "";
            ClientSecret = "";
            ExpiresIn = 0;
            ExpiresTimeStamp = 0;
            RedirectUrl = "";
            RefreshToken = "";
        }
    }
}
