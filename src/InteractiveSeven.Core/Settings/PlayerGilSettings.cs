namespace InteractiveSeven.Core.Settings
{
    public class PlayerGilSettings : ObservableSettingsBase
    {
        private int _giveMultiplier = 100;
        private int _removeMultiplier = 100;
        private bool _allowModOverride = true;
        private bool _giveGilEnabled = false;
        private bool _removeGilEnabled = false;

        public bool GiveGilEnabled // TODO: Add to Settings Screen
        {
            get => _giveGilEnabled;
            set
            {
                _giveGilEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool RemoveGilEnabled // TODO: Add to Settings Screen
        {
            get => _removeGilEnabled;
            set
            {
                _removeGilEnabled = value;
                OnPropertyChanged();
            }
        }
        public int GiveMultiplier // TODO: Add to Settings Screen
        {
            get => _giveMultiplier;
            set
            {
                _giveMultiplier = value;
                OnPropertyChanged();
            }
        }
        public int RemoveMultiplier // TODO: Add to Settings Screen
        {
            get => _removeMultiplier;
            set
            {
                _removeMultiplier = value;
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
    }
}