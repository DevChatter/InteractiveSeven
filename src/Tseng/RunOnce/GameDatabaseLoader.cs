using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Diagnostics;
using InteractiveSeven.Core.FinalFantasy.Constants;
using InteractiveSeven.Core.FinalFantasy.Models;
using Shojy.FF7.Elena;
using Shojy.FF7.Elena.Equipment;
using Accessory = InteractiveSeven.Core.FinalFantasy.Models.Accessory;
using Weapon = InteractiveSeven.Core.FinalFantasy.Models.Weapon;

namespace Tseng.RunOnce
{
    public class GameDatabaseLoader : IGameDatabaseLoader
    {
        private readonly ProcessConnector _processConnector;
        private readonly IModded _modded;

        public GameDatabaseLoader(ProcessConnector processConnector, IModded modded)
        {
            _processConnector = processConnector;
            this._modded = modded;
        }

        private Process FF7 => _processConnector.FF7Process;

        public (bool, List<Accessory>, List<Armlet>, List<Materia>, List<Weapon>)
            LoadDataFromKernel(GameDatabase gameDatabase)
        {
            var accessories = new List<Accessory>();
            var armlets = new List<Armlet>();
            var materias = new List<Materia>();
            var weapons = new List<Weapon>();
            if (FF7?.MainModule is null)
            {
                return (false, accessories, armlets, materias, weapons);
            }
            var ff7Exe = FF7.MainModule?.FileName;
            var ff7Folder = Path.GetDirectoryName(ff7Exe);

            string kernelLocation;

            if (this._modded.IsLoadedBy7H)
            {
                kernelLocation = "kernels";
            }
            else
            {
                // Steam Location
                kernelLocation = Path.Combine(ff7Folder, "data", "lang-en", "kernel");

                if (!File.Exists(Path.Combine(kernelLocation, "KERNEL.BIN")))
                {
                    // Original / GameConverter location
                    kernelLocation = Path.Combine(ff7Folder, "data", "kernel");
                }
            }

            var elena = new KernelReader(Path.Combine(kernelLocation, "KERNEL.BIN"));
            elena.MergeKernel2Data(Path.Combine(kernelLocation, "kernel2.bin"));

            // Map Elena's data into local data dbs.
            foreach (var materia in elena.MateriaData.Materias)
            {
                var m = new Materia { Id = materia.Index, Name = materia.Name };
                switch (materia.MateriaType)
                {
                    case Shojy.FF7.Elena.Materias.MateriaType.Command:
                        m.Type = MateriaType.Command;
                        break;

                    case Shojy.FF7.Elena.Materias.MateriaType.Magic:
                        m.Type = MateriaType.Magic;
                        break;

                    case Shojy.FF7.Elena.Materias.MateriaType.Summon:
                        m.Type = MateriaType.Summon;
                        break;

                    case Shojy.FF7.Elena.Materias.MateriaType.Support:
                        m.Type = MateriaType.Support;
                        break;

                    case Shojy.FF7.Elena.Materias.MateriaType.Independent:
                        m.Type = MateriaType.Independent;
                        break;

                    default:
                        m.Type = MateriaType.None;
                        break;
                }
                materias.Add(m);
            }

            materias.Add(new Materia { Id = FF7Const.Empty, Name = "Empty Slot", Type = MateriaType.None });

            foreach (var wpn in elena.WeaponData.Weapons)
            {
                var w = new Weapon
                {
                    Name = wpn.Name,
                    Id = (ushort)wpn.Index,
                    Growth = (int)wpn.GrowthRate,
                    LinkedSlots = wpn.MateriaSlots.Count(slot =>
                        slot == MateriaSlot.EmptyLeftLinkedSlot
                        || slot == MateriaSlot.EmptyRightLinkedSlot
                        || slot == MateriaSlot.NormalLeftLinkedSlot
                        || slot == MateriaSlot.NormalRightLinkedSlot),
                    SingleSlots = wpn.MateriaSlots.Count(slot =>
                        slot == MateriaSlot.EmptyUnlinkedSlot
                        || slot == MateriaSlot.NormalUnlinkedSlot)
                };
                // Work out what weapon icon to use
                if ((wpn.EquipableBy & (EquipableBy.Cloud | EquipableBy.YoungCloud)) == wpn.EquipableBy)
                {
                    w.Type = WeaponType.Sword;
                }
                else if (wpn.EquipableBy == (wpn.EquipableBy & EquipableBy.Barret))
                {
                    w.Type = WeaponType.Arm;
                }
                else if (wpn.EquipableBy == (wpn.EquipableBy & EquipableBy.Tifa))
                {
                    w.Type = WeaponType.Glove;
                }
                else if (wpn.EquipableBy == (wpn.EquipableBy & EquipableBy.Aeris))
                {
                    w.Type = WeaponType.Staff;
                }
                else if (wpn.EquipableBy == (wpn.EquipableBy & EquipableBy.RedXIII))
                {
                    w.Type = WeaponType.Hairpin;
                }
                else if (wpn.EquipableBy == (wpn.EquipableBy & EquipableBy.Yuffie))
                {
                    w.Type = WeaponType.Shuriken;
                }
                else if (wpn.EquipableBy == (wpn.EquipableBy & EquipableBy.CaitSith))
                {
                    w.Type = WeaponType.Megaphone;
                }
                else if (wpn.EquipableBy == (wpn.EquipableBy & EquipableBy.Vincent))
                {
                    w.Type = WeaponType.Gun;
                }
                else if (wpn.EquipableBy == (wpn.EquipableBy & EquipableBy.Cid))
                {
                    w.Type = WeaponType.Pole;
                }
                else
                {
                    w.Type = WeaponType.Other;
                }
                weapons.Add(w);
            }

            foreach (var arm in elena.ArmorData.Armors)
            {
                armlets.Add(new Armlet
                {
                    Id = (ushort)arm.Index,
                    Name = arm.Name,
                    Growth = (int)arm.GrowthRate,
                    LinkedSlots = arm.MateriaSlots.Count(slot =>
                        slot == MateriaSlot.EmptyLeftLinkedSlot
                        || slot == MateriaSlot.EmptyRightLinkedSlot
                        || slot == MateriaSlot.NormalLeftLinkedSlot
                        || slot == MateriaSlot.NormalRightLinkedSlot),
                    SingleSlots = arm.MateriaSlots.Count(slot =>
                        slot == MateriaSlot.EmptyUnlinkedSlot
                        || slot == MateriaSlot.NormalUnlinkedSlot)
                });
            }

            foreach (var acc in elena.AccessoryData.Accessories)
            {
                accessories.Add(new Accessory
                {
                    Id = (ushort)acc.Index,
                    Name = acc.Name,
                    StatusDefense = (StatusEffects)acc.StatusDefense,
                });
            }

            return (true, accessories, armlets, materias, weapons);
        }
    }
}