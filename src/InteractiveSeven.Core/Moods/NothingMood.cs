using System.Threading;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.FinalFantasy;

namespace InteractiveSeven.Core.Moods
{
    public class NothingMood : Mood
    {
        private bool _isStarted = false;
        private static readonly object Padlock = new();
        private Thread _thread;
        private readonly MemoryFreezer _memoryFreezer;

        public NothingMood(MemoryFreezer memoryFreezer)
            : base("Nothing Mood")
        {
            _memoryFreezer = memoryFreezer;
            _memoryFreezer.AddValue(Addresses.BattleApGain, new byte[] { 0, 0 });
            _memoryFreezer.AddValue(Addresses.BattleXpGain, new byte[] { 0, 0, 0, 0 });
            _memoryFreezer.AddValue(Addresses.BattleGilGain, new byte[] { 0, 0, 0, 0 });
            _memoryFreezer.AddValue(Addresses.RewardItem1Amount, new byte[] { 0, 0 });
            _memoryFreezer.AddValue(Addresses.RewardItem2Amount, new byte[] { 0, 0 });
            _memoryFreezer.AddValue(Addresses.RewardItem3Amount, new byte[] { 0, 0 });
        }

        public const int DefaultId = 5;
        public override int Id => DefaultId;
        public override void ApplyEffect()
        {
            // ReSharper disable once InconsistentlySynchronizedField
            if (!_isStarted)
            {
                lock (Padlock)
                {
                    if (!_isStarted)
                    {
                        _isStarted = true;
                        _thread = new(_memoryFreezer.ThreadStart);
                        _thread.Start();
                    }
                }
            }
        }

        public override void RemoveEffect()
        {
            _memoryFreezer.Thaw();
            _isStarted = false;
            _thread = null;
            base.RemoveEffect();
        }
    }
}
