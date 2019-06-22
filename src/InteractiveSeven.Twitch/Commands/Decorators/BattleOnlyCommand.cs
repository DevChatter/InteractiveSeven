using InteractiveSeven.Core.Memory;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Twitch.Model;
using System.Linq;
using TwitchLib.Client.Interfaces;

namespace InteractiveSeven.Twitch.Commands.Decorators
{
    public class BattleOnlyCommand<T> : ITwitchCommand where T : ITwitchCommand
    {
        private readonly T _internalCommand;
        private readonly ITwitchClient _twitchClient;
        private readonly IMemoryAccessor _memoryAccessor;

        private ApplicationSettings Settings => ApplicationSettings.Instance;

        public BattleOnlyCommand(T internalCommand,
            ITwitchClient twitchClient, IMemoryAccessor memoryAccessor)
        {
            _internalCommand = internalCommand;
            _twitchClient = twitchClient;
            _memoryAccessor = memoryAccessor;
        }

        public bool ShouldExecute(string commandWord)
        {
            return _internalCommand.ShouldExecute(commandWord);
        }

        public void Execute(CommandData commandData)
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
            MemLoc battleIndicator = BattleMemoryLocations.BattleStartedIndicator;
            byte[] bytes = new byte[battleIndicator.NumBytes];
            _memoryAccessor.ReadMem(Settings.ProcessName, battleIndicator.Address, bytes);
            return bytes.Any(b => b > 0);
        }
    }
}