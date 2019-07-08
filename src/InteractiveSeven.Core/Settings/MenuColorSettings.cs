namespace InteractiveSeven.Core.Settings
{
    public class MenuColorSettings : ObservableSettingsBase
    {
        private bool _enabled = true;
        private int _bitCost;
        private bool _allowModOverride = true;
        private bool _transitionColors = true;

        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }
        public int BitCost
        {
            get => _bitCost;
            set
            {
                _bitCost = value;
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
        public bool TransitionColors
        {
            get => _transitionColors;
            set
            {
                _transitionColors = value;
                OnPropertyChanged();
            }
        }
    }
}