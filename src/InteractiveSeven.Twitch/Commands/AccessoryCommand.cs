using System.Linq;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Model;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Twitch.Model;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class AccessoryCommand : BaseCommand
    {
        private readonly IEquipmentAccessor _equipmentAccessor;
        private readonly GilBank _gilBank;
        private readonly ITwitchClient _twitchClient;

        public AccessoryCommand(IEquipmentAccessor equipmentAccessor,
            GilBank gilBank, ITwitchClient twitchClient)
            : base(x => x.AccessoryCommandWords, x => x.EquipmentSettings.Enabled)
        {
            _equipmentAccessor = equipmentAccessor;
            _gilBank = gilBank;
            _twitchClient = twitchClient;
        }

        public override void Execute(CommandData commandData)
        {
            (bool isValidName, CharNames charName) =
                CharNames.GetByName(commandData.Arguments.FirstOrDefault());
            var accessoryText = commandData.Arguments.ElementAtOrDefault(1);

            if (!isValidName
                || !int.TryParse(accessoryText ?? "", out int accessoryId)
                || !Accessories.IsValid(accessoryId))
            {
                _twitchClient.SendMessage(commandData.Channel,
                    "Invalid Request - Specify character and accessory number like this !accessory cloud 15");
                return;
            }

            if (!CanOverrideBitRestriction(commandData.User))
            {
                const int cost = 100;
                (int balance, int withdrawn) = _gilBank.Withdraw(commandData.User, cost, true);
                if (withdrawn < cost)
                {
                    _twitchClient.SendMessage(commandData.Channel,
                        $"Insufficient gil. You only have {balance} gil and needed {cost}");
                    return;
                }
            }

            Accessories accessory = Accessories.Get(accessoryId);
            _equipmentAccessor.SetCharacterAccessory(charName, accessory.Value);
        }

        private bool CanOverrideBitRestriction(ChatUser user)
            => (Settings.EquipmentSettings.AllowModOverride && user.IsMod)
               || user.IsMe || user.IsBroadcaster;
    }
}