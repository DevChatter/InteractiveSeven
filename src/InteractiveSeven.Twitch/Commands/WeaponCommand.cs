using InteractiveSeven.Core.Items;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Twitch.Model;
using System;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class WeaponCommand : BaseCommand
    {
        private readonly IEquipmentAccessor _equipmentAccessor;
        private readonly GilBank _gilBank;
        private readonly ITwitchClient _twitchClient;

        public WeaponCommand(IEquipmentAccessor equipmentAccessor,
            GilBank gilBank, ITwitchClient twitchClient)
            : base(x => new []{"weapon"}, x => true) // TODO: Wire up the settings
        {
            _equipmentAccessor = equipmentAccessor;
            _gilBank = gilBank;
            _twitchClient = twitchClient;
        }

        public override void Execute(CommandData commandData)
        {
            var characterName = commandData.Arguments.FirstOrDefault();
            var weaponText = commandData.Arguments.ElementAtOrDefault(1);

            if (!int.TryParse(weaponText ?? "", out int weaponId)
                || !Weapons.IsValid(characterName, weaponId))
            {
                _twitchClient.SendMessage(commandData.Channel, "Invalid Request - Specify character and weapon number like this !weapon cloud 15");
                return;
            }

            const int cost = 100;
            (int balance, int withdrawn) = _gilBank.Withdraw(commandData.User, cost, true);
            if (withdrawn < cost)
            {
                _twitchClient.SendMessage(commandData.Channel, $"Insufficient gil. You only have {balance} gil and needed {cost}");
                return;
            }

            Weapons weapon = Weapons.Get(characterName, weaponId);
            _equipmentAccessor.SetCharacterWeapon(characterName, weapon.Value);
        }
    }
}