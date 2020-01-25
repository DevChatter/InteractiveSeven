using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Twitch.Payments;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class AccessoryCommand : EquipmentCommand<Accessory>
    {
        public AccessoryCommand(IEquipmentAccessor equipmentAccessor,
            IInventoryAccessor inventoryAccessor, IMateriaAccessor materiaAccessor,
            IStatusHubEmitter statusHubEmitter,
            GameDatabase gameDatabase, GilBank gilBank, ITwitchClient twitchClient,
            EquipmentData<Accessory> equipmentData, PaymentProcessor paymentProcessor)
            : base(equipmentAccessor, inventoryAccessor, materiaAccessor, statusHubEmitter,
                gameDatabase, gilBank, twitchClient, equipmentData,
                x => x.AccessoryCommandWords, paymentProcessor)
        {
        }
    }
}