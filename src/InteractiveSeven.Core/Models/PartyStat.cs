namespace InteractiveSeven.Core.Models
{
    public class PartyStat
    {
        public string DefaultName { get; set; }
        public string Name { get; set; }
        public int CurrentHp { get; set; }
        public int MaxHp { get; set; }
        public int CurrentMp { get; set; }
        public int MaxMp { get; set; }
        public int Strength { get; set; }
        public int Vitality { get; set; }
        public int Magic { get; set; }
        public int Spirit { get; set; }
        public int Dexterity { get; set; }
        public int Luck { get; set; }
    }
}
