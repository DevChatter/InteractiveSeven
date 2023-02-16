using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Commands.MenuColors
{
    public class MenuCommand : BaseCommand
    {
        private readonly ColorPaletteCollection _paletteCollection;
        private readonly PaymentProcessor _paymentProcessor;

        private MenuColorSettings MenuSettings => ApplicationSettings.Instance.MenuSettings;

        public MenuCommand(ColorPaletteCollection paletteCollection, PaymentProcessor paymentProcessor)
            : base(x => x.MenuCommandWords, x => x.MenuSettings.Enabled)
        {
            _paletteCollection = paletteCollection;
            _paymentProcessor = paymentProcessor;
        }

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.DisplayOnly;

        public override void Execute(in CommandData commandData)
        {
            if (commandData.Arguments.Count == 0) return;

            Models.MenuColors menuColors = GetMenuColorsFromArgs(commandData.Arguments);

            if (menuColors == null) return;

            var gilTransaction = _paymentProcessor.ProcessPayment(commandData,
                MenuSettings.BitCost, MenuSettings.AllowModOverride);

            if (!gilTransaction.Paid) return;

            DomainEvents.Raise(new MenuColorChanging(menuColors, commandData.User, gilTransaction.AmountPaid));
        }

        private Models.MenuColors GetMenuColorsFromArgs(List<string> args)
        {
            var specialColor = GetSpecialColor(args.FirstOrDefault());
            if (specialColor != null)
            {
                return specialColor;
            }
            List<string> colorArgs = args.Where(arg => arg.IsColor()).ToList();
            var menuColors = new Models.MenuColors();

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

        private Models.MenuColors GetSpecialColor(string firstArg)
        {
            if (firstArg.EqualsIns("random"))
            {
                return Models.MenuColors.RandomPalette();
            }

            return _paletteCollection.ByName(firstArg);
        }
    }
}
