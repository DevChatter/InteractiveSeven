using Newtonsoft.Json;
using System;

namespace InteractiveSeven.Core.Settings
{
    public class ApplicationSettings : ObservableSettingsBase
    {
        private string _processName = "ff7_en";
        private bool _giveSubscriberBonusBits = true;
        private int _subscriberBonusBits = 150;
        private bool _modsGiveBonusBits = true;

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
        public bool ModsGiveBonusBits
        {
            get => _modsGiveBonusBits;
            set
            {
                _modsGiveBonusBits = value;
                OnPropertyChanged();
            }
        }

        public CommandSettings CommandSettings { get; set; } = new CommandSettings();
        public TwitchSettings TwitchSettings { get; set; } = new TwitchSettings();
        public MenuColorSettings MenuSettings { get; set; } = new MenuColorSettings();
        public NameBiddingSettings NameBiddingSettings { get; set; } = new NameBiddingSettings();
        public BattleSettings BattleSettings { get; set; } = new BattleSettings();
        public EquipmentSettings EquipmentSettings { get; set; } = new EquipmentSettings();
        public ItemSettings ItemSettings { get; set; } = new ItemSettings();
        public MateriaSettings MateriaSettings { get; set; } = new MateriaSettings();

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
    }
}