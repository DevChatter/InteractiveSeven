using System.Linq;
using InteractiveSeven.Core.Commands.Battle;
using InteractiveSeven.Core.Data;
using Tseng.GameData;

namespace InteractiveSeven.Core.FinalFantasy.Models
{
    public class Character
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public string Face { get; set; }
        public uint MaxHp { get; set; }
        public uint CurrentHp { get; set; }
        public ushort MaxMp { get; set; }
        public ushort CurrentMp { get; set; }
        public int Level { get; set; }
        public Weapon Weapon { get; set; }
        public Armlet Armlet { get; set; }
        public Accessory Accessory { get; set; }
        public Materia[] WeaponMateria { get; set; }
        public Materia[] ArmletMateria { get; set; }
        public bool BackRow { get; set; }
        public string Status { get; set; } = "";
        public string[] StatusEffects { get; set; } = new string[0];
        public StatusEffects StatusEffectsValue { get; set; }

        public static Character FromCharacterRecord(CharacterRecord record, GameDatabase gameDatabase)
        {
            var character = new Character
            {
                Id = record.Id,
                MaxHp = record.MaxHp,
                MaxMp = record.MaxMp,
                CurrentHp = record.CurrentHp,
                CurrentMp = record.CurrentMp,
                Name = record.Name,
                Level = record.Level,
                Weapon = gameDatabase.WeaponDatabase.FirstOrDefault(w => w.Id == record.Weapon),
                Armlet = gameDatabase.ArmletDatabase.FirstOrDefault(a => a.Id == record.Armor),
                Accessory = gameDatabase.AccessoryDatabase.FirstOrDefault(a => a.Id == record.Accessory),
                WeaponMateria = new Materia[8],
                ArmletMateria = new Materia[8],
                Face = record.DefaultName.SanitizedDefaultName,
                BackRow = !record.AtFront,
            };
            for (var m = 0; m < record.WeaponMateria.Length; ++m)
            {
                character.WeaponMateria[m] = gameDatabase.MateriaDatabase.FirstOrDefault(x => x.Id == record.WeaponMateria[m].Id);
            }
            for (var m = 0; m < record.ArmorMateria.Length; ++m)
            {
                character.ArmletMateria[m] = gameDatabase.MateriaDatabase.FirstOrDefault(x => x.Id == record.ArmorMateria[m].Id);
            }

            return character;
        }

        public bool HasStatus(StatusEffects status) => (StatusEffectsValue & status) > 0;
    }
}
