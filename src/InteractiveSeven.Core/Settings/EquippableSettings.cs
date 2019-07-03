using InteractiveSeven.Core.Data.Items;
using Newtonsoft.Json;

namespace InteractiveSeven.Core.Settings
{
    public class EquippableSettings : ObservableSettingsBase
    {
        private Items _item;
        [JsonIgnore]
        public Items Item
        {
            get { return _item ??= Items.GetByItemId(ItemId); }
        }

        public ushort ItemId { get; set; }

        public string Name { get; set; }

        public EquippableSettings() // Required for Json Deserialization
        {
        }

        public EquippableSettings(Items item, bool enabled, params string[] words)
        {
            _item = item;
            ItemId = item.ItemId;
            Name = item.Name;
            Enabled = enabled;
            Cost = item.DefaultPrice;
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