using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InteractiveSeven.Core.Settings
{
    public class ApplicationSettings : INotifyPropertyChanged
    {
        private string _processName = "ff7_en";
        private bool _giveSubscriberBonusBits = true;
        private int _subscriberBonusBits = 150;

        public static ApplicationSettings Instance { get; private set; }
        static ApplicationSettings()
        {
            Instance = new ApplicationSettings();
        }

        public string ProcessName
        {
            get => _processName;
            set
            {
                _processName = value;
                OnPropertyChanged();
            }
        }

        public bool GiveSubscriberBonusBits
        {
            get => _giveSubscriberBonusBits;
            set
            {
                _giveSubscriberBonusBits = value;
                OnPropertyChanged();
            }
        }

        public int SubscriberBonusBits
        {
            get => _subscriberBonusBits;
            set
            {
                _subscriberBonusBits = value;
                OnPropertyChanged();
            }
        }

        public TwitchSettings TwitchSettings { get; set; } = new TwitchSettings();
        public MenuColorSettings MenuSettings { get; set; } = new MenuColorSettings();
        public NameBiddingSettings NameBiddingSettings { get; set; } = new NameBiddingSettings();

        public static void LoadFromJson(string json)
        {
            try
            {
                JsonConvert.PopulateObject(json, Instance);
            }
            catch (Exception)
            {
                Instance = new ApplicationSettings();
                // gulp
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class MenuColorSettings : INotifyPropertyChanged
    {
        private bool _enabled = true;
        private int _bitCost;
        private bool _allowModOverride = true;

        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }
        public int BitCost
        {
            get => _bitCost;
            set
            {
                _bitCost = value;
                OnPropertyChanged();
            }
        }
        public bool AllowModOverride
        {
            get => _allowModOverride;
            set
            {
                _allowModOverride = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class NameBiddingSettings : INotifyPropertyChanged
    {
        private bool _enabled = true;
        private int _defaultStartBits = 100;
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}