using System;
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

        private async void SignInButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception exception)
            {
                LogError(exception, "Failed to Connect to YouTube");
            }
        }

        private void DisconnectButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // TODO: Disconnect
            }
            catch (Exception exception)
            {
                LogError(exception, "Failed to Disconnect from YouTube");
            }
        }

        private void ForgetConnection_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _youTubeSettings.Reset();
            }
            catch (Exception exception)
            {
                LogError(exception, "Failed to Forget YouTube Account");
            }
        }

        private void LogError(Exception exception, string message)
        {
            TextBoxOutput.AppendText(exception.Message);
            TextBoxOutput.AppendText("\n");
            Log.Error(exception, message);
        }
    }
}
