using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Battle
{
    public class StatusEffects
    {
        public string[] Words { get; set; }
        public int Value { get; set; }

        private StatusEffects(int value, params string[] words)
        {
            Value = value;
            Words = words;
            All.Add(this);
        }

        public static List<StatusEffects> All = new List<StatusEffects>();

        public static StatusEffects ByWord(string word)
            => All.FirstOrDefault(x => x.Words.Any(w => w.EqualsIns(word)));

        public static StatusEffects Dead = new StatusEffects(1, null);
        public static StatusEffects NearDeath = new StatusEffects(2, null);
        public static StatusEffects Sleep = new StatusEffects(4, null);
        public static StatusEffects Poison = new StatusEffects(8, "psn", "poison");
        public static StatusEffects Sadness = new StatusEffects(16, "sad", "sadness");
        public static StatusEffects Fury = new StatusEffects(32, "fury", "hyper");
        public static StatusEffects Confusion = new StatusEffects(64, "conf", "confusion");
        public static StatusEffects Silence = new StatusEffects(128, "sil", "silence");
        public static StatusEffects Haste = new StatusEffects(256, "haste", "hasted");
        public static StatusEffects Slow = new StatusEffects(512, "slow", "slowed");
        public static StatusEffects Stop = new StatusEffects(1024, "stop", "stopped");
        public static StatusEffects Frog = new StatusEffects(2048, "frog", "toad");
        public static StatusEffects Small = new StatusEffects(4096, "small");
        public static StatusEffects SlowNumb = new StatusEffects(8192, null);
        public static StatusEffects Petrify = new StatusEffects(16384, null);
        public static StatusEffects Regen = new StatusEffects(32768, null);
        public static StatusEffects Barrier = new StatusEffects(65536, null);
        public static StatusEffects MBarrier = new StatusEffects(131072, null);
        public static StatusEffects Reflect = new StatusEffects(262144, null);
        public static StatusEffects Dual = new StatusEffects(524288, null);
        public static StatusEffects Shield = new StatusEffects(1048576, null);
        public static StatusEffects DeathSentence = new StatusEffects(2097152, null);
        //public static StatusEffects Manipulate = new StatusEffects("", 4194304, null);
        public static StatusEffects Berserk = new StatusEffects(8388608, null);
        public static StatusEffects Peerless = new StatusEffects(16777216, null);
        public static StatusEffects Paralyze = new StatusEffects(33554432, null);
        public static StatusEffects Darkness = new StatusEffects(67108864, null);

    }
}