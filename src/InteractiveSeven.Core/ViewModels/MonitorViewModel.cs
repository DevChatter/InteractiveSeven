using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InteractiveSeven.Core.ViewModels
{
    public class MonitorViewModel : INotifyPropertyChanged
    {
        private string _processName;
        public string ProcessName
        {
            get => string.IsNullOrEmpty(_processName) ? "None" : _processName;
            set
            {
                if (_processName != value)
                {
                    _processName = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                if (_isConnected != value)
                {
                    _isConnected = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
