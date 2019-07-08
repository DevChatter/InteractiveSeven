using System.Threading.Tasks;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Model;

namespace InteractiveSeven.Twitch.Commands
{
    public class RainbowCommand : BaseCommand
    {
        private readonly IMenuColorAccessor _menuColorAccessor;

        public RainbowCommand(IMenuColorAccessor menuColorAccessor)
            : base(x => new []{"Rainbow"}, x => true)
        {
            _menuColorAccessor = menuColorAccessor;
        }

        public override void Execute(in CommandData commandData)
        {
            if (!commandData.User.IsBroadcaster)
            {
                return;
            }

            Task.Run(() =>
            {
                // TODO: Mark as "ON" and disable menu command and further instances of this command.
                for (int i = 0; i < 100; i++) // TODO: Configurable Time
                {
                    _menuColorAccessor.SetMenuColors(ApplicationSettings.Instance.ProcessName,
                        MenuColors.RandomPalette());
                }
            });
        }
    }
}