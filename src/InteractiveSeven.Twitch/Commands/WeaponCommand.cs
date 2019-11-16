using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Twitch.Payments;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class WeaponCommand : EquipmentCommand<Weapon>
    {
        public WeaponCommand(IEquipmentAccessor equipmentAccessor,
            IInventoryAccessor inventoryAccessor, IMateriaAccessor materiaAccessor,
            GameDatabase gameDatabase, GilBank gilBank, ITwitchClient twitchClient,
            EquipmentData<Weapon> equipmentData, PaymentProcessor paymentProcessor)
            : base(equipmentAccessor, inventoryAccessor, materiaAccessor, gameDatabase, gilBank,
                twitchClient, equipmentData, x => x.WeaponCommandWords, paymentProcessor)
        {
        }
    }
}