using InteractiveSeven.Core;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;

namespace InteractiveSeven.Twitch
{
    public class ChatBot
    {
        private readonly MenuColorAccessor _menuColorAccessor;
        private readonly ITwitchClient _client;

        public ChatBot(MenuColorAccessor menuColorAccessor)
        {
            _menuColorAccessor = menuColorAccessor;
            _client = new TwitchClient(); // TODO: Use DI

            _client.OnLog += Client_OnLog;
            _client.OnJoinedChannel += Client_OnJoinedChannel;
            _client.OnMessageReceived += Client_OnMessageReceived;
            _client.OnChatCommandReceived += Client_OnChatCommandReceived;
            _client.OnConnected += Client_OnConnected;
            _client.OnDisconnected += Client_OnDisconnected;
        }

        public event EventHandler<OnConnectedArgs> OnConnected;
        public event EventHandler<OnDisconnectedEventArgs> OnDisconnected;

        public bool IsMenuCommandAllowed { get; set; } = true;

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
            switch (e.Command.CommandText)
            {
                case "menu":
                    HandleMenuCommand(e.Command);
                    break;
                default:
                    return;
            }
        }

        private void HandleMenuCommand(ChatCommand command)
        {
            if (!IsMenuCommandAllowed) return;

            List<string> args = command.ArgumentsAsList;
            var menuColors = new MenuColors();

            switch (args.Count)
            {
                case 1:
                    Color hexColor = args[0].ToColor();
                    menuColors.TopLeft = hexColor;
                    menuColors.TopRight = hexColor;
                    menuColors.BotLeft = hexColor;
                    menuColors.BotRight = hexColor;
                    break;
                case 4:
                    menuColors.TopLeft = args[0].ToColor();
                    menuColors.TopRight = args[1].ToColor();
                    menuColors.BotLeft = args[2].ToColor();
                    menuColors.BotRight = args[3].ToColor();
                    break;
                default:
                    // Invalid case, do nothing.
                    return;
            }

            DomainEvents.Raise(new MenuColorChanging(menuColors));

            _menuColorAccessor.SetMenuColors("ff7_en", menuColors);
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