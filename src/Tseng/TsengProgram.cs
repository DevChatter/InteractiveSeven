using InteractiveSeven.Core.Battle;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Diagnostics;
using InteractiveSeven.Core.FinalFantasy;
using InteractiveSeven.Core.FinalFantasy.Models;
using InteractiveSeven.Core.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;
using InteractiveSeven.Core.Settings;
using Tseng.GameData;
using Tseng.lib;
using Tseng.RunOnce;
using Character = Tseng.GameData.Character;
using Timer = System.Timers.Timer;

namespace Tseng
{
    public class TsengProgram
    {
        private readonly PartyStatusViewModel _partyStatusViewModel;
        private readonly ProcessConnector _processConnector;
        private readonly GameDatabase _gameDatabase;
        private readonly ILogger<TsengProgram> _logger;

        public TsengProgram(PartyStatusViewModel partyStatusViewModel,
            ProcessConnector processConnector,
            GameDatabase gameDatabase,
            ILogger<TsengProgram> logger)
        {
            _partyStatusViewModel = partyStatusViewModel;
            _processConnector = processConnector;
            _gameDatabase = gameDatabase;
            _logger = logger;
        }

        #region Private Properties

        private FF7BattleMap BattleMap { get; set; }
        private Process FF7 => _processConnector.FF7Process;
        private NativeMemoryReader MemoryReader { get; set; }
        private FF7SaveMap SaveMap { get; set; }
        private Timer Timer { get; set; }
        private ApplicationSettings Settings => ApplicationSettings.Instance;

        #endregion Private Properties

        #region Public Methods

        public void UpdateStatusFromMap(FF7SaveMap map, FF7BattleMap battleMap)
        {
            var t = TimeSpan.FromSeconds(map.LiveTotalSeconds).ToString("hh\\:mm\\:ss");

            _partyStatusViewModel.Gil = map.LiveGil;
            _partyStatusViewModel.Location = map.LiveMapName;
            _partyStatusViewModel.Party = new InteractiveSeven.Core.FinalFantasy.Models.Character[3];
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

                var chr = new InteractiveSeven.Core.FinalFantasy.Models.Character()
                {
                    MaxHp = chars[index].MaxHp,
                    MaxMp = chars[index].MaxMp,
                    CurrentHp = chars[index].CurrentHp,
                    CurrentMp = chars[index].CurrentMp,
                    Name = chars[index].Name,
                    Level = chars[index].Level,
                    Weapon = _gameDatabase.WeaponDatabase.FirstOrDefault(w => w.Id == chars[index].Weapon),
                    Armlet = _gameDatabase.ArmletDatabase.FirstOrDefault(a => a.Id == chars[index].Armor),
                    Accessory = _gameDatabase.AccessoryDatabase.FirstOrDefault(a => a.Id == chars[index].Accessory),
                    WeaponMateria = new Materia[8],
                    ArmletMateria = new Materia[8],
                    Face = GetFaceForCharacter(chars[index]),
                    BackRow = !chars[index].AtFront,
                };

                for (var m = 0; m < chars[index].WeaponMateria.Length; ++m)
                {
                    chr.WeaponMateria[m] = _gameDatabase.MateriaDatabase.FirstOrDefault(x => x.Id == chars[index].WeaponMateria[m]);
                }
                for (var m = 0; m < chars[index].ArmorMateria.Length; ++m)
                {
                    chr.ArmletMateria[m] = _gameDatabase.MateriaDatabase.FirstOrDefault(x => x.Id == chars[index].ArmorMateria[m]);
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

            _gameDatabase.LoadData();

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

        private void SearchForProcess()
        {
            _logger.LogInformation("Searching for FF7 Process...");
            if (Timer is null)
            {
                Timer = new Timer(300);
                Timer.Elapsed += ReadAllGameData;
                Timer.AutoReset = true;

                ReadAllGameData(null, null);
                Timer.Start();
            }
            lock (Timer) // TODO: Find out why we are locking here?
            {
                if (null != Timer)
                {
                    Timer.Enabled = false;
                }

                while (FF7 is null)
                {
                    // Attempt to connect is automatic when checked.
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

        private void StartMonitoringGame()
        {
            if (Timer is null)
            {
                Timer = new Timer(Settings.TsengSettings.MemoryReadIntervalInMs);
                Timer.Elapsed += ReadAllGameData;
                Timer.AutoReset = true;

                // TODO: Confirm if this needs to run or if start starts it.
                ReadAllGameData(null, null);
                Timer.Start();
            }
        }

        private void ReadAllGameData(object sender, ElapsedEventArgs e)
        {
            try
            {
                var saveMapByteData = MemoryReader.ReadMemory(Addresses.SaveMapStart);
                var isBattle = MemoryReader.ReadMemory(Addresses.ActiveBattleState).First();
                var battleMapByteData = MemoryReader.ReadMemory(Addresses.BattleMapStart);
                var colors = MemoryReader.ReadMemory(Addresses.MenuColorAll);

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