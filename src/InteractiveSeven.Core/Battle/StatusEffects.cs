namespace InteractiveSeven.Core.Battle
{
    public class StatusEffects
    {
        public int Value { get; set; }

        private StatusEffects(int value)
        {
            Value = value;
        }

        public static StatusEffects Dead = new StatusEffects(1);
        public static StatusEffects NearDeath = new StatusEffects(2);
        public static StatusEffects Sleep = new StatusEffects(4);
        public static StatusEffects Poison = new StatusEffects(8);
        public static StatusEffects Sadness = new StatusEffects(16);
        public static StatusEffects Fury = new StatusEffects(32);
        public static StatusEffects Confusion = new StatusEffects(64);
        public static StatusEffects Silence = new StatusEffects(128);
        public static StatusEffects Haste = new StatusEffects(256);
        public static StatusEffects Slow = new StatusEffects(512);
        public static StatusEffects Stop = new StatusEffects(1024);
        public static StatusEffects Frog = new StatusEffects(2048);
        public static StatusEffects Small = new StatusEffects(4096);
        public static StatusEffects SlowNumb = new StatusEffects(8192);
        public static StatusEffects Petrify = new StatusEffects(16384);
        public static StatusEffects Regen = new StatusEffects(32768);
        public static StatusEffects Barrier = new StatusEffects(65536);
        public static StatusEffects MBarrier = new StatusEffects(131072);
        public static StatusEffects Reflect = new StatusEffects(262144);
        public static StatusEffects Dual = new StatusEffects(524288);
        public static StatusEffects Shield = new StatusEffects(1048576);
        public static StatusEffects DeathSentence = new StatusEffects(2097152);
        public static StatusEffects Manipulate = new StatusEffects(4194304);
        public static StatusEffects Berserk = new StatusEffects(8388608);
        public static StatusEffects Peerless = new StatusEffects(16777216);
        public static StatusEffects Paralyze = new StatusEffects(33554432);
        public static StatusEffects Darkness = new StatusEffects(67108864);
    }
}