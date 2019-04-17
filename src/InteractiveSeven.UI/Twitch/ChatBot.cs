using System.Collections.Generic;
using InteractiveSeven.UI.Memory;
using InteractiveSeven.UI.Models;
using InteractiveSeven.UI.Services;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;

namespace InteractiveSeven.UI.Twitch
{
    public class ChatBot
    {
        private readonly MenuColorAccessor _menuColorAccessor;
        private readonly IFormSync _formSync;
        private readonly TwitchClient _client;
        private readonly Regex _hexCodeRegex = new Regex("^#(?:[0-9a-fA-F]{6})$");

        public ChatBot(MenuColorAccessor menuColorAccessor, IFormSync formSync)
        {
            _menuColorAccessor = menuColorAccessor;
            _formSync = formSync;
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
            List<string> args = command.ArgumentsAsList;
            var menuColors = new MenuColors();

            switch (args.Count)
            {
                case 1 when _hexCodeRegex.IsMatch(args.Single()):
                    MenuCornerColor hexColor = GetCornerColorFromHex(args.Single());
                    menuColors.TopLeft = hexColor;
                    menuColors.TopRight = hexColor;
                    menuColors.BotLeft = hexColor;
                    menuColors.BotRight = hexColor;
                    break;
                case 1 when Colors.IsValid(args.Single()):
                    MenuCornerColor namedColor = GetCornerColorFromNamed(args.Single());
                    menuColors.TopLeft = namedColor;
                    menuColors.TopRight = namedColor;
                    menuColors.BotLeft = namedColor;
                    menuColors.BotRight = namedColor;
                    break;
                case 4 when args.All(x => _hexCodeRegex.IsMatch(x)):
                    menuColors.TopLeft = GetCornerColorFromHex(args[0]);
                    menuColors.TopRight = GetCornerColorFromHex(args[1]);
                    menuColors.BotLeft = GetCornerColorFromHex(args[2]);
                    menuColors.BotRight = GetCornerColorFromHex(args[3]);
                    break;
                case 4 when args.All(Colors.IsValid):
                    menuColors.TopLeft = GetCornerColorFromNamed(args[0]);
                    menuColors.TopRight = GetCornerColorFromNamed(args[1]);
                    menuColors.BotLeft = GetCornerColorFromNamed(args[2]);
                    menuColors.BotRight = GetCornerColorFromNamed(args[3]);
                    break;
                default:
                    return;
            }

            _menuColorAccessor.SetMenuColors(_formSync.GetProcessName(), menuColors);
            _formSync.RefreshColors();
        }

        private static MenuCornerColor GetCornerColorFromHex(string color)
        {
            string blueHex = color.Substring(color.Length - 2, 2);
            string greenHex = color.Substring(color.Length - 4, 2);
            string redHex = color.Substring(color.Length - 6, 2);

            int blue = int.Parse(blueHex, NumberStyles.HexNumber);
            int green = int.Parse(greenHex, NumberStyles.HexNumber);
            int red = int.Parse(redHex, NumberStyles.HexNumber);

            return new MenuCornerColor((byte) blue, (byte) green, (byte) red);
        }

        private static MenuCornerColor GetCornerColorFromNamed(string colorName)
        {
            Colors color = Colors.ByName(colorName);

            return new MenuCornerColor(color.Blue, color.Green, color.Red);
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