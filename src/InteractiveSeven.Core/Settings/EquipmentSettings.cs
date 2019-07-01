using Newtonsoft.Json;

namespace InteractiveSeven.Core.Settings
{
    public class EquipmentSettings : ObservableSettingsBase
    {
        private bool _enabled = true;
        private bool _allowModOverride = true;
        private bool _keepPreviousEquipment = true;
        private bool _enablePauperCommand = true;

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

        [JsonIgnore]
        public EquippableSettings[] AllEquippableSettings { get; set; }
    }
}