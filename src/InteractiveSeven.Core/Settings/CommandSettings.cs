namespace InteractiveSeven.Core.Settings
{
    public class CommandSettings : ObservableSettingsBase
    {
        private string[] _costsCommandWords = { "Costs", "Cost", "Price", "Prices"};
        private string[] _balanceCommandWords = { "Balance", "Gil" };
        private string[] _giveGilCommandWords = { "GiveGil", "Give" };
        private string[] _i7CommandWords = { "i7", "Interactive7", "Interactive" };
        private string[] _menuCommandWords = { "Menu", "MenuColor", "Window", "Windows" };
        private string[] _nameBidsCommandWords = { "NameBids" };
        private string[] _refreshCommandWords = { "Refresh" };

        public string[] CostsCommandWords
        {
            get => _costsCommandWords;
            set
            {
                _costsCommandWords = value;
                OnPropertyChanged();
            }
        }

        public string[] BalanceCommandWords
        {
            get => _balanceCommandWords;
            set
            {
                _balanceCommandWords = value;
                OnPropertyChanged();
            }
        }

        public string[] GiveGilCommandWords
        {
            get => _giveGilCommandWords;
            set
            {
                _giveGilCommandWords = value;
                OnPropertyChanged();
            }
        }

        public string[] I7CommandWords
        {
            get => _i7CommandWords;
            set
            {
                _i7CommandWords = value;
                OnPropertyChanged();
            }
        }

        public string[] MenuCommandWords
        {
            get => _menuCommandWords;
            set
            {
                _menuCommandWords = value;
                OnPropertyChanged();
            }
        }

        public string[] NameBidsCommandWords
        {
            get => _nameBidsCommandWords;
            set
            {
                _nameBidsCommandWords = value;
                OnPropertyChanged();
            }
        }

        public string[] RefreshCommandWords
        {
            get => _refreshCommandWords;
            set
            {
                _refreshCommandWords = value;
                OnPropertyChanged();
            }
        }
    }
}