﻿using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.ViewModels;

namespace InteractiveSeven.Core.Commands.Equipment
{
    public class WeaponCommand : EquipmentCommand<Weapon>
    {
        public WeaponCommand(IEquipmentAccessor equipmentAccessor,
            IInventoryAccessor inventoryAccessor, IMateriaAccessor materiaAccessor,
            IStatusHubEmitter statusHubEmitter, PartyStatusViewModel partyStatusViewModel,
            GameDatabase gameDatabase, GilBank gilBank,
            EquipmentData<Weapon> equipmentData, PaymentProcessor paymentProcessor)
            : base(equipmentAccessor, inventoryAccessor, materiaAccessor, statusHubEmitter,
                partyStatusViewModel, gameDatabase, gilBank, equipmentData,
                x => x.WeaponCommandWords, paymentProcessor)
        {
        }
    }
}
