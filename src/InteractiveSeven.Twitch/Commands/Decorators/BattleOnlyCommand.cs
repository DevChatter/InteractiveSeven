using InteractiveSeven.Core;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Twitch.Model;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands.Decorators
{
    public class BattleOnlyCommand<T> : ITwitchCommand where T : ITwitchCommand
    {
        private readonly T _internalCommand;
        private readonly ITwitchClient _twitchClient;
        private readonly IBattleInfoAccessor _battleInfoAccessor;

        public BattleOnlyCommand(T internalCommand,
            ITwitchClient twitchClient,
            IBattleInfoAccessor battleInfoAccessor)
        {
            _internalCommand = internalCommand;
            _twitchClient = twitchClient;
            _battleInfoAccessor = battleInfoAccessor;
        }

        public GamePlayEffects GamePlayEffects => _internalCommand.GamePlayEffects;

        public bool ShouldExecute(string commandWord)
        {
            return _internalCommand.ShouldExecute(commandWord);
        }

        public void Execute(in CommandData commandData)
        {
            if (IsBattleActive())
            {
                _internalCommand.Execute(commandData);
            }
            else
            {
                _twitchClient.SendMessage(commandData.Channel,
                    $"Can only use !{commandData.CommandText} during battle.");
            }
        }

        private bool IsBattleActive()
        {
            var ff7BattleMap = _battleInfoAccessor.GetBattleMap();
            return ff7BattleMap.IsActiveBattle && !ff7BattleMap.IsBattleEnding;
        }
    }
}