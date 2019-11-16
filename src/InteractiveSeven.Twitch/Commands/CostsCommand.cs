using InteractiveSeven.Twitch.Model;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class CostsCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;

        public CostsCommand(ITwitchClient twitchClient)
            : base(x => x.CostsCommandWords, x => true)
        {
            _twitchClient = twitchClient;
        }

        public override void Execute(in CommandData commandData)
        {
            string message = "";

            if (Settings.MenuSettings.Enabled)
            {
                message += $"[Change Menu: {Settings.MenuSettings.BitCost}] ";
            }

            if (Settings.MenuSettings.EnableMakoCommand)
            {
                message += $"[Mako Mode: {Settings.MenuSettings.MakoModeCost}] ";
            }

            if (Settings.MenuSettings.EnableRainbowCommand)
            {
                message += $"[Rainbow Mode: {Settings.MenuSettings.RainbowModeCost}] ";
            }

            _twitchClient.SendMessage(commandData.Channel, message);
        }
    }
}