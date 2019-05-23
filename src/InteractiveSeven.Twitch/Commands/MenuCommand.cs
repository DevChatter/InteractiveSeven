using InteractiveSeven.Core;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Model;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class MenuCommand : BaseCommand
    {
        private readonly IMenuColorAccessor _menuColorAccessor;
        private readonly ITwitchClient _twitchClient;

        private string ProcessName => ApplicationSettings.Instance.ProcessName;
        private MenuColorSettings MenuSettings => ApplicationSettings.Instance.MenuSettings;

        public MenuCommand(IMenuColorAccessor menuColorAccessor, ITwitchClient twitchClient)
            : base(new []{ "Menu", "MenuColor", "Window", "Windows" })
        {
            _menuColorAccessor = menuColorAccessor;
            _twitchClient = twitchClient;
        }

        public override void Execute(CommandData commandData)
        {
            if (!MenuSettings.Enabled) return;
            if (commandData.Bits < MenuSettings.BitCost)
            {
                var message = $"Sorry, '!{commandData.CommandText}' has a minimum cheer cost of {MenuSettings.BitCost}.";
                _twitchClient.SendMessage(commandData.Channel, message);
                return;
            }

            MenuColors menuColors = GetMenuColorsFromArgs(commandData.Arguments);

            if (menuColors != null)
            {
                DomainEvents.Raise(new MenuColorChanging(menuColors));

                _menuColorAccessor.SetMenuColors(ProcessName, menuColors);
            }
        }

        private static MenuColors GetMenuColorsFromArgs(List<string> args)
        {
            var specialColor = GetSpecialColor(args.FirstOrDefault());
            if (specialColor != null)
            {
                return specialColor;
            }
            List<string> colorArgs = args.Where(arg => arg.IsColor()).ToList();
            var menuColors = new MenuColors();

            switch (colorArgs.Count)
            {
                case 1:
                    Color hexColor = colorArgs[0].ToColor();
                    menuColors.TopLeft = hexColor;
                    menuColors.TopRight = hexColor;
                    menuColors.BotLeft = hexColor;
                    menuColors.BotRight = hexColor;
                    break;
                case 4:
                    menuColors.TopLeft = colorArgs[0].ToColor();
                    menuColors.TopRight = colorArgs[1].ToColor();
                    menuColors.BotLeft = colorArgs[2].ToColor();
                    menuColors.BotRight = colorArgs[3].ToColor();
                    break;
                default:
                    // Invalid case, do nothing.
                    return null;
            }

            return menuColors;
        }

        private static MenuColors GetSpecialColor(string firstArg)
        {
            switch (firstArg.ToLower())
            {
                case "tsunamods":
                case "tsunamix":
                case "tsuna":
                    return MenuColors.tsuna;
                case "brendan":
                case "brendoneus":
                case "devchatter":
                    return MenuColors.brendan;
                case "classic":
                case "default":
                case "base":
                    return MenuColors.classic;
                case "strife":
                    return MenuColors.strife;
                default:
                    return null;
            }
        }
    }
}