using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Model;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Twitch.Model;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class ArmletCommand : BaseCommand
    {
        private readonly IEquipmentAccessor _equipmentAccessor;
        private readonly IInventoryAccessor _inventoryAccessor;
        private readonly IMateriaAccessor _materiaAccessor;
        private readonly GilBank _gilBank;
        private readonly ITwitchClient _twitchClient;

        public ArmletCommand(IEquipmentAccessor equipmentAccessor,
            IInventoryAccessor inventoryAccessor, IMateriaAccessor materiaAccessor,
            GilBank gilBank, ITwitchClient twitchClient)
            : base(x => x.ArmletCommandWords, x => x.EquipmentSettings.Enabled)
        {
            _equipmentAccessor = equipmentAccessor;
            _inventoryAccessor = inventoryAccessor;
            _materiaAccessor = materiaAccessor;
            _gilBank = gilBank;
            _twitchClient = twitchClient;
        }

        public override void Execute(CommandData commandData)
        {
            (bool isValidName, CharNames charName) =
                CharNames.GetByName(commandData.Arguments.FirstOrDefault());
            var armletText = commandData.Arguments.ElementAtOrDefault(1);

            if (!isValidName
                || !int.TryParse(armletText ?? "", out int armletId)
                || !Armlets.IsValid(armletId))
            {
                _twitchClient.SendMessage(commandData.Channel,
                    "Invalid Request - Specify character and armlet number like this !armlet cloud 15");
                return;
            }

            (int balance, int withdrawn) = (0, 0);
            if (!CanOverrideBitRestriction(commandData.User))
            {
                const int cost = 100; // TODO: Configurable Costs
                (balance, withdrawn) = _gilBank.Withdraw(commandData.User, cost, true);
                if (withdrawn < cost)
                {
                    _twitchClient.SendMessage(commandData.Channel,
                        $"Insufficient gil. You only have {balance} gil and needed {cost}");
                    return;
                }
            }

            Armlets armlet = Armlets.GetArmlet(armletId);
            int existingArmletId = _equipmentAccessor.GetCharacterArmlet(charName);
            if (armlet.Value == existingArmletId)
            {
                _twitchClient.SendMessage(commandData.Channel,
                    $"Sorry, {charName.DefaultName} already has {armlet.Name} equipped.");
                if (withdrawn > 0) // return the gil, since we did nothing
                {
                    _gilBank.Deposit(commandData.User, withdrawn);
                }
                return;
            }

            Armlets removedArmlet = Armlets.GetArmlet(existingArmletId);
            _equipmentAccessor.SetCharacterArmlet(charName, armlet.Value);
            _inventoryAccessor.AddItem(removedArmlet.ItemId, 1, true);
            _materiaAccessor.RemoveArmletMateria(charName);
            _twitchClient.SendMessage(commandData.Channel,
                $"Equipped {charName.DefaultName} with a {armlet.Name}.");
        }

        private bool CanOverrideBitRestriction(ChatUser user)
            => (Settings.EquipmentSettings.AllowModOverride && user.IsMod)
               || user.IsMe || user.IsBroadcaster;
    }
}