using InteractiveSeven.Core;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Payments;
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
        private readonly ITwitchClient _twitchClient;
        private readonly ColorPaletteCollection _paletteCollection;
        private readonly GilBank _gilBank;

        private MenuColorSettings MenuSettings => ApplicationSettings.Instance.MenuSettings;

        public MenuCommand(ITwitchClient twitchClient, ColorPaletteCollection paletteCollection, GilBank gilBank)
            : base(x => x.MenuCommandWords, x => x.MenuSettings.Enabled)
        {
            _twitchClient = twitchClient;
            _paletteCollection = paletteCollection;
            _gilBank = gilBank;
        }

        public override void Execute(in CommandData commandData)
        {
            if (commandData.Arguments.Count == 0) return;

            MenuColors menuColors = GetMenuColorsFromArgs(commandData.Arguments);

            if (menuColors == null) return;

            var (paidFor, paidAmount) = ProcessPayment(commandData,
                $"Sorry, '!{commandData.CommandText}' has a minimum gil cost of {MenuSettings.BitCost}. Cheer for gil.",
                MenuSettings.BitCost, MenuSettings.AllowModOverride);

            if (!paidFor) return;

            DomainEvents.Raise(new MenuColorChanging(menuColors, commandData.User, paidAmount));
        }

        private (bool, int) ProcessPayment(CommandData commandData, string failMessage, int amount, bool canModsOverride)
        {
            int gilSpent = 0;
            bool requiresBits = !commandData.User.IsBroadcaster
                                && !commandData.User.IsMe
                                && (!canModsOverride || !commandData.User.IsMod);
            if (requiresBits)
            {
                (_, gilSpent) = _gilBank.Withdraw(commandData.User, amount, true);
                if (gilSpent < amount)
                {
                    _twitchClient.SendMessage(commandData.Channel, failMessage);
                    return (false, gilSpent);
                }
            }

            return (true, gilSpent);
        }

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
                return MenuColors.RandomPalette();
            }

            return _paletteCollection.ByName(firstArg);
        }
    }
}