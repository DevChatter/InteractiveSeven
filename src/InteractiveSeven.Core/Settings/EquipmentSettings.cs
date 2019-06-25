using Newtonsoft.Json;

namespace InteractiveSeven.Core.Settings
{
    public class EquipmentSettings : ObservableSettingsBase
    {
        private bool _enabled = true;
        private bool _allowModOverride = true;
        private bool _keepPreviousEquipment = true;

        public bool Enabled // TODO: Add to Settings Screen
        {
            get => _enabled;
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }

        public bool AllowModOverride // TODO: Add to Settings Screen
        {
            get => _allowModOverride;
            set
            {
                _allowModOverride = value;
                OnPropertyChanged();
            }
        }

        public bool KeepPreviousEquipment // TODO: Add to Settings Screen
        {
            get => _keepPreviousEquipment;
            set
            {
                _keepPreviousEquipment = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public EquippableSettings[] AllEquippableSettings { get; set; }
    }
}