using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.ViewModels;
using InteractiveSeven.Twitch.Payments;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class AccessoryCommand : EquipmentCommand<Accessory>
    {
        public AccessoryCommand(IEquipmentAccessor equipmentAccessor,
            IInventoryAccessor inventoryAccessor, IMateriaAccessor materiaAccessor,
            IStatusHubEmitter statusHubEmitter, PartyStatusViewModel partyStatusViewModel,
            GameDatabase gameDatabase, GilBank gilBank, ITwitchClient twitchClient,
            EquipmentData<Accessory> equipmentData, PaymentProcessor paymentProcessor)
            : base(equipmentAccessor, inventoryAccessor, materiaAccessor, statusHubEmitter,
                partyStatusViewModel, gameDatabase, gilBank, twitchClient, equipmentData,
                x => x.AccessoryCommandWords, paymentProcessor)
        {
        }
    }
}