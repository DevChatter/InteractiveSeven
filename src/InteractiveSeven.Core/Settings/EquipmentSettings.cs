using System.Collections.Generic;
using System.Linq;
using InteractiveSeven.Core.Data.Items;
using Newtonsoft.Json;

namespace InteractiveSeven.Core.Settings
{
    public class EquipmentSettings : ObservableSettingsBase
    {
        private bool _enabled = true;
        private bool _allowModOverride = true;
        private bool _keepPreviousEquipment = true;
        private bool _enablePauperCommand = true;

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

        public List<EquippableSettings> AllWeapons { get; set; }
        public List<EquippableSettings> AllArmlets { get; set; }
        public List<EquippableSettings> AllAccessories { get; set; }
    }
}