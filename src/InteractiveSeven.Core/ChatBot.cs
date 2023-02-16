using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Commands;
using InteractiveSeven.Core.IntervalMessages;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Settings;
using Microsoft.Extensions.Logging;

namespace InteractiveSeven.Core
{
    public class ChatBot : INotifyPropertyChanged
    {
        private readonly IChatClient _chatClient;
        private readonly IList<IChatCommand> _commands;
        private readonly IntervalMessagingService _intervalMessaging;
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

        public ChatBot(IChatClient chatClient, IList<IChatCommand> commands,
            IntervalMessagingService intervalMessaging, GilBank gilBank, ILogger<ChatBot> logger)
        {
            _chatClient = chatClient;
            _commands = commands;
            _intervalMessaging = intervalMessaging;
            _gilBank = gilBank;
            _logger = logger;

            _chatClient.OnLog += Client_OnLog;
            _chatClient.OnJoinedChannel += Client_OnJoinedChannel;
            _chatClient.OnMessageReceived += Client_OnMessageReceived;
            _chatClient.OnChatCommandReceived += Client_OnChatCommandReceived;
            _chatClient.OnConnected += Client_OnConnected;
            _chatClient.OnDisconnected += Client_OnDisconnected;
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
                _chatClient.Connect(Settings.Username, Settings.AccessToken, Settings.Channel);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error Connecting to Twitch");
            }
        }

        public void Disconnect()
        {
            _chatClient.SendMessage(Settings.Channel, "Disconnecting Interactive Seven!");
            _chatClient.Disconnect();
        }

        private void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            try
            {
                _commands.FirstOrDefault(x => x.ShouldExecute(e.CommandData.CommandText))
                    ?.Execute(e.CommandData);
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
            _chatClient.SendMessage(e.Channel, "Interactive Seven is live!");
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            try
            {
                int bits = e.Bits;
                if (bits > 0)
                {
                    _gilBank.Deposit(e.ChatUser, bits);
                }
                else
                {
                    _gilBank.EnsureAccountExists(e.ChatUser);
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
