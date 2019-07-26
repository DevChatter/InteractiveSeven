using System.Collections.Generic;
using InteractiveSeven.Core.Tseng.Models;

namespace InteractiveSeven.Core.Data
{
    public class GameDatabase
    {
        // TODO: Move the GameDatabase loading call in here.
        public List<Accessory> AccessoryDatabase { get; set; } = new List<Accessory>();
        public List<Armlet> ArmletDatabase { get; set; } = new List<Armlet>();
        public List<Materia> MateriaDatabase { get; set; } = new List<Materia>();
        public List<Weapon> WeaponDatabase { get; set; } = new List<Weapon>();
    }
}