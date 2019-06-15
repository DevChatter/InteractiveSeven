using System;
using System.Linq;
using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Model;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands
{
    public class StatusEffectCommand : BaseCommand
    {
        private readonly ITwitchClient _twitchClient;
        private readonly IStatusAccessor _statusAccessor;

        private static string[] AllWords(CommandSettings settings)
            => StatusEffects.All
                .SelectMany(effect => effect.Words)
                .ToArray();


        public StatusEffectCommand(ITwitchClient twitchClient, IStatusAccessor statusAccessor)
            : base(AllWords, x => true) // TODO: Add Settings
        {
            _twitchClient = twitchClient;
            _statusAccessor = statusAccessor;
        }

        public override void Execute(CommandData commandData)
        {
            var statusEffect = StatusEffects.ByWord(commandData.CommandText);
            var actor = Allies.ByWord(commandData.Arguments.FirstOrDefault());
            if (statusEffect == null || actor == null)
            {
                _twitchClient.SendMessage(commandData.Channel, "Be sure to name a valid status and actor. Example: !psn top");
                return;
            }

            _statusAccessor.SetActorStatus(actor, statusEffect);

            _twitchClient.SendMessage(commandData.Channel,
                $"Applied {statusEffect.Words.First()} to {actor.Words.First()}.");
        }
    }
}