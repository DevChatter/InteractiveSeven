using CommunityToolkit.Mvvm.ComponentModel;

namespace InteractiveSeven.Core.ViewModels
{
    public partial class MonitorViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _processName;

        [ObservableProperty]
        private bool _isConnected;
    }
}
