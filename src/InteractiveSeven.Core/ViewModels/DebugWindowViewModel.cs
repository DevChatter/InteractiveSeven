using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InteractiveSeven.Core.ViewModels
{
    public class DebugWindowViewModel : INotifyPropertyChanged
    {
        public PartyStatusViewModel PartyStatus { get; }

        public DebugWindowViewModel(PartyStatusViewModel partyStatusViewModel)
        {
            PartyStatus = partyStatusViewModel;
        }

        private ushort _gameMoment;
        public ushort GameMoment
        {
            get => _gameMoment;
            set
            {
                _gameMoment = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}