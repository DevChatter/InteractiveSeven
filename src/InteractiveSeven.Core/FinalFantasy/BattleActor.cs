using InteractiveSeven.Core.Battle;

namespace Tseng.GameData
{
    public struct BattleActor
    {
        public int CurrentHp { get; set; }
        public int MaxHp { get; set; }
        public short CurrentMp { get; set; }
        public short MaxMp { get; set; }
        public byte Level { get; set; }
        public StatusEffects Status { get; set; }
        public bool IsBackRow { get; set; }
    }
}