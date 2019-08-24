using InteractiveSeven.Core.Settings;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InteractiveSeven.Core.ViewModels
{
    public class StreamOverlayViewModel : INotifyPropertyChanged
    {
        public StreamOverlayViewModel()
        {
            int portNumber = ApplicationSettings.Instance.TsengSettings.PortNumber;
            _statusOverlayUrl = new UriBuilder("http", "localhost", portNumber, "Status").Uri.AbsoluteUri;
            _menuOverlayUrl = new UriBuilder("http", "localhost", portNumber, "Menu").Uri.AbsoluteUri;
        }

        private string _statusOverlayUrl;
        public string StatusOverlayUrl
        {
            get => _statusOverlayUrl;
            set
            {
                _statusOverlayUrl = value;
                OnPropertyChanged();
            }
        }

        private string _menuOverlayUrl;
        public string MenuOverlayUrl
        {
            get => _menuOverlayUrl;
            set
            {
                _menuOverlayUrl = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}