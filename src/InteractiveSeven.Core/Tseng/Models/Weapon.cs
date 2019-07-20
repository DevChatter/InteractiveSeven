namespace InteractiveSeven.Core.Tseng.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LinkedSlots { get; set; }
        public int SingleSlots { get; set; }
        public int Growth { get; set; }
        public WeaponType Type { get; set; }
        public string WeaponType => Type.ToString();
    }
}
