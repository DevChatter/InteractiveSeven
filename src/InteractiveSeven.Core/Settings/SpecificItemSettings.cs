using InteractiveSeven.Core.Data.Items;
using Newtonsoft.Json;

namespace InteractiveSeven.Core.Settings
{
    public class SpecificItemSettings : ObservableSettingsBase, INamedSetting
    {
        private Items _item;
        [JsonIgnore]
        public Items Item
        {
            get { return _item ??= Items.GetByItemId(ItemId); }
        }

        public ushort ItemId { get; set; }
        public string Name { get; set; }

        public SpecificItemSettings() // Required for Json Deserialization
        {
        }

        public SpecificItemSettings(Items item, bool enabled)
        {
            _item = item;
            ItemId = item.ItemId;
            Name = item.Name;
            Enabled = enabled;
            Cost = item.DefaultPrice;
            DropCost = item.DefaultPrice + 50;
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

        private int _dropCost;
        public int DropCost
        {
            get => _dropCost;
            set
            {
                _dropCost = value;
                OnPropertyChanged();
            }
        }

        public bool IsMatchByName(string name) => Name.NoSpaces().StartsWithIns(name);
    }
}