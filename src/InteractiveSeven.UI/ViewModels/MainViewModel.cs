namespace InteractiveSeven.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _processName = "ff7_en";
        public string ProcessName
        {
            get => _processName;
            set
            {
                _processName = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isConnected = false;
        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                _isConnected = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ConnectionStatus));
            }
        }

        public string ConnectionStatus
            => IsConnected ? "Connected" : "Disconnected";
    }
}