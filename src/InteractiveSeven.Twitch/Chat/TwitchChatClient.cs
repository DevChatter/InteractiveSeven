using System;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Events;
using Serilog;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace InteractiveSeven.Twitch.Chat
{
    public class TwitchChatClient : IChatClient
    {
        private readonly ITwitchClient _twitchClient;

        private TwitchSettings Settings => TwitchSettings.Instance;

        public TwitchChatClient(ITwitchClient twitchClient)
        {
            _twitchClient = twitchClient;
            _twitchClient.OnLog += (s, e) => OnLog?.Invoke(s, e.ToCore());
            _twitchClient.OnJoinedChannel += (s, e) => OnJoinedChannel?.Invoke(s, e.ToCore());
            _twitchClient.OnMessageReceived += (s, e) => OnMessageReceived?.Invoke(s, e.ToCore());
            _twitchClient.OnChatCommandReceived += (s, e) => OnChatCommandReceived?.Invoke(s, e.ToCore());
            _twitchClient.OnConnected += (s, e) => OnConnected?.Invoke(s, e.ToCore());
            _twitchClient.OnDisconnected += (s, e) => OnDisconnected?.Invoke(s, e.ToCore());
        }

        public void SendMessage(string channel, string message)
        {
            _twitchClient.SendMessage(channel, message);
        }

        public event EventHandler<OnLogArgs> OnLog;
        public event EventHandler<OnJoinedChannelArgs> OnJoinedChannel;
        public event EventHandler<OnMessageReceivedArgs> OnMessageReceived;
        public event EventHandler<OnChatCommandReceivedArgs> OnChatCommandReceived;
        public event EventHandler<OnConnectedArgs> OnConnected;
        public event EventHandler<OnDisconnectedEventArgs> OnDisconnected;

        public void Connect(string username, string accessToken, string channel)
        {
            Log.Information($"Trying to connect to Twitch channel {channel} as {username}");
            ConnectionCredentials credentials = new ConnectionCredentials(Settings.Username, Settings.AccessToken);
            _twitchClient.Initialize(credentials, Settings.Channel);
            _twitchClient.Connect();
        }

        public void Disconnect()
        {
            Log.Information("Disconnecting from Twitch");
            _twitchClient.Disconnect();
        }
    }
}
