using InteractiveSeven.Core.Memory;
using InteractiveSeven.Twitch.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using TwitchLib.Client.Events;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;

namespace InteractiveSeven.Twitch
{
    public class ChatBot
    {
        private readonly ITwitchClient _client;
        private readonly IList<ITwitchCommand> _commands;

        public ChatBot(MenuColorAccessor menuColorAccessor,
            ITwitchClient twitchClient, IList<ITwitchCommand> commands)
        {
            _client = twitchClient;
            _commands = commands;

            _client.OnLog += Client_OnLog;
            _client.OnJoinedChannel += Client_OnJoinedChannel;
            _client.OnMessageReceived += Client_OnMessageReceived;
            _client.OnChatCommandReceived += Client_OnChatCommandReceived;
            _client.OnConnected += Client_OnConnected;
            _client.OnDisconnected += Client_OnDisconnected;
        }

        public event EventHandler<OnConnectedArgs> OnConnected;
        public event EventHandler<OnDisconnectedEventArgs> OnDisconnected;

        public void Connect(string username, string accessToken, string channel)
        {
            ConnectionCredentials credentials = 
                new ConnectionCredentials(username, accessToken);
            _client.Initialize(credentials, channel);
            _client.Connect();
        }

        public void Disconnect()
        {
            _client.SendMessage(_client.JoinedChannels.FirstOrDefault(), "Disconnecting Interactive Seven!");
            _client.Disconnect();
        }

        private void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            _commands
                .SingleOrDefault(x => x.ShouldExecute(e.Command.CommandText))
                ?.Execute(e.Command);
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            OnConnected?.Invoke(sender,e);
        }

        private void Client_OnDisconnected(object sender, OnDisconnectedEventArgs e)
        {
            OnDisconnected?.Invoke(sender, e);
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            _client.SendMessage(e.Channel, "Interactive Seven is live!");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
        }
    }
}