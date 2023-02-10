using System.Collections.Generic;
using System.Linq;
using InteractiveSeven.Core.Data.Items;

namespace InteractiveSeven.Core.Settings
{
    public class ItemSettings : ObservableSettingsBase
    {
        private bool _enabled = true;
        private bool _allowMod = true;

        public ItemSettings()
        {
            AllItems = Items.All.Select(x => new SpecificItemSettings(x, true)).ToList();
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

        public bool AllowMod // TODO: Add to Settings Screen
        {
            get => _allowMod;
            set
            {
                _allowMod = value;
                OnPropertyChanged();
            }
        }

        public List<SpecificItemSettings> AllItems { get; set; }

        public List<SpecificItemSettings> AllByName(string name)
            => AllItems.Where(x => x.IsMatchByName(name)).ToList();
    }
}