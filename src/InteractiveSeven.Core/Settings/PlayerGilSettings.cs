namespace InteractiveSeven.Core.Settings
{
    public class PlayerGilSettings : ObservableSettingsBase
    {
        private int _giveMultiplier = 100;
        private int _removeMultiplier = 100;
        private bool _allowModOverride = true;
        private bool _giveGilEnabled = false;
        private bool _removeGilEnabled = false;

        public bool GiveGilEnabled
        {
            get => _giveGilEnabled;
            set
            {
                _giveGilEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool RemoveGilEnabled
        {
            get => _removeGilEnabled;
            set
            {
                _removeGilEnabled = value;
                OnPropertyChanged();
            }
        }
        public int GiveMultiplier
        {
            get => _giveMultiplier;
            set
            {
                _giveMultiplier = value;
                OnPropertyChanged();
            }
        }
        public int RemoveMultiplier
        {
            get => _removeMultiplier;
            set
            {
                _removeMultiplier = value;
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
    }
}