using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using InteractiveSeven.Core;
using InteractiveSeven.Core.Commands;
using InteractiveSeven.Core.IntervalMessages;
using InteractiveSeven.Core.Model;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Commands;
using Microsoft.Extensions.Logging;
using TwitchLib.Client.Events;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;

namespace InteractiveSeven.Twitch
{
    public class ChatBot : INotifyPropertyChanged, IChatBot
    {
        private readonly ITwitchClient _client;
        private readonly IList<ITwitchCommand> _commands;
        private readonly IIntervalMessagingService _intervalMessaging;
        private readonly GilBank _gilBank;
        private readonly ILogger<ChatBot> _logger;
        private bool _isConnected;

        private TwitchSettings Settings => TwitchSettings.Instance;

        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                _isConnected = value;
                OnPropertyChanged();
            }
        }

        public ChatBot(ITwitchClient twitchClient, IList<ITwitchCommand> commands,
            IIntervalMessagingService intervalMessaging, GilBank gilBank, ILogger<ChatBot> logger)
        {
            _client = twitchClient;
            _commands = commands;
            _intervalMessaging = intervalMessaging;
            _gilBank = gilBank;
            _logger = logger;

            _client.OnLog += Client_OnLog;
            _client.OnJoinedChannel += Client_OnJoinedChannel;
            _client.OnMessageReceived += Client_OnMessageReceived;
            _client.OnChatCommandReceived += Client_OnChatCommandReceived;
            _client.OnConnected += Client_OnConnected;
            _client.OnDisconnected += Client_OnDisconnected;
        }

        public void Connect()
        {
            if (string.IsNullOrWhiteSpace(Settings.Username)
                || string.IsNullOrWhiteSpace(Settings.AccessToken)
                || string.IsNullOrWhiteSpace(Settings.Channel))
            {
                return;
            }

            Settings.AccessToken = Settings.AccessToken.Replace("oauth:", "", StringComparison.OrdinalIgnoreCase);

            try
            {
                ConnectionCredentials credentials = new ConnectionCredentials(Settings.Username, Settings.AccessToken);
                _client.Initialize(credentials, Settings.Channel);
                _client.Connect();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error Connecting to Twitch");
            }
        }

        public void Disconnect()
        {
            _client.SendMessage(_client.JoinedChannels.FirstOrDefault(), "Disconnecting Interactive Seven!");
            _client.Disconnect();
        }

        private void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            try
            {
                _commands.FirstOrDefault(x => x.ShouldExecute(e.Command.CommandText))
                    ?.Execute(CommandData.FromChatCommand(e.Command));
                _intervalMessaging.MessageReceived();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Command Error");
            }
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            IsConnected = true;
        }

        private void Client_OnDisconnected(object sender, OnDisconnectedEventArgs e)
        {
            IsConnected = false;
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            _client.SendMessage(e.Channel, "Interactive Seven is live!");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            try
            {
                int bits = e.ChatMessage.Bits;
                if (bits > 0)
                {
                    _gilBank.Deposit(ChatUser.FromChatMessage(e.ChatMessage), bits);
                }
                else
                {
                    _gilBank.EnsureAccountExists(ChatUser.FromChatMessage(e.ChatMessage));
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error updating account balances.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
