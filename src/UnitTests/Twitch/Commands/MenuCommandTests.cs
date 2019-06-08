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

        [Fact]
        public void SetColor_GivenModWithoutEnoughBits()
        {
            SetSettings(true, 1);
            var (menuColorAcc, twitchClient, menuCommand) = SetUpTest();
            var commandData = new CommandData
            {
                IsMod = true,
                Arguments = new List<string> { "red" },
            };

            menuCommand.Execute(commandData);

            menuColorAcc.Verify(
                x => x.SetMenuColors(It.IsAny<string>(), It.IsAny<MenuColors>()), Times.Once);
        }

        [Fact]
        public void DoNothing_GivenNotEnoughBitsAndModOverrideTurnedOff()
        {
            SetSettings(true, 1, false);
            var (menuColorAcc, twitchClient, menuCommand) = SetUpTest();
            var commandData = new CommandData
            {
                IsMod = true,
                Arguments = new List<string> { "red" },
            };

            menuCommand.Execute(commandData);

            menuColorAcc.Verify(
                x => x.SetMenuColors(It.IsAny<string>(), It.IsAny<MenuColors>()), Times.Never);
        }


        private void SetSettings(bool enabled, int bits, bool allowOverride = true)
        {
            ApplicationSettings.Instance.MenuSettings.Enabled = enabled;
            ApplicationSettings.Instance.MenuSettings.BitCost = bits;
            ApplicationSettings.Instance.MenuSettings.AllowModOverride = allowOverride;
        }

        private static (Mock<IMenuColorAccessor>, Mock<ITwitchClient>, MenuCommand)
            SetUpTest()
        {
            var menuColorAccessor = new Mock<IMenuColorAccessor>();
            var twitchClient = new Mock<ITwitchClient>();
            var menuCommand = new MenuCommand(twitchClient.Object);
            return (menuColorAccessor, twitchClient, menuCommand);
        }
    }
}