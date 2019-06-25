namespace InteractiveSeven.Core.Settings
{
    public class ItemSettings : ObservableSettingsBase
    {
        private bool _enabled = true;
        private bool _allowMod = true;

        public bool Enabled // TODO: Add to Settings Screen
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
    }
}