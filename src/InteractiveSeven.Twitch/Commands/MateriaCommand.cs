using InteractiveSeven.Core.Data.Items;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Twitch.Model;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class MateriaCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;
        private readonly IMateriaAccessor _materiaAccessor;

        public MateriaCommand(ITwitchClient twitchClient, IMateriaAccessor materiaAccessor)
            : base(x => new[] {"materia"}, x => true)
        {
            _twitchClient = twitchClient;
            _materiaAccessor = materiaAccessor;
        }

        public override void Execute(CommandData commandData)
        {
            // TODO: Allow Moderators to Use this Command
            if (!commandData.User.IsMe && !commandData.User.IsBroadcaster) return;

            string materiaIdText = commandData.Arguments.FirstOrDefault();
            if (materiaIdText != null && byte.TryParse(materiaIdText, out byte materiaId) && materiaId < 91)
            {
                _materiaAccessor.AddMateria(materiaId);
                Materia materia = Materia.All.SingleOrDefault(x => x.Value == materiaId);
                string materiaName = materia == null ? "Unknown Materia" : materia.Name;
                _twitchClient.SendMessage(commandData.Channel,
                    $"Materia {materiaName} Added");
            }
        }
    }
}