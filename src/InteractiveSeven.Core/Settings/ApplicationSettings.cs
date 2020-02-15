using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace InteractiveSeven.Core.Settings
{
    public class ApplicationSettings : ObservableSettingsBase
    {
        private string _processName = "ff7_en";
        private bool _giveSubscriberBonusBits = true;
        private int _subscriberBonusBits = 150;
        private bool _modsGiveBonusBits = true;
        private bool _enableModCommand = true;
        private bool _allowGameAffectingCommands = true;

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

        public bool EnableModCommand
        {
            get => _enableModCommand;
            set
            {
                _enableModCommand = value;
                OnPropertyChanged();
            }
        }

        public bool AllowGameAffectingCommands
        {
            get => _allowGameAffectingCommands;
            set
            {
                _allowGameAffectingCommands = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public GamePlayEffects GamePlayMode => AllowGameAffectingCommands ?
            GamePlayEffects.DisplayOnly | GamePlayEffects.MildEffect | GamePlayEffects.MajorEffect
            : GamePlayEffects.DisplayOnly;

        public CommandSettings CommandSettings { get; set; } = new CommandSettings();
        public TwitchSettings TwitchSettings { get; set; } = new TwitchSettings();
        public MenuColorSettings MenuSettings { get; set; } = new MenuColorSettings();
        public NameBiddingSettings NameBiddingSettings { get; set; } = new NameBiddingSettings();
        public BattleSettings BattleSettings { get; set; } = new BattleSettings();
        public EquipmentSettings EquipmentSettings { get; set; } = new EquipmentSettings();
        public ItemSettings ItemSettings { get; set; } = new ItemSettings();
        public MateriaSettings MateriaSettings { get; set; } = new MateriaSettings();
        public TsengSettings TsengSettings { get; set; } = new TsengSettings();

        public static void LoadFromJson(string json, Action<Exception> errorLogging = null)
        {
            try
            {
                JsonConvert.PopulateObject(json, Instance);
            }
            catch (Exception ex)
            {
                Instance = new ApplicationSettings();
                errorLogging?.Invoke(ex);
            }
        }

        /// <summary>
        /// Temporary solution to the duplicate load in collections issue.
        /// </summary>
        public void CleanUpCollections()
        {
            RemoveDuplicates(BattleSettings.AllStatusEffects);
            RemoveDuplicates(EquipmentSettings.AllAccessories);
            RemoveDuplicates(EquipmentSettings.AllArmlets);
            RemoveDuplicates(EquipmentSettings.AllWeapons);
        }

        /// <summary>
        /// Keep the later item, since that should be the one from their local store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        private void RemoveDuplicates<T>(List<T> collection)
            where T : INamedSetting
        {
            HashSet<string> names = new HashSet<string>();
            for (int i = collection.Count - 1; i >= 0; i--)
            {
                if (names.Contains(collection[i].Name))
                {
                    collection.RemoveAt(i);
                }
                else
                {
                    names.Add(collection[i].Name);
                }
            }
        }

    }
}