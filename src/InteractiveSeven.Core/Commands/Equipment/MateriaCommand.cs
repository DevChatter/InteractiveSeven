using System.Linq;
using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Payments;

namespace InteractiveSeven.Core.Commands.Equipment
{
    public class MateriaCommand : BaseCommand
    {
        private readonly IChatClient _chatClient;
        private readonly IMateriaAccessor _materiaAccessor;
        private readonly IStatusHubEmitter _statusHubEmitter;
        private readonly PaymentProcessor _paymentProcessor;

        public MateriaCommand(IChatClient chatClient, IMateriaAccessor materiaAccessor,
            IStatusHubEmitter statusHubEmitter, PaymentProcessor paymentProcessor)
            : base(x => x.MateriaCommandWords, x => x.MateriaSettings.Enabled)
        {
            _chatClient = chatClient;
            _materiaAccessor = materiaAccessor;
            _statusHubEmitter = statusHubEmitter;
            _paymentProcessor = paymentProcessor;
        }

        public override async Task Execute(CommandData commandData)
        {
            string materiaName = commandData.Arguments.Count == 1
                ? commandData.Arguments.FirstOrDefault()
                : string.Join(' ', commandData.Arguments);

            var candidates = Settings.MateriaSettings.AllByName(materiaName);

            if (candidates.Count == 0)
            {
                await _chatClient.SendMessage(commandData.Channel, "Error: No matching Materia.");
                return;
            }

            if (candidates.Count > 15)
            {
                await _chatClient.SendMessage(commandData.Channel, "Error: Too many matching materia, be more specific.");
                return;
            }

            if (candidates.Count > 1)
            {
                string matches = string.Join(", ", candidates.Select(x => x.Name.NoSpaces()));
                await _chatClient.SendMessage(commandData.Channel, $"Error: matched ({matches})");
                return;
            }

            var materiaSetting = candidates.Single();

            GilTransaction gilTransaction = await _paymentProcessor.ProcessPayment(
                commandData, materiaSetting.Cost, Settings.EquipmentSettings.AllowModOverride);

            if (!gilTransaction.Paid)
            {
                return;
            }

            _materiaAccessor.AddMateria(materiaSetting.Materia.Value);
            string message = $"Materia {materiaSetting.Materia.Name} Added";
            await _chatClient.SendMessage(commandData.Channel, message);
            await _statusHubEmitter.ShowEvent(message);
        }
    }
}
