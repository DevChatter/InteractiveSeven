using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Commands;
using InteractiveSeven.Core.IntervalMessages;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Settings;
using Serilog;

namespace InteractiveSeven.Core
{
    public partial class ChatBot : ObservableObject
    {
        private readonly IChatClient _chatClient;
        private readonly IList<IChatCommand> _commands;
        private readonly IntervalMessagingService _intervalMessaging;
        private readonly GilBank _gilBank;

        [ObservableProperty]
        private bool _isConnected;

        private TwitchSettings Settings => TwitchSettings.Instance;

        public ChatBot(IChatClient chatClient, IList<IChatCommand> commands,
            IntervalMessagingService intervalMessaging, GilBank gilBank)
        {
            _chatClient = chatClient;
            _commands = commands;
            _intervalMessaging = intervalMessaging;
            _gilBank = gilBank;

            _chatClient.OnLog += Client_OnLog;
            _chatClient.OnJoinedChannel += Client_OnJoinedChannel;
            _chatClient.OnMessageReceived += Client_OnMessageReceived;
            _chatClient.OnChatCommandReceived += Client_OnChatCommandReceived;
            _chatClient.OnConnected += Client_OnConnected;
            _chatClient.OnDisconnected += Client_OnDisconnected;
        }

        public async Task Connect()
        {
            if (string.IsNullOrWhiteSpace(Settings.Username)
                || string.IsNullOrWhiteSpace(Settings.AccessToken)
                || string.IsNullOrWhiteSpace(Settings.Channel))
            {
                Log.Error("Cannot Connect. Please confirm that Username, Channel, and Access token are set.");
                return;
            }

            Settings.AccessToken = Settings.AccessToken.Replace("oauth:", "", StringComparison.OrdinalIgnoreCase);

            try
            {
                await _chatClient.Connect(Settings.Username, Settings.AccessToken, Settings.Channel);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error Connecting to Twitch");
            }
        }

        public async Task Disconnect()
        {
            await _chatClient.SendMessage(Settings.Channel, "Disconnecting Interactive Seven!");
            await _chatClient.Disconnect();
        }

        private async void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            try
            {
                IChatCommand command = _commands.FirstOrDefault(x => x.ShouldExecute(e.CommandData.CommandText));
                await (command?.Execute(e.CommandData) ?? Task.CompletedTask);
                await _intervalMessaging.MessageReceived();
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Command Error");
            }
        }

        private void Client_OnLog(object sender, OnLogArgs e)
        {
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Log.Information($"ChatBot connected as {e.Username}");
            IsConnected = true;
        }

        private void Client_OnDisconnected(object sender, OnDisconnectedEventArgs e)
        {
            IsConnected = false;
        }

        private async void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            await _chatClient.SendMessage(e.Channel, "Interactive Seven is live!");
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
                Log.Error(exception, "Error updating account balances.");
            }
        }
    }
}
