﻿using System.Collections.Generic;
using InteractiveSeven.Core.FinalFantasy.Models;
using Serilog;

namespace InteractiveSeven.Core.Data
{
    public class GameDatabase
    {
        private readonly IGameDatabaseLoader _loader;
        private bool _isLoaded = false;
        private readonly List<Accessory> _accessoryDatabase = new();
        private readonly List<Armlet> _armletDatabase = new();
        private readonly List<Materia> _materiaDatabase = new();
        private readonly List<Weapon> _weaponDatabase = new();

        private readonly object _padlock = new object();

        public GameDatabase(IGameDatabaseLoader loader)
        {
            _loader = loader;
        }

        public void LoadData()
        {
            Log.Logger.Information("Loading game metadata from Kernel");
            if (!_isLoaded)
            {
                lock (_padlock)
                {
                    if (!_isLoaded)
                    {
                        var result = _loader.LoadDataFromKernel(this);
                        _isLoaded = result.loaded;
                        if (_isLoaded)
                        {
                            _accessoryDatabase.AddRange(result.accessories);
                            _armletDatabase.AddRange(result.armlets);
                            _materiaDatabase.AddRange(result.materias);
                            _weaponDatabase.AddRange(result.weapons);
                        }
                    }
                }
            }
        }

        public IReadOnlyList<Accessory> AccessoryDatabase
        {
            get
            {
                if (!_isLoaded) LoadData();
                return _accessoryDatabase;
            }
        }

        public IReadOnlyList<Armlet> ArmletDatabase
        {
            get
            {
                if (!_isLoaded) LoadData();
                return _armletDatabase;
            }
        }

        public IReadOnlyList<Materia> MateriaDatabase
        {
            get
            {
                if (!_isLoaded) LoadData();
                return _materiaDatabase;
            }
        }

        public IReadOnlyList<Weapon> WeaponDatabase
        {
            get
            {
                if (!_isLoaded) LoadData();
                return _weaponDatabase;
            }
        }
    }
}
