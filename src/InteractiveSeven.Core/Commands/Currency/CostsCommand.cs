using System.Linq;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Commands.Currency
{
    public class CostsCommand : BaseCommand
    {
        private readonly IChatClient _chatClient;

        public CostsCommand(IChatClient chatClient)
            : base(x => x.CostsCommandWords, x => true)
        {
            _chatClient = chatClient;
        }

        private BattleSettings BattleSettings => Settings.BattleSettings;

        public override GamePlayEffects GamePlayEffects => GamePlayEffects.DisplayOnly;

        public override void Execute(in CommandData commandData)
        {
            var argument = commandData.Arguments.FirstOrDefault();
            if (argument is null)
            {
                var message = "Specify cost type to check (color, status, item, materia, equipment).";
                SendMessage(commandData, message);
            }
            else if (argument.StartsWithIns("col") || argument.StartsWithIns("menu"))
            {
                var message = GetColorCostMessage();
                SendMessage(commandData, message);
            }
            else if (argument.StartsWithIns("status") || argument.StartsWithIns("eff"))
            {
                if (BattleSettings.AllowStatusEffects)
                {
                    SendMessage(commandData, GetStatusCostMessage());
                    SendMessage(commandData, GetCureCostMessage());
                }
                else
                {
                    SendMessage(commandData, "Status Effects are Disabled.");
                }
            }
            else if (argument.StartsWithIns("item"))
            {
                if (Settings.ItemSettings.Enabled)
                {
                    SendMessage(commandData, GetItemCostMessage());
                    SendMessage(commandData, GetItemDropCostMessage());
                }
                else
                {
                    SendMessage(commandData, "Item Command is Disabled.");
                }
            }
            else if (argument.StartsWithIns("mat"))
            {
                if (Settings.MateriaSettings.Enabled)
                {
                    SendMessage(commandData, GetMateriaCostMessage());
                    SendMessage(commandData, GetMateriaDropCostMessage());
                }
                else
                {
                    SendMessage(commandData, "Materia Command is Disabled.");
                }

            }
            else if (argument.StartsWithIns("equip") || argument.StartsWithIns("wea")
                    || argument.StartsWithIns("arm") || argument.StartsWithIns("acc"))
            {
                if (Settings.EquipmentSettings.Enabled)
                {
                    SendMessage(commandData, "Equipment costs coming soon."); // TODO: Real data here.
                }
                else
                {
                    SendMessage(commandData, "Equipment Commands are Disabled.");
                }

            }
        }

        private string GetItemCostMessage()
        {
            var pieces = Settings.ItemSettings.AllItems
                .Where(x => x.Enabled)
                .Select(item => $"[{item.Name}: {item.Cost}]");

            return $"Item Costs: {string.Join(' ', pieces)}";
        }

        private string GetItemDropCostMessage()
        {
            var pieces = Settings.ItemSettings.AllItems
                .Where(x => x.Enabled)
                .Select(item => $"[{item.Name}: {item.DropCost}]");

            return $"Drop Item Costs: {string.Join(' ', pieces)}";
        }

        private string GetMateriaCostMessage()
        {
            var pieces = Settings.MateriaSettings.AllMateria
                .Where(x => x.Enabled)
                .Select(materia => $"[{materia.Name}: {materia.Cost}]");

            return $"Materia Costs: {string.Join(' ', pieces)}";
        }

        private string GetMateriaDropCostMessage()
        {
            var pieces = Settings.MateriaSettings.AllMateria
                .Where(x => x.Enabled)
                .Select(materia => $"[{materia.Name}: {materia.DropCost}]");

            return $"Drop Materia Costs: {string.Join(' ', pieces)}";
        }

        private string GetStatusCostMessage()
        {
            var pieces = BattleSettings.AllStatusEffects
                .Where(x => x.Enabled)
                .Select(effect => $"[{effect.Name}: {effect.Cost}]");

            return $"Status Costs: {string.Join(' ', pieces)}";
        }

        private string GetCureCostMessage()
        {
            var pieces = BattleSettings.AllStatusEffects
                .Where(x => x.Enabled)
                .Select(effect => $"[{effect.Name}: {effect.CureCost}]");

            return $"Cure Costs: {string.Join(' ', pieces)}";
        }

        private string GetColorCostMessage()
        {
            var message = "";

            if (Settings.MenuSettings.Enabled)
            {
                message += $"[Change Menu: {Settings.MenuSettings.BitCost}] ";

                if (Settings.MenuSettings.EnableMakoCommand)
                {
                    message += $"[Mako Mode: {Settings.MenuSettings.MakoModeCost}] ";
                }

                if (Settings.MenuSettings.EnableRainbowCommand)
                {
                    message += $"[Rainbow Mode: {Settings.MenuSettings.RainbowModeCost}] ";
                }

            }

            return message;
        }
        private void SendMessage(CommandData commandData, string message)
        {
            _chatClient.SendMessage(commandData.Channel, message);
        }
    }
}
