using InteractiveSeven.Core;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using InteractiveSeven.Core.Model;
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
            MenuColors menuColors = GetMenuColorsFromArgs(commandData.Arguments);
            var colorChanging = new MenuColorChanging(menuColors, commandData.User, MenuSettings.BitCost);

            if (CanOverrideBitRestriction(commandData.User))
            {
                colorChanging.Gil = 0;
            }
            else if (BelowBitThreshold(commandData))
            {
                var message = $"Sorry, '!{commandData.CommandText}' has a minimum gil cost of {MenuSettings.BitCost}. Cheer for gil.";
                _twitchClient.SendMessage(commandData.Channel, message);
                return;
            }

            if (menuColors != null)
            {
                DomainEvents.Raise(colorChanging);
            }
        }

        private bool CanOverrideBitRestriction(ChatUser user)
            => MenuSettings.AllowModOverride && (user.IsMod || user.IsBroadcaster);

        private bool BelowBitThreshold(CommandData commandData)
            => _gilBank.CheckBalance(commandData.User) < MenuSettings.BitCost;

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