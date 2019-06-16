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
            Words = words ?? new string[0];
            All.Add(this);
        }

        public static List<StatusEffects> All = new List<StatusEffects>();

        public static StatusEffects ByWord(string word)
            => All.FirstOrDefault(x => x.Words.Any(w => w.EqualsIns(word)));

        public static StatusEffects Dead = new StatusEffects(1);
        public static StatusEffects NearDeath = new StatusEffects(2);
        public static StatusEffects Sleep = new StatusEffects(4, "sleep", "slp", "sleeping");
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
        public static StatusEffects SlowNumb = new StatusEffects(8192);
        public static StatusEffects Petrify = new StatusEffects(16384, "petrify");
        public static StatusEffects Regen = new StatusEffects(32768, "regen");
        public static StatusEffects Barrier = new StatusEffects(65536, "barrier");
        public static StatusEffects MBarrier = new StatusEffects(131072, "mbarrier");
        public static StatusEffects Reflect = new StatusEffects(262144, "reflect");
        public static StatusEffects Dual = new StatusEffects(524288, "dual");
        public static StatusEffects Shield = new StatusEffects(1048576, "shield");
        public static StatusEffects DeathSentence = new StatusEffects(2097152, "deathsentence");
        public static StatusEffects Manipulate = new StatusEffects(4194304);
        public static StatusEffects Berserk = new StatusEffects(8388608, "berserk");
        public static StatusEffects Peerless = new StatusEffects(16777216, "peerless");
        public static StatusEffects Paralyze = new StatusEffects(33554432, "paralyze");
        public static StatusEffects Darkness = new StatusEffects(67108864, "darkness");

    }
}