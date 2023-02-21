using Chat.YouTube.Events;
using Google.Apis.YouTube.v3.Data;
using InteractiveSeven.Core.Chat;
using YouTube.Base.Clients;
using YouTube.Base;

namespace Chat.YouTube.Chat
{
    public class YouTubeChatClient : IChatClient
    {
        public static string clientID = "884596410562-pcrl1fn8ov0npj7fhjl086ffmud7r5j6.apps.googleusercontent.com";
        public static string clientSecret = "QBkxNmPNIvWatRvOIfRYrXlc";

        public static readonly List<OAuthClientScopeEnum> scopes = new List<OAuthClientScopeEnum>()
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

        public void SendMessage(string channel, string message)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<OnLogArgs>? OnLog;
        public event EventHandler<OnJoinedChannelArgs>? OnJoinedChannel;
        public event EventHandler<OnMessageReceivedArgs>? OnMessageReceived;
        public event EventHandler<OnChatCommandReceivedArgs>? OnChatCommandReceived;
        public event EventHandler<OnConnectedArgs>? OnConnected;
        public event EventHandler<OnDisconnectedEventArgs>? OnDisconnected;

        public void Connect(string username, string accessToken, string channel)
        {
            Task.Run(async () =>
            {
                try
                {
                    System.Console.WriteLine("Initializing connection");

                    YouTubeConnection connection = await YouTubeConnection.ConnectViaLocalhostOAuthBrowser(clientID, clientSecret, scopes);
                    if (connection != null)
                    {
                        Channel channel = await connection.Channels.GetMyChannel();

                        //Channel channel = await connection.Channels.GetChannelByID("");

                        if (channel != null)
                        {
                            System.Console.WriteLine("Connection successful. Logged in as: " + channel.Snippet.Title);

                            var broadcast = await connection.LiveBroadcasts.GetMyActiveBroadcast();

                            System.Console.WriteLine("Connecting chat client!");

                            ChatClient client = new ChatClient(connection);
                            client.OnMessagesReceived += Client_OnMessagesReceived;

                            if (await client.Connect(broadcast))
                            {
                                System.Console.WriteLine("Live chat connection successful!");

                                if (await connection.LiveBroadcasts.GetMyActiveBroadcast() != null)
                                {
                                    await client.SendMessage("Hello World!");
                                }

                                while (true) { }
                            }
                            else
                            {
                                System.Console.WriteLine("Failed to connect to live chat");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                }
            }).RunSynchronously();
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
                    // TODO: Log
                }
            }
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }
    }
}
