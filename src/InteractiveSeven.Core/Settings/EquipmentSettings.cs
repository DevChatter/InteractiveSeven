﻿using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Settings
{
    public class EquipmentSettings : ObservableSettingsBase
    {
        private bool _enabled = true;
        private bool _allowModOverride = true;
        private bool _keepPreviousEquipment = true;
        private bool _enablePauperCommand = true;
        private int _pauperCommandCost = 20000;

        public EquipmentSettings()
        {
            AllWeapons = Items.All.OfType<Weapon>().Select(x => new EquippableSettings(x, true, x.Words)).ToList();
            AllArmlets = Items.All.OfType<Armlet>().Select(x => new EquippableSettings(x, true, x.Words)).ToList();
            AllAccessories = Items.All.OfType<Accessory>().Select(x => new EquippableSettings(x, true, x.Words)).ToList();
        }

        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }

        public bool AllowModOverride
        {
            get => _allowModOverride;
            set
            {
                _allowModOverride = value;
                OnPropertyChanged();
            }
        }

        public bool KeepPreviousEquipment
        {
            get => _keepPreviousEquipment;
            set
            {
                _keepPreviousEquipment = value;
                OnPropertyChanged();
            }
        }

        public bool EnablePauperCommand
        {
            get => _enablePauperCommand;
            set
            {
                _enablePauperCommand = value;
                OnPropertyChanged();
            }
        }

        public int PauperCommandCost
        {
            get => _pauperCommandCost;
            set
            {
                _pauperCommandCost = value;
                OnPropertyChanged();
            }
        }

        public List<EquippableSettings> AllByName(string name, CharNames charName, Type type)
        {
            if (type == typeof(Weapon))
            {
                return AllWeapons.AllByName(name, charName);
            }
            if (type == typeof(Accessory))
            {
                return AllAccessories.AllByName(name, charName);
            }
            if (type == typeof(Armlet))
            {
                return AllArmlets.AllByName(name, charName);
            }
            return new List<EquippableSettings>();
        }

        public List<EquippableSettings> AllWeapons { get; set; }
        public List<EquippableSettings> AllArmlets { get; set; }
        public List<EquippableSettings> AllAccessories { get; set; }
        public PlayerGilSettings PlayerGilSettings { get; set; } = new PlayerGilSettings();
        public PlayerGpSettings PlayerGpSettings { get; set; } = new PlayerGpSettings();
    }
}