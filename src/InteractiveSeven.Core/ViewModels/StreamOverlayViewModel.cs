using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.ViewModels
{
    public class StreamOverlayViewModel : INotifyPropertyChanged
    {
        public StreamOverlayViewModel()
        {
            int portNumber = ApplicationSettings.Instance.TsengSettings.PortNumber;
            _statusOverlayUrl = new UriBuilder("http", "localhost", portNumber, "Status").Uri.AbsoluteUri;
            _menuOverlayUrl = new UriBuilder("http", "localhost", portNumber, "Menu").Uri.AbsoluteUri;
            _eventOverlayUrl = new UriBuilder("http", "localhost", portNumber, "Events").Uri.AbsoluteUri;
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

        private string _eventOverlayUrl;
        public string EventOverlayUrl
        {
            get => _eventOverlayUrl;
            set
            {
                _eventOverlayUrl = value;
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