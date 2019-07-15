namespace InteractiveSeven.Core.Settings
{
    public class NameBiddingSettings : ObservableSettingsBase
    {
        private bool _enabled = true;
        private int _defaultStartBits = 99;
        private bool _allowModeration = true;
        private bool _allowModBits = true;
        private bool _namingCloudEnabled = true;
        private bool _namingBarretEnabled = true;
        private bool _namingTifaEnabled = true;
        private bool _namingAerisEnabled = true;
        private bool _namingCaitSithEnabled = true;
        private bool _namingCidEnabled = true;
        private bool _namingRedEnabled = true;
        private bool _namingVincentEnabled = true;
        private bool _namingYuffieEnabled = true;

        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }

        public int DefaultStartBits
        {
            get => _defaultStartBits;
            set
            {
                _defaultStartBits = value;
                OnPropertyChanged();
            }
        }

        public bool AllowModeration
        {
            get => _allowModeration;
            set
            {
                _allowModeration = value;
                OnPropertyChanged();
            }
        }

        public bool AllowModBits
        {
            get => _allowModBits;
            set
            {
                _allowModBits = value;
                OnPropertyChanged();
            }
        }

        public bool NamingCloudEnabled
        {
            get => _namingCloudEnabled;
            set
            {
                _namingCloudEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool NamingBarretEnabled
        {
            get => _namingBarretEnabled;
            set
            {
                _namingBarretEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool NamingTifaEnabled
        {
            get => _namingTifaEnabled;
            set
            {
                _namingTifaEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool NamingAerisEnabled
        {
            get => _namingAerisEnabled;
            set
            {
                _namingAerisEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool NamingCaitSithEnabled
        {
            get => _namingCaitSithEnabled;
            set
            {
                _namingCaitSithEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool NamingCidEnabled
        {
            get => _namingCidEnabled;
            set
            {
                _namingCidEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool NamingRedEnabled
        {
            get => _namingRedEnabled;
            set
            {
                _namingRedEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool NamingVincentEnabled
        {
            get => _namingVincentEnabled;
            set
            {
                _namingVincentEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool NamingYuffieEnabled
        {
            get => _namingYuffieEnabled;
            set
            {
                _namingYuffieEnabled = value;
                OnPropertyChanged();
            }
        }
    }
}