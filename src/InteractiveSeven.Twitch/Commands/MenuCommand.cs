using System.Collections.Generic;
using System.Drawing;
using InteractiveSeven.Core;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace InteractiveSeven.Twitch.Commands
{
    public class MenuCommand : BaseCommand
    {
        private readonly MenuColorAccessor _menuColorAccessor;
        private readonly ITwitchClient _twitchClient;

        private MenuColorSettings MenuSettings => ApplicationSettings.Instance.MenuSettings;

        public MenuCommand(MenuColorAccessor menuColorAccessor, ITwitchClient twitchClient)
            : base(new []{ "Menu", "MenuColor", "Window", "Windows" })
        {
            _menuColorAccessor = menuColorAccessor;
            _twitchClient = twitchClient;
        }

        public override void Execute(ChatCommand chatCommand)
        {
            if (!MenuSettings.Enabled) return;
            if (chatCommand.ChatMessage.Bits < MenuSettings.BitCost)
            {
                var message = $"Sorry, '!{chatCommand.CommandText}' has a minimum cheer cost of {MenuSettings.BitCost}.";
                _twitchClient.SendMessage(chatCommand.ChatMessage.Channel, message);
                return;
            }

            List<string> args = chatCommand.ArgumentsAsList;
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
    }
}