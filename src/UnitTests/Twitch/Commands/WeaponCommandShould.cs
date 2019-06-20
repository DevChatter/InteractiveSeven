using System;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Model;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Twitch.Commands;
using InteractiveSeven.Twitch.Model;
using Moq;
using System.Linq;
using InteractiveSeven.Core.Data;
using TwitchLib.Client.Interfaces;
using Xunit;

namespace UnitTests.Twitch.Commands
{
    public class WeaponCommandShould
    {
        [Fact]
        public void ChangeCharacterWeapon_GivenValidCallAndEnoughGil()
        {
            var (characterName, weaponNumber) = (CharNames.Cloud.DefaultName, 1);
            var (commandData, gilBank, accessor, chat) = SetUpTest(1000, characterName, weaponNumber.ToString());
            var weaponCommand = new WeaponCommand(accessor.Object, gilBank, chat.Object);

            weaponCommand.Execute(commandData);

            accessor.Verify(x => x.SetCharacterWeapon(CharNames.Cloud, weaponNumber), Times.Once);
        }

        [Fact]
        public void ReportError_GivenInvalidCommandArgs()
        {
            var (commandData, gilBank, accessor, chat) = SetUpTest(1000, "cloud");
            var weaponCommand = new WeaponCommand(accessor.Object, gilBank, chat.Object);

            weaponCommand.Execute(commandData);

            chat.Verify(x => x.SendMessage(commandData.Channel, It.IsAny<string>(), false), Times.Once);
            accessor.Verify(x => x.SetCharacterWeapon(It.IsAny<CharNames>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ReportError_GivenInsufficientGil()
        {
            var (commandData, gilBank, accessor, chat) = SetUpTest(0, "cloud", "1");
            var weaponCommand = new WeaponCommand(accessor.Object, gilBank, chat.Object);

            weaponCommand.Execute(commandData);

            chat.Verify(x => x.SendMessage(commandData.Channel, It.IsAny<string>(), false), Times.Once);
            accessor.Verify(x => x.SetCharacterWeapon(It.IsAny<CharNames>(), It.IsAny<int>()), Times.Never);
        }

        private (CommandData data, GilBank gilBank,
            Mock<IEquipmentAccessor> accessor, Mock<ITwitchClient> chat)
            SetUpTest(int gil, params string[] args)
        {
            var mock = new Mock<IEquipmentAccessor>();
            var chat = new Mock<ITwitchClient>();
            var gilBank = new GilBank();
            var chatUser = new ChatUser { Username = "Fred" };
            gilBank.Deposit(chatUser, gil);
            var commandData = new CommandData
            {
                User = chatUser,
                Arguments = args.ToList()
            };
            return (commandData, gilBank, mock, chat);
        }
    }
}