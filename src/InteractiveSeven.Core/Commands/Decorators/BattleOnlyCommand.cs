﻿using System.Threading.Tasks;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Diagnostics.Memory;

namespace InteractiveSeven.Core.Commands.Decorators
{
    public class BattleOnlyCommand<T> : IChatCommand where T : IChatCommand
    {
        private readonly T _internalCommand;
        private readonly IBattleInfoAccessor _battleInfoAccessor;

        public BattleOnlyCommand(T internalCommand,
            IBattleInfoAccessor battleInfoAccessor)
        {
            _internalCommand = internalCommand;
            _battleInfoAccessor = battleInfoAccessor;
        }

        public GamePlayEffects GamePlayEffects => _internalCommand.GamePlayEffects;

        public bool ShouldExecute(string commandWord)
        {
            return _internalCommand.ShouldExecute(commandWord);
        }

        public async Task Execute(CommandData commandData, IChatClient chatClient)
        {
            if (IsBattleActive())
            {
                await _internalCommand.Execute(commandData, chatClient);
            }
            else
            {
                await chatClient.SendMessage(commandData.Channel,
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
