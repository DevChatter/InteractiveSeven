using InteractiveSeven.Core;
using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Model;
using InteractiveSeven.Twitch.Model;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class MateriaCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;
        private readonly IMateriaAccessor _materiaAccessor;
        private readonly IStatusHubEmitter _statusHubEmitter;

        public MateriaCommand(ITwitchClient twitchClient, IMateriaAccessor materiaAccessor,
            IStatusHubEmitter statusHubEmitter)
            : base(x => x.MateriaCommandWords, x => x.MateriaSettings.Enabled)
        {
            _twitchClient = twitchClient;
            _materiaAccessor = materiaAccessor;
            _statusHubEmitter = statusHubEmitter;
        }

        public override void Execute(in CommandData commandData)
        {
            if (!IsAllowedToUseCommand(commandData.User)) return;

            string materiaName = commandData.Arguments.FirstOrDefault();

            var candidates = Materia.All.Where(x => x.IsMatchByName(materiaName)).ToList(); // TODO: Make this lookup based on settings, not on the materia.

            if (candidates.Count == 0)
            {
                _twitchClient.SendMessage(commandData.Channel, "Error: No matching Materia.");
                return;
            }

            if (candidates.Count > 15)
            {
                _twitchClient.SendMessage(commandData.Channel, "Error: Too many matching materia, be more specific.");
                return;
            }

            if (candidates.Count > 1)
            {
                string matches = string.Join(", ", candidates.Select(x => x.Name.NoSpaces()));
                _twitchClient.SendMessage(commandData.Channel, $"Error: matched ({matches})");
                return;
            }

            Materia materia = candidates.Single();
            _materiaAccessor.AddMateria(materia.Value);
            string message = $"Materia {materia.Name} Added";
            _twitchClient.SendMessage(commandData.Channel, message);
            _statusHubEmitter.ShowEvent(message);
        }
        private bool IsAllowedToUseCommand(in ChatUser user)
            => (Settings.MateriaSettings.AllowMod && user.IsMod)
               || user.IsMe || user.IsBroadcaster;
    }
}