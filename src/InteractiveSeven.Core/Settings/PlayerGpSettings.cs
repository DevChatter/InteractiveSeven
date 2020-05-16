namespace InteractiveSeven.Core.Settings
{
    public class PlayerGpSettings : ObservableSettingsBase
    {
        private ushort _giveMultiplier = 100;
        private ushort _removeMultiplier = 100;
        private bool _allowModOverride = true;
        private bool _giveGpEnabled = false;
        private bool _removeGpEnabled = false;

        public bool GiveGpEnabled
        {
            get => _giveGpEnabled;
            set
            {
                _giveGpEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool RemoveGpEnabled
        {
            get => _removeGpEnabled;
            set
            {
                _removeGpEnabled = value;
                OnPropertyChanged();
            }
        }
        public ushort GiveMultiplier
        {
            get => _giveMultiplier;
            set
            {
                _giveMultiplier = value;
                OnPropertyChanged();
            }
        }
        public ushort RemoveMultiplier
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