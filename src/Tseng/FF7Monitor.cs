using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Diagnostics;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.FinalFantasy;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.ViewModels;
using Serilog;
using Tseng.GameData;
using Tseng.lib;
using Tseng.RunOnce;
using Timer = System.Timers.Timer;

namespace Tseng
{
    public class FF7Monitor
    {
        private readonly PartyStatusViewModel _partyStatusViewModel;
        private readonly ProcessConnector _processConnector;
        private readonly GameDatabase _gameDatabase;
        private readonly IStatusHubEmitter _statusHubEmitter;
        private readonly NativeMemoryReader _memoryReader;
        private readonly MonitorViewModel _monitorViewModel;

        public FF7Monitor(PartyStatusViewModel partyStatusViewModel,
            ProcessConnector processConnector,
            GameDatabase gameDatabase,
            NativeMemoryReader memoryReader,
            MonitorViewModel monitorViewModel,
            IStatusHubEmitter statusHubEmitter)
        {
            _memoryReader = memoryReader;
            _monitorViewModel = monitorViewModel;
            _partyStatusViewModel = partyStatusViewModel;
            _processConnector = processConnector;
            _gameDatabase = gameDatabase;
            _statusHubEmitter = statusHubEmitter;
        }

        private FF7BattleMap BattleMap { get; set; }
        private Process FF7 => _processConnector.FF7Process;
        private FF7SaveMap SaveMap { get; set; }
        private Timer Timer { get; set; }
        private ApplicationSettings Settings => ApplicationSettings.Instance;

        public void UpdateStatusFromMap(FF7SaveMap map, FF7BattleMap battleMap)
        {
            _partyStatusViewModel.UpdateStatusFromMap(map, battleMap, _gameDatabase);
            _statusHubEmitter.ShowNewPartyStatus(_partyStatusViewModel);
        }

        public void Start(bool support7H = false)
        {
            Log.Logger.Information("Starting FF7 Monitor");
            SearchForProcess();

            _gameDatabase.LoadData();

            var missingAssets = AssetExtractor.IsAssetExtractionRequired();

            if (missingAssets.Any())
            {
                var ff7Exe = FF7.MainModule?.FileName;
                var ff7Folder = Path.GetDirectoryName(ff7Exe);
                AssetExtractor.ExtractAssets(ff7Folder, missingAssets);
            }
        }

        private void SearchForProcess()
        {
            Log.Logger.Information("Checking for FF7 Process...");
            if (Timer is null || FF7 is null || FF7.HasExited)
            {
                if (Timer != null)
                {
                    Timer.Enabled = false;
                }

                while (FF7 is null)
                {
                    // Attempt to connect is automatic when checked.
                    Thread.Sleep(2000);
                }

                Log.Logger.Information($"Located FF7 process {FF7.ProcessName}");
                if (Timer != null)
                {
                    Timer.Enabled = true;
                }
            }
            StartMonitoringGame();
        }

        private void StartMonitoringGame()
        {
            if (Timer is null)
            {
                Timer = new Timer(Settings.TsengSettings.MemoryReadIntervalInMs);
                Timer.Elapsed += Timer_Elapsed;
                Timer.AutoReset = true;

                Timer.Start();
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ReadAllGameData();
        }

        private void ReadAllGameData()
        {
            try
            {
                if (FF7?.HasExited ?? true)
                {
                    _monitorViewModel.IsConnected = false;
                    _monitorViewModel.ProcessName = FF7?.ProcessName;
                    return;
                }

                var saveMapByteData = _memoryReader.ReadMemory(Addresses.SaveMapStart);
                byte isBattle = _memoryReader.ReadMemory(Addresses.ActiveBattleState)?.First() ?? 0;
                var battleMapByteData = _memoryReader.ReadMemory(Addresses.BattleMapStart);
                var colors = _memoryReader.ReadMemory(Addresses.MenuColorAll);

                if (saveMapByteData is null)
                {
                    return;
                }

                SaveMap = new FF7SaveMap(saveMapByteData, colors);
                BattleMap = new FF7BattleMap(battleMapByteData, isBattle);

                UpdateStatusFromMap(SaveMap, BattleMap);
                _monitorViewModel.IsConnected = true;
                _monitorViewModel.ProcessName = FF7?.ProcessName;
            }
            catch (Exception ex)
            {
                _monitorViewModel.IsConnected = false;
                _monitorViewModel.ProcessName = FF7?.ProcessName;
                Log.Logger.Error(ex, "Error Updating Tseng Info");
            }
        }
    }
}
