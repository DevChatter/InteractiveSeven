using InteractiveSeven.Core.Battle;
using Newtonsoft.Json;

namespace InteractiveSeven.Core.Settings
{
    public class StatusEffectSettings : ObservableSettingsBase
    {
        public StatusEffects Effect { get; }

        public string Name { get; }

        public StatusEffectSettings()
        {
        }

        public StatusEffectSettings(string name, StatusEffects effect, bool enabled, int cost, params string[] words)
        {
            Name = name;
            Effect = effect;
            _words = words ?? new string[0];
            _enabled = enabled;
            _cost = cost;
        }

        private string[] _words;
        public string[] Words
        {
            get => _words;
            set
            {
                _words = value;
                OnPropertyChanged();
            }
        }

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