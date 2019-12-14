using InteractiveSeven.Core.Data.Items;
using Newtonsoft.Json;

namespace InteractiveSeven.Core.Settings
{
    public class EquippableSettings : ObservableSettingsBase, INamedSetting
    {
        private Equipment _item;
        [JsonIgnore]
        public Equipment Item
        {
            get { return _item ??= Items.GetByItemId(ItemId) as Equipment; }
        }

        public ushort ItemId { get; set; }

        public string Name { get; set; }

        public EquippableSettings() // Required for Json Deserialization
        {
        }

        public EquippableSettings(Equipment item, bool enabled, params string[] words)
        {
            _item = item;
            ItemId = item.ItemId;
            Name = item.Name;
            Enabled = enabled;
            Cost = item.DefaultPrice;
            Words = words;
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

        private int _cost;
        public int Cost
        {
            get => _cost;
            set
            {
                _cost = value;
                OnPropertyChanged();
            }
        }

        private string[] _words;

        public string[] Words // TODO: Add to Settings Screen
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