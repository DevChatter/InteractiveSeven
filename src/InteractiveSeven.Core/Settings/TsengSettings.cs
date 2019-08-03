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

        private int _portNumber = 7777;
        public int PortNumber
        {
            get => _portNumber;
            set
            {
                _portNumber = value;
                OnPropertyChanged();
            }
        }

    }
}