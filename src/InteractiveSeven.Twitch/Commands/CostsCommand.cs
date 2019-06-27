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

        public override void Execute(CommandData commandData)
        {
            _twitchClient.SendMessage(commandData.Channel,
                $"[MenuColors costs {Settings.MenuSettings.BitCost}] [Default Names Started At {Settings.NameBiddingSettings.DefaultStartBits}]");
        }
    }
}