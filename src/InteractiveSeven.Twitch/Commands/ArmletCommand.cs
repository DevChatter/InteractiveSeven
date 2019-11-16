using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Twitch.Payments;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class ArmletCommand : EquipmentCommand<Armlet>
    {
        public ArmletCommand(IEquipmentAccessor equipmentAccessor,
            IInventoryAccessor inventoryAccessor, IMateriaAccessor materiaAccessor,
            GameDatabase gameDatabase, GilBank gilBank, ITwitchClient twitchClient,
            EquipmentData<Armlet> equipmentData, PaymentProcessor paymentProcessor)
            : base(equipmentAccessor, inventoryAccessor, materiaAccessor, gameDatabase, gilBank,
                twitchClient, equipmentData, x => x.ArmletCommandWords, paymentProcessor)
        {
        }
    }
}