namespace InteractiveSeven.Core.Settings
{
    public class StatusEffectSettings : ObservableSettingsBase
    {
        public StatusEffectSettings()
        {
        }

        public StatusEffectSettings(string name, bool enabled, int cost)
        {
            Name = name;
            _enabled = enabled;
            _cost = cost;
        }

        public string Name { get; set; }

        private bool _enabled = true;
        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }

        private int _cost = 100;
        public int Cost
        {
            get => _cost;
            set
            {
                _cost = value;
                OnPropertyChanged();
            }
        }
    }
}