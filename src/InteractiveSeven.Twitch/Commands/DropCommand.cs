using System;
using System.Linq;
using InteractiveSeven.Core;
using InteractiveSeven.Core.Commands;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Settings;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class DropCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;
        private readonly IInventoryAccessor _inventoryAccessor;
        private readonly IMateriaAccessor _materiaAccessor;
        private readonly IStatusHubEmitter _statusHubEmitter;
        private readonly PaymentProcessor _paymentProcessor;

        public DropCommand(ITwitchClient twitchClient, IInventoryAccessor inventoryAccessor,
            IMateriaAccessor materiaAccessor, IStatusHubEmitter statusHubEmitter,
            PaymentProcessor paymentProcessor)
            : base(x => x.DropCommandWords, x => x.ItemSettings.Enabled)
        {
            _twitchClient = twitchClient;
            _inventoryAccessor = inventoryAccessor;
            _statusHubEmitter = statusHubEmitter;
            _paymentProcessor = paymentProcessor;
            _materiaAccessor = materiaAccessor;
        }


        public override void Execute(in CommandData commandData)
        {
            string name = commandData.Arguments.FirstOrDefault();

            var candidates = Settings.ItemSettings.AllByName(name).OfType<INamedSetting>()
                .Union(Settings.MateriaSettings.AllByName(name)).ToList();

            if (candidates.Count == 0)
            {
                _twitchClient.SendMessage(commandData.Channel, "Error: No matching Item.");
                return;
            }

            if (candidates.Count > 15)
            {
                _twitchClient.SendMessage(commandData.Channel, "Error: Too many matching items, be more specific.");
                return;
            }

            if (candidates.Count > 1)
            {
                string matches = string.Join(", ", candidates.Select(x => x.Name.NoSpaces()));
                _twitchClient.SendMessage(commandData.Channel, $"Error: matched ({matches})");
                return;
            }


            switch (candidates.Single())
            {
                case SpecificItemSettings itemSettings:
                    TryToDrop(itemSettings.ItemId, itemSettings.Name,
                        itemSettings.DropCost, commandData, "Item",
                        x => _inventoryAccessor.HasItem(x),
                        x => _inventoryAccessor.DropItem(x, 1));
                    break;

                case SpecificMateriaSettings materiaSettings:
                    TryToDrop(materiaSettings.MateriaId, materiaSettings.Name,
                        materiaSettings.DropCost, commandData, "Materia",
                        x => _materiaAccessor.HasMateria((byte)x),
                        x => _materiaAccessor.DropMateria((byte)x));
                    break;
            }
        }

        private void TryToDrop(ushort id, string name, int cost,
            CommandData commandData, string typeName,
            Func<ushort, bool> hasIt, Func<ushort, bool> dropIt)
        {
            if (!hasIt(id))
            {
                string message = $"Player out of {name}s.";
                _twitchClient.SendMessage(commandData.Channel, message);
                return;
            }

            GilTransaction gilTransaction = _paymentProcessor.ProcessPayment(
                commandData, cost, Settings.EquipmentSettings.AllowModOverride);

            if (!gilTransaction.Paid) return;

            if (dropIt(id))
            {
                string message = $"{typeName} {name} Dropped";
                _twitchClient.SendMessage(commandData.Channel, message);
                _statusHubEmitter.ShowEvent(message);
            }
            else
            {
                string message = $"Player out of {name}s.";
                _twitchClient.SendMessage(commandData.Channel, message);
            }
        }
    }
}
