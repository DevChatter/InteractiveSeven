namespace InteractiveSeven.Core.Settings
{
    public class TsengSettings : ObservableSettingsBase
    {
        private int _memoryReadIntervalInMs = 500;
        public int MemoryReadIntervalInMs
        {
            get => _memoryReadIntervalInMs;
            set
            {
                _memoryReadIntervalInMs = value;
                OnPropertyChanged();
            }
        }

    }
}