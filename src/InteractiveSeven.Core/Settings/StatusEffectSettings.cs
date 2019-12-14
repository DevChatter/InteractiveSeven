using InteractiveSeven.Core.Battle;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Linq;

namespace InteractiveSeven.Core.Settings
{
    public class StatusEffectSettings : ObservableSettingsBase, INamedSetting
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusEffects Effect { get; set; }

        public string Name { get; set; }

        public StatusEffectSettings()
        {
        }

        public StatusEffectSettings(string name, StatusEffects effect, bool enabled, int cost, int cureCost, params string[] words)
        {
            Name = name;
            Effect = effect;
            _words = words ?? new string[0];
            _enabled = enabled;
            _cost = cost;
            _cureCost = cureCost;
        }

        private string[] _words;
        public string[] Words
        {
            get => _words;
            set
            {
                _words = RemoveDuplicates(value);
                OnPropertyChanged();
            }
        }

        private string[] RemoveDuplicates(string[] value)
        {
            var allStatusEffects = ApplicationSettings.Instance.BattleSettings.AllStatusEffects;

            var otherEffectWords = allStatusEffects
                .Where(x => x.Name != Name)
                .SelectMany(x => x.Words);

            return value.Except(otherEffectWords, StringComparer.OrdinalIgnoreCase).ToArray();
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

        private int _cureCost = 100;
        public int CureCost
        {
            get => _cureCost;
            set
            {
                _cureCost = value;
                OnPropertyChanged();
            }
        }
    }
}