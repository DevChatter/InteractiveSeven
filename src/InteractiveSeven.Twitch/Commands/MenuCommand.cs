using InteractiveSeven.Core;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class MenuCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;
        private readonly ColorPaletteCollection _paletteCollection;
        private readonly GilBank _gilBank;
        private static readonly Random Rand = new Random();

        private MenuColorSettings MenuSettings => ApplicationSettings.Instance.MenuSettings;

        public MenuCommand(ITwitchClient twitchClient, ColorPaletteCollection paletteCollection, GilBank gilBank)
            : base(new[] { "Menu", "MenuColor", "Window", "Windows" }, x => x.MenuSettings.Enabled)
        {
            _twitchClient = twitchClient;
            _paletteCollection = paletteCollection;
            _gilBank = gilBank;
        }

        public override void Execute(CommandData commandData)
        {
            if (BelowBitThreshold(commandData) && !CanOverrideBitRestriction(commandData))
            {
                var message = $"Sorry, '!{commandData.CommandText}' has a minimum cheer cost of {MenuSettings.BitCost}.";
                _twitchClient.SendMessage(commandData.Channel, message);
                return;
            }

            MenuColors menuColors = GetMenuColorsFromArgs(commandData.Arguments);

            if (menuColors != null)
            {
                DomainEvents.Raise(new MenuColorChanging(menuColors, commandData.Username));
            }
        }

        private bool CanOverrideBitRestriction(CommandData commandData)
            => MenuSettings.AllowModOverride && (commandData.IsMod || commandData.IsBroadcaster);

        private bool BelowBitThreshold(CommandData commandData)
            => commandData.Bits < MenuSettings.BitCost;

        private MenuColors GetMenuColorsFromArgs(List<string> args)
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

        private MenuColors GetSpecialColor(string firstArg)
        {
            if (firstArg.EqualsIns("random"))
            {
                return RandomPalette();
            }

            return _paletteCollection.ByName(firstArg);
        }

        private static MenuColors RandomPalette()
        {
            return new MenuColors
            {
                TopLeft = GetRandomColor(),
                TopRight = GetRandomColor(),
                BotLeft = GetRandomColor(),
                BotRight = GetRandomColor()
            };
            Color GetRandomColor()
            {
                byte[] b = new byte[3];
                Rand.NextBytes(b);
                return Color.FromArgb(b[0], b[1], b[2]);
            }
        }
    }
}