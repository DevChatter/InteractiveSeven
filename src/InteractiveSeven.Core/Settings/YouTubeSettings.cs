using CommunityToolkit.Mvvm.ComponentModel;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.ViewModels;

namespace InteractiveSeven.Core.Settings
{
    public partial class YouTubeSettings : ObservableObject
    {
        [ObservableProperty]
        private string _accessToken;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _channel;

        [ObservableProperty]
        private bool _updatedFromYouTube;

        public static YouTubeSettings Instance { get; private set; }

        static YouTubeSettings()
        {
            Instance = new YouTubeSettings();
        }

        private void ReceivedAccessToken(AccessTokenReceived e)
        {
            // TODO: change to YouTube
            if (e.State == TwitchAuthViewModel.State
                && e.TokenType == "bearer")
            {
                AccessToken = $"oauth:{e.AccessToken}";

                UpdatedFromYouTube = true;
            }
        }

    }
}
