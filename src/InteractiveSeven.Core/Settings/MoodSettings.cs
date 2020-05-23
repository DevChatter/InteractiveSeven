namespace InteractiveSeven.Core.Settings
{
    public class MoodSettings : ObservableSettingsBase
    {
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
    }
}