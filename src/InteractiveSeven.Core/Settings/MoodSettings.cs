namespace InteractiveSeven.Core.Settings
{
    public class MoodSettings : ObservableSettingsBase
    {
        private bool _enabled = true;
        public bool Enabled // TODO: Add to UI
        {
            get => _enabled;
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }

        private int _defaultNormalBid = 500;
        public int DefaultNormalBid // TODO: Add to UI
        {
            get => _defaultNormalBid;
            set
            {
                _defaultNormalBid = value;
                OnPropertyChanged();
            }
        }
    }
}
