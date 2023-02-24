using System.Linq;
using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Models;

namespace InteractiveSeven.Core.Commands.MenuColors
{
    public class PaletteCommand : BaseCommand
    {
        private readonly IDataStore<ColorPalette> _colorPaletteDataStore;
        private readonly ColorPaletteCollection _colorPaletteCollection;

        public PaletteCommand(IDataStore<ColorPalette> colorPaletteDataStore,
            ColorPaletteCollection colorPaletteCollection)
            : base(x => x.PaletteCommandWords,
                x => x.MenuSettings.EnablePaletteCommand)
        {
            _colorPaletteDataStore = colorPaletteDataStore;
            _colorPaletteCollection = colorPaletteCollection;
        }

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.DisplayOnly;

        public override async Task Execute(CommandData commandData, IChatClient chatClient)
        {
            string operation = commandData.Arguments.FirstOrDefault();

            switch (operation?.ToLower())
            {
                case "all":
                case "show":
                case "list":
                case "l":
                    await ShowCurrentPaletteList(commandData, chatClient);
                    break;
                case "new":
                case "add":
                case "create":
                    await CreateNewPalette(commandData, chatClient);
                    break;
                case "edit":
                case "mod":
                case "change":
                case "modify":
                    await EditPalette(commandData, chatClient);
                    break;
                case "rm":
                case "rem":
                case "remove":
                case "del":
                case "delete":
                    await DeletePalette(commandData, chatClient);
                    break;
                default:
                    await DisplayHelpText(commandData, chatClient);
                    break;
            }
        }

        private async Task DeletePalette(CommandData commandData, IChatClient chatClient)
        {
            if (!AllowedToRunCommand(commandData))
            {
                return;
            }

            if (commandData.Arguments.Count < 2)
            {
                await DisplayHelpText(commandData, chatClient);
                return;
            }

            string paletteName = commandData.Arguments[1];

            _colorPaletteCollection.RemovePalette(paletteName);
            _colorPaletteDataStore.SaveData(_colorPaletteCollection.All);

            await chatClient.SendMessage(commandData.Channel, $"Removed the '!menu {paletteName}' color palette.");
        }

        private async Task EditPalette(CommandData commandData, IChatClient chatClient)
        {
            if (!AllowedToRunCommand(commandData))
            {
                return;
            }

            if (commandData.Arguments.Count < 6)
            {
                await DisplayHelpText(commandData, chatClient);
                return;
            }

            string paletteName = commandData.Arguments[1];
            Models.MenuColors menuColors = GetMenuColors(commandData);

            if (menuColors == null)
            {
                await chatClient.SendMessage(commandData.Channel, "One of those colors was invalid.");
                return;
            }

            _colorPaletteCollection.EditPalette(menuColors, paletteName);
            _colorPaletteDataStore.SaveData(_colorPaletteCollection.All);

            await chatClient.SendMessage(commandData.Channel, $"Edited the '!menu {paletteName}' color palette.");
        }

        private static Models.MenuColors GetMenuColors(CommandData commandData)
        {
            string topLeftText = commandData.Arguments[2];
            string topRightText = commandData.Arguments[3];
            string botLeftText = commandData.Arguments[4];
            string botRightText = commandData.Arguments[5];

            if (!topLeftText.IsColor()
                || !topRightText.IsColor()
                || !botLeftText.IsColor()
                || !botRightText.IsColor())
            {
                return null;
            }

            var menuColors = new Models.MenuColors
            {
                TopLeft = topLeftText.ToColor(),
                TopRight = topRightText.ToColor(),
                BotLeft = botLeftText.ToColor(),
                BotRight = botRightText.ToColor(),
            };
            return menuColors;
        }

        private async Task DisplayHelpText(CommandData commandData, IChatClient chatClient)
        {
            const string word = "palette";
            string message = $"List available palettes by '!{word} list' or add a new one by '!{word} add Sunset Orange OrangeRed Red DarkRed'.";
            await chatClient.SendMessage(commandData.Channel, message);
        }

        private async Task CreateNewPalette(CommandData commandData, IChatClient chatClient)
        {
            if (!AllowedToRunCommand(commandData))
            {
                return;
            }

            if (commandData.Arguments.Count < 6)
            {
                await DisplayHelpText(commandData, chatClient);
                return;
            }

            string paletteName = commandData.Arguments[1];
            Models.MenuColors menuColors = GetMenuColors(commandData);

            if (menuColors == null)
            {
                await chatClient.SendMessage(commandData.Channel, "One of those colors was invalid.");
                return;
            }

            var colorPalette = new ColorPalette(menuColors, paletteName);

            _colorPaletteCollection.AddPalette(colorPalette);
            _colorPaletteDataStore.SaveData(_colorPaletteCollection.All);

            await chatClient.SendMessage(commandData.Channel, $"Added a '!menu {paletteName}' color palette.");
        }

        private bool AllowedToRunCommand(CommandData commandData)
        {
            return commandData.User.IsBroadcaster || commandData.User.IsMe
                || (commandData.User.IsMod && Settings.MenuSettings.AllowModsToCreatePalettes);
        }

        private async Task ShowCurrentPaletteList(CommandData commandData, IChatClient chatClient)
        {
            string message = string.Join(", ", _colorPaletteCollection.All.Select(x => x.Names.First()));
            await chatClient.SendMessage(commandData.Channel, message);
        }
    }
}
