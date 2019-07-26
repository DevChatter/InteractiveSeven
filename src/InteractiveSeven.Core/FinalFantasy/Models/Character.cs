namespace InteractiveSeven.Core.FinalFantasy.Models
{
    public class Character
    {
        public string Name { get; set; }
        public string Face { get; set; }
        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }
        public int MaxMp { get; set; }
        public int CurrentMp { get; set; }
        public int Level { get; set; }
        public Weapon Weapon { get; set; }
        public Armlet Armlet { get; set; }
        public Accessory Accessory { get; set; }
        public Materia[] WeaponMateria { get; set; }
        public Materia[] ArmletMateria { get; set; }
        public bool BackRow { get; set; }
        public string Status { get; set; } = "";
        public string[] StatusEffects { get; set; } = new string[0];
    }
}
