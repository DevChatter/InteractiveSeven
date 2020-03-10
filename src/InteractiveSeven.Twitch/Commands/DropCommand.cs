using InteractiveSeven.Core;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Model;
using InteractiveSeven.Twitch.Payments;
using System.Linq;
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
                    DropItem(itemSettings, commandData);
                    break;
                case SpecificMateriaSettings materiaSettings:
                    DropMateria(materiaSettings, commandData);
                    break;
            }
        }

        private void DropItem(SpecificItemSettings itemSettings, CommandData commandData)
        {
            Items item = itemSettings.Item;

            if (!_inventoryAccessor.HasItem(item.ItemId))
            {
                string message = $"Player out of {item.Name}s.";
                _twitchClient.SendMessage(commandData.Channel, message);
                return;
            }

            GilTransaction gilTransaction = _paymentProcessor.ProcessPayment(
                commandData, itemSettings.DropCost, Settings.EquipmentSettings.AllowModOverride);

            if (!gilTransaction.Paid)
            {
                return;
            }

            bool dropped = _inventoryAccessor.DropItem(item.ItemId, 1);
            if (dropped)
            {
                string message = $"Item {item.Name} Dropped";
                _twitchClient.SendMessage(commandData.Channel, message);
                _statusHubEmitter.ShowEvent(message);
            }
            else
            {
                string message = $"Player out of {item.Name}s.";
                _twitchClient.SendMessage(commandData.Channel, message);
            }
        }

        private void DropMateria(SpecificMateriaSettings materiaSettings, CommandData commandData)
        {
            Materia materia = materiaSettings.Materia;

            if (!_materiaAccessor.HasMateria(materia.Value))
            {
                string message = $"Player out of {materia.Name}s.";
                _twitchClient.SendMessage(commandData.Channel, message);
                return;
            }

            GilTransaction gilTransaction = _paymentProcessor.ProcessPayment(
                commandData, materiaSettings.DropCost, Settings.EquipmentSettings.AllowModOverride);

            if (!gilTransaction.Paid)
            {
                return;
            }

            bool dropped = _materiaAccessor.DropMateria(materia.Value);
            if (dropped)
            {
                string message = $"Materia {materia.Name} Dropped";
                _twitchClient.SendMessage(commandData.Channel, message);
                _statusHubEmitter.ShowEvent(message);
            }
            else
            {
                string message = $"Player out of {materia.Name}s.";
                _twitchClient.SendMessage(commandData.Channel, message);
            }
        }
    }
}