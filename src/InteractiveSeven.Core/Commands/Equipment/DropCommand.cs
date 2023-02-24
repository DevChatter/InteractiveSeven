using System;
using System.Linq;
using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Payments;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Commands.Equipment
{
    public class DropCommand : BaseCommand
    {
        private readonly IInventoryAccessor _inventoryAccessor;
        private readonly IMateriaAccessor _materiaAccessor;
        private readonly IStatusHubEmitter _statusHubEmitter;
        private readonly PaymentProcessor _paymentProcessor;

        public DropCommand(IInventoryAccessor inventoryAccessor,
            IMateriaAccessor materiaAccessor, IStatusHubEmitter statusHubEmitter,
            PaymentProcessor paymentProcessor)
            : base(x => x.DropCommandWords, x => x.ItemSettings.Enabled)
        {
            _inventoryAccessor = inventoryAccessor;
            _statusHubEmitter = statusHubEmitter;
            _paymentProcessor = paymentProcessor;
            _materiaAccessor = materiaAccessor;
        }


        public override async Task Execute(CommandData commandData, IChatClient chatClient)
        {
            string name = commandData.Arguments.Count == 1
                    ? commandData.Arguments.FirstOrDefault()
                    : string.Join(' ', commandData.Arguments);

            var candidates = Settings.ItemSettings.AllByName(name).OfType<INamedSetting>()
                .Union(Settings.MateriaSettings.AllByName(name)).ToList();

            if (candidates.Count == 0)
            {
                await chatClient.SendMessage(commandData.Channel, "Error: No matching Item.");
                return;
            }

            if (candidates.Count > 15)
            {
                await chatClient.SendMessage(commandData.Channel, "Error: Too many matching items, be more specific.");
                return;
            }

            if (candidates.Count > 1)
            {
                string matches = string.Join(", ", candidates.Select(x => x.Name.NoSpaces()));
                await chatClient.SendMessage(commandData.Channel, $"Error: matched ({matches})");
                return;
            }


            switch (candidates.Single())
            {
                case SpecificItemSettings itemSettings:
                    await TryToDrop(chatClient,
                        itemSettings.ItemId, itemSettings.Name,
                        itemSettings.DropCost, commandData, "Item",
                        x => _inventoryAccessor.HasItem(x),
                        x => _inventoryAccessor.DropItem(x, 1));
                    break;

                case SpecificMateriaSettings materiaSettings:
                    await TryToDrop(chatClient,
                        materiaSettings.MateriaId, materiaSettings.Name,
                        materiaSettings.DropCost, commandData, "Materia",
                        x => _materiaAccessor.HasMateria((byte)x),
                        x => _materiaAccessor.DropMateria((byte)x));
                    break;
            }
        }

        private async Task TryToDrop(IChatClient chatClient,
            ushort id, string name, int cost,
            CommandData commandData, string typeName,
            Func<ushort, bool> hasIt, Func<ushort, bool> dropIt)
        {
            if (!hasIt(id))
            {
                string message = $"Player out of {name}s.";
                await chatClient.SendMessage(commandData.Channel, message);
                return;
            }

            GilTransaction gilTransaction = await _paymentProcessor.ProcessPayment(
                commandData, cost, Settings.EquipmentSettings.AllowModOverride, chatClient);

            if (!gilTransaction.Paid) return;

            if (dropIt(id))
            {
                string message = $"{typeName} {name} Dropped";
                await chatClient.SendMessage(commandData.Channel, message);
                await _statusHubEmitter.ShowEvent(message);
            }
            else
            {
                string message = $"Player out of {name}s.";
                await chatClient.SendMessage(commandData.Channel, message);
            }
        }
    }
}
