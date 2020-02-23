﻿using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Diagnostics;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.FinalFantasy;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;
using Tseng.GameData;
using Tseng.lib;
using Tseng.RunOnce;
using Timer = System.Timers.Timer;

namespace Tseng
{
    public class TsengMonitor
    {
        private readonly PartyStatusViewModel _partyStatusViewModel;
        private readonly ProcessConnector _processConnector;
        private readonly GameDatabase _gameDatabase;
        private readonly IStatusHubEmitter _statusHubEmitter;
        private readonly ILogger<TsengMonitor> _logger;
        private readonly NativeMemoryReader _memoryReader;

        public TsengMonitor(PartyStatusViewModel partyStatusViewModel,
            ProcessConnector processConnector,
            GameDatabase gameDatabase,
            NativeMemoryReader memoryReader,
            IStatusHubEmitter statusHubEmitter,
            ILogger<TsengMonitor> logger)
        {
            _memoryReader = memoryReader;
            _partyStatusViewModel = partyStatusViewModel;
            _processConnector = processConnector;
            _gameDatabase = gameDatabase;
            _statusHubEmitter = statusHubEmitter;
            _logger = logger;
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
            _logger.LogInformation("Checking for FF7 Process...");
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

                _logger.LogInformation($"Located FF7 process {FF7.ProcessName}");
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Updating Tseng Info");
            }
        }
    }
}