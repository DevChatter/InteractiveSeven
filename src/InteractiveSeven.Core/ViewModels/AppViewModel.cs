using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.General;

namespace InteractiveSeven.Core.ViewModels
{
    public class AppViewModel : BaseViewModel, IModded
    {
        private GameSelection _gameSelection = GameSelection.None;
        public GameSelection GameSelection
        {
            get => _gameSelection;
            set
            {
                _gameSelection = value;
                OnPropertyChanged();
                OnPropertyChanged("IsAwaitingGameSelection");
            }
        }

        private IGameRunner _gameRunner = null;
        public IGameRunner GameRunner
        {
            get => _gameRunner;
            set
            {
                _gameRunner = value;
                OnPropertyChanged();
            }
        }

        public bool IsAwaitingGameSelection => GameSelection == GameSelection.None;

        public bool IsLoadedBy7H { get; private set; }

        public void SetLoadedBy7H(bool wasLoaded)
        {
            IsLoadedBy7H = wasLoaded;
        }
    }
}
