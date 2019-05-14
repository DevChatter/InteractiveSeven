using InteractiveSeven.Core.Settings;
using ReactiveUI;

namespace InteractiveSeven.UI.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        public string ProcessName
        {
            get => ApplicationSettings.Instance.ProcessName;
            set => this.RaiseAndSetPropertyIfChanged(value,
                s => s.ProcessName, (s,v) => s.ProcessName = v);
        }


        private bool _isConnected = false;
        public bool IsConnected
        {
            set => this.RaiseAndSetIfChanged(ref _isConnected, value);
        }

        private string _connectionStatus = "Disconnected";
        public string ConnectionStatus
        {
            get => _connectionStatus;
            set => this.RaiseAndSetIfChanged(ref _connectionStatus, value);
        }
    }
}