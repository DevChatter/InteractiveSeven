using System.Reactive;
using ReactiveUI;

namespace InteractiveSeven.UI.ViewModels
{
    public class MainViewModel : ReactiveObject
    {

        public MainViewModel()
        {
            BrowseExeCmd = ReactiveCommand.Create(() => { });
        }

        private string _processName = "ff7_en";
        public string ProcessName
        {
            get => _processName;
            set => this.RaiseAndSetIfChanged(ref _processName, value);
        }

        private bool _isConnected = false;
        public bool IsConnected
        {
            set => this.RaiseAndSetIfChanged(ref _isConnected, value, "ConnectionStatus");
        }

        public string ConnectionStatus
            => _isConnected ? "Connected" : "Disconnected";

        public ReactiveCommand<Unit, Unit> BrowseExeCmd { get; }

    }
}