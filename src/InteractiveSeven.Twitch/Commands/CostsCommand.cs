using System;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Model;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class CostsCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;
        private ApplicationSettings Settings => ApplicationSettings.Instance;

        public CostsCommand(ITwitchClient twitchClient)
            : base(new[] { "costs", "cost", "price", "prices" }, x => true)
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