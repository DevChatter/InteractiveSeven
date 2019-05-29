using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InteractiveSeven.Core.Settings
{
    public class ApplicationSettings : INotifyPropertyChanged
    {
        private string _processName = "ff7_en";

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
        public MenuColorSettings MenuSettings { get; set; } = new MenuColorSettings();
        public NameBiddingSettings NameBiddingSettings { get; set; } = new NameBiddingSettings();

        public static void LoadFromJson(string json)
        {
            try
            {
                JsonConvert.PopulateObject(json, Instance);
                //SetAllInstanceValues(JsonConvert.DeserializeObject<ApplicationSettings>(json));
            }
            catch (Exception)
            {
                Instance = new ApplicationSettings();
                // gulp
            }
        }

        private static void SetAllInstanceValues(ApplicationSettings s)
        {
            Instance.ProcessName = s.ProcessName;

            Instance.MenuSettings.AllowModOverride = s.MenuSettings.AllowModOverride;
            Instance.MenuSettings.BitCost = s.MenuSettings.BitCost;
            Instance.MenuSettings.Enabled = s.MenuSettings.Enabled;

            Instance.NameBiddingSettings.Enabled = s.NameBiddingSettings.Enabled;
            Instance.NameBiddingSettings.NamingAerisEnabled = s.NameBiddingSettings.NamingAerisEnabled;
            Instance.NameBiddingSettings.NamingBarretEnabled = s.NameBiddingSettings.NamingBarretEnabled;
            Instance.NameBiddingSettings.NamingCaitSithEnabled = s.NameBiddingSettings.NamingCaitSithEnabled;
            Instance.NameBiddingSettings.NamingCidEnabled = s.NameBiddingSettings.NamingCidEnabled;
            Instance.NameBiddingSettings.NamingCloudEnabled = s.NameBiddingSettings.NamingCloudEnabled;
            Instance.NameBiddingSettings.NamingRedEnabled = s.NameBiddingSettings.NamingRedEnabled;
            Instance.NameBiddingSettings.NamingTifaEnabled = s.NameBiddingSettings.NamingTifaEnabled;
            Instance.NameBiddingSettings.NamingVincentEnabled = s.NameBiddingSettings.NamingVincentEnabled;
            Instance.NameBiddingSettings.NamingYuffieEnabled = s.NameBiddingSettings.NamingYuffieEnabled;
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