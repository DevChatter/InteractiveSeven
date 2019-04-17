using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using InteractiveSeven.UI.Memory;
using InteractiveSeven.UI.Models;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;

namespace InteractiveSeven.UI.Twitch
{
    public class ChatBot
    {
        private readonly MenuColorAccessor _menuColorAccessor;
        private readonly TwitchClient _client;
        private readonly Regex _hexCodeRegex = new Regex("^#(?:[0-9a-fA-F]{6})$");

        public ChatBot(MenuColorAccessor menuColorAccessor)
        {
            _menuColorAccessor = menuColorAccessor;
            _client = new TwitchClient();

            _client.OnLog += Client_OnLog;
            _client.OnJoinedChannel += Client_OnJoinedChannel;
            _client.OnMessageReceived += Client_OnMessageReceived;
            _client.OnChatCommandReceived += Client_OnChatCommandReceived;
            _client.OnConnected += Client_OnConnected;
            _client.OnDisconnected += Client_OnDisconnected;
        }

        public void Connect()
        {
            ConnectionCredentials credentials = 
                new ConnectionCredentials(TwitchSettings.Settings.Username, TwitchSettings.Settings.AccessToken);
            _client.Initialize(credentials, TwitchSettings.Settings.Channel);
            _client.Connect();
        }

        public void Disconnect()
        {
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
            if (command.ArgumentsAsList.Count == 1)
            {
                string color = command.ArgumentsAsList.Single();
                if (_hexCodeRegex.IsMatch(color))
                {
                    string blueHex = color.Substring(color.Length - 2, 2);
                    string greenHex = color.Substring(color.Length - 4, 2);
                    string redHex = color.Substring(color.Length - 6, 2);

                    int blue = int.Parse(blueHex, NumberStyles.HexNumber);
                    int green = int.Parse(greenHex, NumberStyles.HexNumber);
                    int red = int.Parse(redHex, NumberStyles.HexNumber);

                    var menuColors = new MenuColors
                    {
                        TopLeft = new MenuCornerColor((byte)blue, (byte)green, (byte)red),
                        BotLeft = new MenuCornerColor((byte)blue, (byte)green, (byte)red),
                        TopRight = new MenuCornerColor((byte)blue, (byte)green, (byte)red),
                        BotRight = new MenuCornerColor((byte)blue, (byte)green, (byte)red),
                    };
                    _menuColorAccessor.SetMenuColors("ff7_en", menuColors);
                }
            }
        }


        private void Client_OnLog(object sender, OnLogArgs e)
        {
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
        }

        private void Client_OnDisconnected(object sender, OnDisconnectedEventArgs e)
        {
        }

        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            string message = "FF7 menu color control enabled!";
            _client.SendMessage(e.Channel, message);
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
        }
    }
}