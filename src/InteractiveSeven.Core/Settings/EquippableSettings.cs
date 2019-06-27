using InteractiveSeven.Core.Data.Items;
using Newtonsoft.Json;

namespace InteractiveSeven.Core.Settings
{
    public class EquippableSettings : ObservableSettingsBase
    {
        [JsonIgnore]
        public Items Item { get; }
        public string Name { get; set; }

        public EquippableSettings(Items item, bool enabled, int cost, params string[] words)
        {
            Item = item;
            Name = item.Name;
            Enabled = enabled;
            Cost = cost;
            Words = words;
        }

        private bool _enabled = true;
        public bool Enabled // TODO: Add to Settings Screen
        {
            get => _enabled;
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }

        private int _cost;
        public int Cost // TODO: Add to Settings Screen
        {
            get => _cost;
            set
            {
                _cost = value;
                OnPropertyChanged();
            }
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

    }
}