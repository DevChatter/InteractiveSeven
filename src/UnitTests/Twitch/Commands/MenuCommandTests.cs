using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Commands;
using InteractiveSeven.Twitch.Model;
using Moq;
using System.Collections.Generic;
using TwitchLib.Client.Interfaces;
using Xunit;

namespace UnitTests.Twitch.Commands
{
    public class MenuCommandTests
    {
        [Fact]
        public void SetColors_GivenValidRequestNoBits()
        {
            SetSettings(true, 0);
            var (menuColorAcc, twitchClient, menuCommand) = SetUpTest();
            var commandData = new CommandData
            {
                Arguments = new List<string> { "red" },
            };

            menuCommand.Execute(commandData);

            menuColorAcc.Verify(
                x => x.SetMenuColors(It.IsAny<string>(), It.IsAny<MenuColors>()));
        }

        [Fact]
        public void DoNothing_GivenNotEnoughBits()
        {
            SetSettings(true, 1);
            var (menuColorAcc, twitchClient, menuCommand) = SetUpTest();
            var commandData = new CommandData
            {
                Arguments = new List<string> { "red" },
            };

            menuCommand.Execute(commandData);

            menuColorAcc.Verify(
                x => x.SetMenuColors(It.IsAny<string>(), It.IsAny<MenuColors>()), Times.Never);
        }

        [Fact]
        public void SetColor_GivenEnoughBits()
        {
            SetSettings(true, 1);
            var (menuColorAcc, twitchClient, menuCommand) = SetUpTest();
            var commandData = new CommandData
            {
                Arguments = new List<string> { "red", "Cheer1" },
                Bits = 1,
                Message = "!menu red Cheer1"
            };

            menuCommand.Execute(commandData);

            menuColorAcc.Verify(
                x => x.SetMenuColors(It.IsAny<string>(), It.IsAny<MenuColors>()), Times.Once);
        }

        private void SetSettings(bool enabled, int bits)
        {
            ApplicationSettings.Instance.MenuSettings.Enabled = enabled;
            ApplicationSettings.Instance.MenuSettings.BitCost = bits;
        }

        private static (Mock<IMenuColorAccessor>, Mock<ITwitchClient>, MenuCommand)
            SetUpTest()
        {
            var menuColorAccessor = new Mock<IMenuColorAccessor>();
            var twitchClient = new Mock<ITwitchClient>();
            var menuCommand = new MenuCommand(menuColorAccessor.Object, twitchClient.Object);
            return (menuColorAccessor, twitchClient, menuCommand);
        }
    }
}