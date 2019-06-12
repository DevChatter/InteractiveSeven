using FluentAssertions;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Commands;
using InteractiveSeven.Twitch.Model;
using Microsoft.Extensions.Logging;
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
            bool called = false;
            DomainEvents.Clear();
            DomainEvents.Register<MenuColorChanging>(x => called = true);
            SetSettings(true, 0);
            var (menuColorAcc, twitchClient, menuCommand) = SetUpTest();
            var commandData = new CommandData
            {
                Arguments = new List<string> { "red" },
            };

            menuCommand.Execute(commandData);


            called.Should().BeTrue();
        }

        [Fact]
        public void DoNothing_GivenNotEnoughBits()
        {
            bool called = false;
            DomainEvents.Clear();
            DomainEvents.Register<MenuColorChanging>(x => called = true);
            SetSettings(true, 1);
            var (menuColorAcc, twitchClient, menuCommand) = SetUpTest();
            var commandData = new CommandData
            {
                Arguments = new List<string> { "red" },
            };

            menuCommand.Execute(commandData);

            called.Should().BeFalse();
        }

        [Fact]
        public void SetColor_GivenEnoughBits()
        {
            bool called = false;
            DomainEvents.Clear();
            DomainEvents.Register<MenuColorChanging>(x => called = true);
            SetSettings(true, 1);
            var (menuColorAcc, twitchClient, menuCommand) = SetUpTest();
            var commandData = new CommandData
            {
                Arguments = new List<string> { "red", "Cheer1" },
                Bits = 1,
                Message = "!menu red Cheer1"
            };

            menuCommand.Execute(commandData);

            called.Should().BeTrue();
        }

        [Fact]
        public void SetColor_GivenModWithoutEnoughBits()
        {
            bool called = false;
            DomainEvents.Clear();
            DomainEvents.Register<MenuColorChanging>(x => called = true);
            SetSettings(true, 1);
            var (menuColorAcc, twitchClient, menuCommand) = SetUpTest();
            var commandData = new CommandData
            {
                IsMod = true,
                Arguments = new List<string> { "red" },
            };

            menuCommand.Execute(commandData);

            called.Should().BeTrue();
        }

        [Fact]
        public void DoNothing_GivenNotEnoughBitsAndModOverrideTurnedOff()
        {
            bool called = false;
            DomainEvents.Clear();
            DomainEvents.Register<MenuColorChanging>(x => called = true);
            SetSettings(true, 1, false);
            var (menuColorAcc, twitchClient, menuCommand) = SetUpTest();
            var commandData = new CommandData
            {
                IsMod = true,
                Arguments = new List<string> { "red" },
            };

            menuCommand.Execute(commandData);

            called.Should().BeFalse();
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
            var logger = new Mock<ILogger<ColorPaletteCollection>>();
            var menuCommand = new MenuCommand(twitchClient.Object, new ColorPaletteCollection(logger.Object), new GilBank());
            return (menuColorAccessor, twitchClient, menuCommand);
        }
    }
}