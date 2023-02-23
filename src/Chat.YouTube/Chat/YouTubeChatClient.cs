using Chat.YouTube.Events;
using Google.Apis.YouTube.v3.Data;
using InteractiveSeven.Core.Chat;
using Serilog;
using YouTube.Base.Clients;
using YouTube.Base;

namespace Chat.YouTube.Chat
{
    public class YouTubeChatClient : IChatClient
    {
        private const string ClientId = "884596410562-pcrl1fn8ov0npj7fhjl086ffmud7r5j6.apps.googleusercontent.com";
        private const string ClientSecret = "QBkxNmPNIvWatRvOIfRYrXlc";

        public static readonly List<OAuthClientScopeEnum> Scopes = new()
        {
            OAuthClientScopeEnum.ChannelMemberships,
            OAuthClientScopeEnum.ManageAccount,
            OAuthClientScopeEnum.ManageData,
            OAuthClientScopeEnum.ManagePartner,
            OAuthClientScopeEnum.ManagePartnerAudit,
            OAuthClientScopeEnum.ManageVideos,
            OAuthClientScopeEnum.ReadOnlyAccount,
            OAuthClientScopeEnum.ViewAnalytics,
            OAuthClientScopeEnum.ViewMonetaryAnalytics
        };

        public YouTubeChatClient()
        {
        }

        public Task SendMessage(string channel, string message)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<OnLogArgs>? OnLog;
        public event EventHandler<OnJoinedChannelArgs>? OnJoinedChannel;
        public event EventHandler<OnMessageReceivedArgs>? OnMessageReceived;
        public event EventHandler<OnChatCommandReceivedArgs>? OnChatCommandReceived;
        public event EventHandler<OnConnectedArgs>? OnConnected;
        public event EventHandler<OnDisconnectedEventArgs>? OnDisconnected;

        public async Task Connect(string username, string accessToken, string channelId)
        {
            try
            {
                Log.Information("Initializing connection");
                YouTubeConnection connection = await YouTubeConnection.ConnectViaLocalhostOAuthBrowser(ClientId, ClientSecret, Scopes);
                if (connection != null)
                {
                    Channel channel = await connection.Channels.GetChannelByID(channelId);

                    //Channel channel = await connection.Channels.GetMyChannel("");

                    if (channel != null)
                    {
                        Log.Information("Connection successful. Logged in as: " + channel.Snippet.Title);

                        var broadcast = await connection.LiveBroadcasts.GetMyActiveBroadcast();

                        Log.Information("Connecting chat client!");

                        ChatClient client = new ChatClient(connection);
                        client.OnMessagesReceived += Client_OnMessagesReceived;

                        if (await client.Connect(broadcast))
                        {
                            Log.Information("Live chat connection successful!");

                            if (await connection.LiveBroadcasts.GetMyActiveBroadcast() != null)
                            {
                                await client.SendMessage("Hello World!");
                            }

                            while (true) { }
                        }
                        else
                        {
                            Log.Information("Failed to connect to live chat");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error connecting to YouTube chat");
            }
        }

        private void Client_OnMessagesReceived(object? sender, IEnumerable<LiveChatMessage> messages)
        {
            foreach (LiveChatMessage message in messages)
            {
                try
                {
                    OnMessageReceived?.Invoke(sender, message.ToCore());
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error invoking individual message handling.");
                }
            }
        }

        public Task Disconnect()
        {
            throw new NotImplementedException();
        }
    }
}
