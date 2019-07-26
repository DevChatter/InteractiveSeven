using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Tseng.Models;
using InteractiveSeven.Core.ViewModels;
using Shojy.FF7.Elena;
using Shojy.FF7.Elena.Equipment;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;
using InteractiveSeven.Core.Settings;
using Microsoft.Extensions.Logging;
using Tseng.Constants;
using Tseng.GameData;
using Tseng.lib;
using Tseng.RunOnce;
using Accessory = InteractiveSeven.Core.Tseng.Models.Accessory;
using Character = Tseng.GameData.Character;
using Timer = System.Timers.Timer;
using Weapon = InteractiveSeven.Core.Tseng.Models.Weapon;

namespace Tseng
{
    public class TsengProgram
    {
        private readonly PartyStatusViewModel _partyStatusViewModel;
        private readonly ILogger<TsengProgram> _logger;

        public TsengProgram(PartyStatusViewModel partyStatusViewModel, ILogger<TsengProgram> logger)
        {
            _partyStatusViewModel = partyStatusViewModel;
            _logger = logger;
        }

        #region Public Properties

        public List<Accessory> AccessoryDatabase { get; set; } = new List<Accessory>();
        public List<Armlet> ArmletDatabase { get; set; } = new List<Armlet>();
        public FF7BattleMap BattleMap { get; set; }
        public List<Materia> MateriaDatabase { get; set; } = new List<Materia>();
        public List<Weapon> WeaponDatabase { get; set; } = new List<Weapon>();

        #endregion Public Properties

        #region Private Properties

        private static Process FF7 { get; set; }
        private static NativeMemoryReader MemoryReader { get; set; }
        private static string ProcessName => ApplicationSettings.Instance.ProcessName;
        private static FF7SaveMap SaveMap { get; set; }
        private static Timer Timer { get; set; }

        #endregion Private Properties

        #region Public Methods

        public void UpdateStatusFromMap(FF7SaveMap map, FF7BattleMap battleMap)
        {
            var time = map.LiveTotalSeconds; // TODO: Use a TimeSpan here?

            var t = $"{(time / 3600):00}:{((time % 3600) / 60):00}:{(time % 60):00}";

            _partyStatusViewModel.Gil = map.LiveGil;
            _partyStatusViewModel.Location = map.LiveMapName;
            _partyStatusViewModel.Party = new InteractiveSeven.Core.Tseng.Models.Character[3];
            _partyStatusViewModel.ActiveBattle = battleMap.IsActiveBattle;
            _partyStatusViewModel.ColorTopLeft = map.WindowColorTopLeft;
            _partyStatusViewModel.ColorBottomLeft = map.WindowColorBottomLeft;
            _partyStatusViewModel.ColorBottomRight = map.WindowColorBottomRight;
            _partyStatusViewModel.ColorTopRight = map.WindowColorTopRight;
            _partyStatusViewModel.TimeActive = t;
            var party = battleMap.Party;

            var chars = map.LiveParty;

            for (var index = 0; index < chars.Length; ++index)
            {
                // Skip empty party
                if (chars[index].Id == 0xFF) continue;

                var chr = new InteractiveSeven.Core.Tseng.Models.Character()
                {
                    MaxHp = chars[index].MaxHp,
                    MaxMp = chars[index].MaxMp,
                    CurrentHp = chars[index].CurrentHp,
                    CurrentMp = chars[index].CurrentMp,
                    Name = chars[index].Name,
                    Level = chars[index].Level,
                    Weapon = WeaponDatabase.FirstOrDefault(w => w.Id == chars[index].Weapon),
                    Armlet = ArmletDatabase.FirstOrDefault(a => a.Id == chars[index].Armor),
                    Accessory = AccessoryDatabase.FirstOrDefault(a => a.Id == chars[index].Accessory),
                    WeaponMateria = new Materia[8],
                    ArmletMateria = new Materia[8],
                    Face = GetFaceForCharacter(chars[index]),
                    BackRow = !chars[index].AtFront,
                };

                for (var m = 0; m < chars[index].WeaponMateria.Length; ++m)
                {
                    chr.WeaponMateria[m] = MateriaDatabase.FirstOrDefault(x => x.Id == chars[index].WeaponMateria[m]);
                }
                for (var m = 0; m < chars[index].ArmorMateria.Length; ++m)
                {
                    chr.ArmletMateria[m] = MateriaDatabase.FirstOrDefault(x => x.Id == chars[index].ArmorMateria[m]);
                }

                var effect = (StatusEffects)chars[index].Flags;

                if (battleMap.IsActiveBattle)
                {
                    chr.CurrentHp = party[index].CurrentHp;
                    chr.MaxHp = party[index].MaxHp;
                    chr.CurrentMp = party[index].CurrentMp;
                    chr.MaxMp = party[index].MaxMp;
                    chr.Level = party[index].Level;
                    effect = party[index].Status;
                    chr.BackRow = party[index].IsBackRow;
                }

                var effs = effect.ToString()
                    .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
                effs.RemoveAll(x => new[] { "None", "Death" }.Contains(x));
                chr.StatusEffects = effs.ToArray();
                _partyStatusViewModel.Party[index] = chr;
            }
        }

        public static string GetFaceForCharacter(CharacterRecord chr)
        {
            // TODO: Abstract magic string names behind variable set that's also used for image extraction
            switch (chr.Character)
            {
                case Character.Cloud:
                    return "cloud";

                case Character.Barret:
                    return "barret";

                case Character.Tifa:
                    return "tifa";

                case Character.Aeris:
                    return "aeris";

                case Character.RedXIII:
                    return "red-xiii";

                case Character.Yuffie:
                    return "yuffie";

                case Character.CaitSith:
                    return "cait-sith";

                case Character.Vincent:
                    return "vincent";

                case Character.Cid:
                    return "cid";

                case Character.YoungCloud:
                    return "young-cloud";

                case Character.Sephiroth:
                    return "sephiroth";

                default:
                    return "";
            }
        }

        public void Start()
        {
            SearchForProcess();

            LoadDataFromKernel();

            var missingAssets = AssetExtractor.IsAssetExtractionRequired();

            if (missingAssets.Any())
            {
                var ff7Exe = FF7.MainModule?.FileName;
                var ff7Folder = Path.GetDirectoryName(ff7Exe);
                AssetExtractor.ExtractAssets(ff7Folder, missingAssets);
            }

            StartMonitoringGame();
        }

        #endregion Public Methods

        #region Private Methods

        private void LoadDataFromKernel()
        {
            if (FF7?.MainModule == null)
            {
                throw new Exception("FF7 Process MUST be discovered before data can be loaded");
            }
            var ff7Exe = FF7.MainModule?.FileName;
            var ff7Folder = Path.GetDirectoryName(ff7Exe);
            var kernelLocation = Path.Combine(ff7Folder, "data", "lang-en", "kernel");

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
                MateriaDatabase.Add(m);
            }

            MateriaDatabase.Add(new Materia { Id = 255, Name = "Empty Slot", Type = MateriaType.None });

            foreach (var wpn in elena.WeaponData.Weapons)
            {
                var w = new Weapon
                {
                    Name = wpn.Name,
                    Id = wpn.Index,
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
                WeaponDatabase.Add(w);
            }

            foreach (var arm in elena.ArmorData.Armors)
            {
                ArmletDatabase.Add(new Armlet
                {
                    Id = arm.Index,
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
                AccessoryDatabase.Add(new Accessory
                {
                    Id = acc.Index,
                    Name = acc.Name
                });
            }
        }

        private void SearchForProcess()
        {
            _logger.LogInformation("Searching for FF7 Process...");
            if (Timer is null)
            {
                Timer = new Timer(300);
                Timer.Elapsed += Timer_Elapsed;
                Timer.AutoReset = true;

                Timer_Elapsed(null, null);
                Timer.Start();
            }
            lock (Timer) // TODO: Find out why we are locking here?
            {
                if (null != Timer)
                {
                    Timer.Enabled = false;
                }

                FF7 = null;
                while (FF7 is null)
                {
                    GetRunningFF7Process();

                    Thread.Sleep(2000);
                }

                MemoryReader = new NativeMemoryReader(FF7);
                _logger.LogInformation($"Located FF7 process {FF7.ProcessName}");
                if (null != Timer)
                {
                    Timer.Enabled = true;
                }
            }
        }

        private void GetRunningFF7Process()
        {
            try
            {
                FF7 ??= (!string.IsNullOrWhiteSpace(ProcessName)
                            ? Process.GetProcessesByName(ProcessName).FirstOrDefault()
                            : FF7)
                        ?? Process.GetProcessesByName("ff7_en").FirstOrDefault()
                        ?? Process.GetProcessesByName("ff7").FirstOrDefault();
            }
            catch
            {
                // TODO: Log here
            }
        }

        private void StartMonitoringGame()
        {
            if (Timer is null)
            {
                Timer = new Timer(500);
                Timer.Elapsed += Timer_Elapsed;
                Timer.AutoReset = true;

                Timer_Elapsed(null, null);
                Timer.Start();
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                var saveMapByteData = MemoryReader.ReadMemory(new IntPtr(Addresses.SaveMapStart), 4342);
                var isBattle = MemoryReader.ReadMemory(new IntPtr(Addresses.ActiveBattleState), 1).First();
                var battleMapByteData = MemoryReader.ReadMemory(new IntPtr(Addresses.BattleMapStart), 0x750);
                var colors = MemoryReader.ReadMemory(new IntPtr(Addresses.WindowColorBlockStart), 16);

                SaveMap = new FF7SaveMap(saveMapByteData);
                BattleMap = new FF7BattleMap(battleMapByteData, isBattle);

                SaveMap.WindowColorTopLeft = $"{colors[0x2]:X2}{colors[0x1]:X2}{colors[0x0]:X2}";
                SaveMap.WindowColorBottomLeft = $"{colors[0x6]:X2}{colors[0x5]:X2}{colors[0x4]:X2}";
                SaveMap.WindowColorTopRight = $"{colors[0xA]:X2}{colors[0x9]:X2}{colors[0x8]:X2}";
                SaveMap.WindowColorBottomRight = $"{colors[0xE]:X2}{colors[0xD]:X2}{colors[0xC]:X2}";

                UpdateStatusFromMap(SaveMap, BattleMap);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Updating Tseng Info");
                SearchForProcess();
            }
        }

        #endregion Private Methods
    }
}