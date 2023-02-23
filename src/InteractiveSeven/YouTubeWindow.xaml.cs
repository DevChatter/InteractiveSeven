using System.Windows;
using Chat.YouTube;
using Chat.YouTube.Chat;
using InteractiveSeven.Core.Settings;
using MahApps.Metro.Controls;
using Serilog;
using StreamingClient.Base.Model.OAuth;
using YouTube.Base;

namespace InteractiveSeven
{
    public partial class YouTubeWindow : MetroWindow
    {
        private readonly YouTubeSettings _youTubeSettings;
        private const string ClientId = "581786658708-elflankerquo1a6vsckabbhn25hclla0.apps.googleusercontent.com";
        private const string ClientSecret = "3f6NggMbPtrmIBpgx-MK2xXK";

        public YouTubeWindow(YouTubeSettings youTubeSettings)
        {
            _youTubeSettings = youTubeSettings;
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var connection = await YouTubeConnection
                .ConnectViaLocalhostOAuthBrowser(
                    ClientId,
                    ClientSecret,
                    YouTubeChatClient.Scopes);

            if (connection == null)
            {
                Log.Error("Failed to Connect to YouTube.");
                return;
            }

            OAuthTokenModel tokenCopy = connection.GetOAuthTokenCopy();
            tokenCopy.MapOntoSettings(_youTubeSettings);

        }
    }
}
