namespace InteractiveSeven.Core.Settings
{
    public class PlayerGpSettings : ObservableSettingsBase
    {
        private int _giveMultiplier = 100;
        private int _removeMultiplier = 100;
        private bool _allowModOverride = true;
        private bool _giveGpEnabled = false;
        private bool _removeGpEnabled = false;

        public bool GiveGpEnabled // TODO: Add to UI
        {
            get => _giveGpEnabled;
            set
            {
                _giveGpEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool RemoveGpEnabled // TODO: Add to UI
        {
            get => _removeGpEnabled;
            set
            {
                _removeGpEnabled = value;
                OnPropertyChanged();
            }
        }
        public int GiveMultiplier // TODO: Add to UI
        {
            get => _giveMultiplier;
            set
            {
                _giveMultiplier = value;
                OnPropertyChanged();
            }
        }
        public int RemoveMultiplier // TODO: Add to UI
        {
            get => _removeMultiplier;
            set
            {
                _removeMultiplier = value;
                OnPropertyChanged();
            }
        }
        public bool AllowModOverride // TODO: Add to UI
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