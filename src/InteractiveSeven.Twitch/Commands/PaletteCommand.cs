using InteractiveSeven.Core;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Twitch.Model;
using System.Linq;
using InteractiveSeven.Core.Data;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class PaletteCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;
        private readonly IDataStore<ColorPalette> _colorPaletteDataStore;
        private readonly ColorPaletteCollection _colorPaletteCollection;

        public PaletteCommand(ITwitchClient twitchClient,
            IDataStore<ColorPalette> colorPaletteDataStore,
            ColorPaletteCollection colorPaletteCollection)
            : base(x => x.PaletteCommandWords,
                x => x.MenuSettings.EnablePaletteCommand)
        {
            _twitchClient = twitchClient;
            _colorPaletteDataStore = colorPaletteDataStore;
            _colorPaletteCollection = colorPaletteCollection;
        }

        public override void Execute(in CommandData commandData)
        {
            string operation = commandData.Arguments.FirstOrDefault();

            switch (operation?.ToLower())
            {
                case "all":
                case "show":
                case "list":
                case "l":
                    ShowCurrentPaletteList(commandData);
                    break;
                case "new":
                case "add":
                case "create":
                    CreateNewPalette(commandData);
                    break;
                default:
                    DisplayHelpText(commandData);
                    break;
            }
        }

        private void DisplayHelpText(CommandData commandData)
        {
            const string word = "palette";
            string message =
                $"List available palettes by '!{word} list' or add a new one by '!{word} add Sunset Orange OrangeRed Red DarkRed'.";
            _twitchClient.SendMessage(commandData.Channel, message);
        }

        private void CreateNewPalette(CommandData commandData)
        {
            if (!AllowedToRunCommand(commandData))
            {
                return;
            }

            if (commandData.Arguments.Count < 6)
            {
                DisplayHelpText(commandData);
                return;
            }

            string paletteName = commandData.Arguments[1];
            string topLeftText = commandData.Arguments[2];
            string topRightText = commandData.Arguments[3];
            string botLeftText = commandData.Arguments[4];
            string botRightText = commandData.Arguments[5];

            if (!topLeftText.IsColor() || !topRightText.IsColor() || !botLeftText.IsColor() ||
                !botRightText.IsColor())
            {
                _twitchClient.SendMessage(commandData.Channel, "One of those colors was invalid.");
                return;
            }

            var menuColors = new MenuColors
            {
                TopLeft = topLeftText.ToColor(),
                TopRight = topRightText.ToColor(),
                BotLeft = botLeftText.ToColor(),
                BotRight = botRightText.ToColor(),
            };
            var colorPalette = new ColorPalette(menuColors, paletteName);

            _colorPaletteCollection.AddPalette(colorPalette);
            _colorPaletteDataStore.SaveData(_colorPaletteCollection.All);

            _twitchClient.SendMessage(commandData.Channel, $"Added a '!menu {paletteName}' color palette.");
        }

        private bool AllowedToRunCommand(CommandData commandData)
        {
            return commandData.User.IsBroadcaster || commandData.User.IsMe
                || (commandData.User.IsMod && Settings.MenuSettings.AllowModsToCreatePalettes);
        }

        private void ShowCurrentPaletteList(CommandData commandData)
        {
            string message = string.Join(", ", _colorPaletteCollection.All.Select(x => x.Names.First()));
            _twitchClient.SendMessage(commandData.Channel, message);
        }
    }
}