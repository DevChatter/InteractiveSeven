﻿using System.Linq;
using InteractiveSeven.Core.Chat;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Commands.Decorators
{
    public class NonBattleCommand<T> : IChatCommand where T : IChatCommand
    {
        private readonly T _internalCommand;
        private readonly IChatClient _chatClient;
        private readonly IMemoryAccessor _memoryAccessor;

        private ApplicationSettings Settings => ApplicationSettings.Instance;

        public NonBattleCommand(T internalCommand,
            IChatClient chatClient, IMemoryAccessor memoryAccessor)
        {
            _internalCommand = internalCommand;
            _chatClient = chatClient;
            _memoryAccessor = memoryAccessor;
        }

        public GamePlayEffects GamePlayEffects => _internalCommand.GamePlayEffects;

        public bool ShouldExecute(string commandWord)
        {
            return _internalCommand.ShouldExecute(commandWord);
        }

        public void Execute(in CommandData commandData)
        {
            if (!IsBattleActive())
            {
                _internalCommand.Execute(commandData);
            }
            else
            {
                _chatClient.SendMessage(commandData.Channel,
                    $"Can only use !{commandData.CommandText} outside of battle.");
            }
        }

        private bool IsBattleActive()
        {
            MemLoc battleIndicator = BattleMemoryLocations.BattleStartedIndicator;
            byte[] bytes = new byte[battleIndicator.NumBytes];
            _memoryAccessor.ReadMem(Settings.ProcessName, battleIndicator.Address, bytes);
            return bytes.Any(b => b > 0);
        }
    }
}
