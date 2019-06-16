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

    public class BattleSettings : ObservableSettingsBase
    {
        private bool _allowStatusEffects = false;
        public BattleSettings()
        {
            StatusEffects = new[]
            {
                Barrier,
                Berserk,
                Confusion,
                Darkness,
                Frog,
                Fury,
                Haste,
                MBarrier,
                Poison,
                Reflect,
                Regen,
                Sadness,
                Silence,
                Sleep,
                Slow,
                Small,
                Stop,
            };
        }

        public bool AllowStatusEffects
        {
            get => _allowStatusEffects;
            set
            {
                _allowStatusEffects = value;
                OnPropertyChanged();
            }
        }


        [JsonIgnore]
        public StatusEffectSettings[] StatusEffects { get; set; }

        public StatusEffectSettings Sleep { get; set; } = new StatusEffectSettings{ Name = nameof(Sleep) };
        public StatusEffectSettings Poison { get; set; } = new StatusEffectSettings{ Name = nameof(Poison)};
        public StatusEffectSettings Sadness { get; set; } = new StatusEffectSettings{ Name = nameof(Sadness)};
        public StatusEffectSettings Fury { get; set; } = new StatusEffectSettings{ Name = nameof(Fury)};
        public StatusEffectSettings Confusion { get; set; } = new StatusEffectSettings{ Name = nameof(Confusion)};
        public StatusEffectSettings Silence { get; set; } = new StatusEffectSettings{ Name = nameof(Silence)};
        public StatusEffectSettings Haste { get; set; } = new StatusEffectSettings{ Name = nameof(Haste)};
        public StatusEffectSettings Slow { get; set; } = new StatusEffectSettings{ Name = nameof(Slow)};
        public StatusEffectSettings Stop { get; set; } = new StatusEffectSettings{ Name = nameof(Stop)};
        public StatusEffectSettings Frog { get; set; } = new StatusEffectSettings{ Name = nameof(Frog)};
        public StatusEffectSettings Small { get; set; } = new StatusEffectSettings{ Name = nameof(Small)};
        public StatusEffectSettings Regen { get; set; } = new StatusEffectSettings{ Name = nameof(Regen)};
        public StatusEffectSettings Barrier { get; set; } = new StatusEffectSettings{ Name = nameof(Barrier)};
        public StatusEffectSettings MBarrier { get; set; } = new StatusEffectSettings{ Name = nameof(MBarrier)};
        public StatusEffectSettings Reflect { get; set; } = new StatusEffectSettings{ Name = nameof(Reflect)};
        public StatusEffectSettings Berserk { get; set; } = new StatusEffectSettings{ Name = nameof(Berserk)};
        public StatusEffectSettings Darkness { get; set; } = new StatusEffectSettings{ Name = nameof(Darkness)};
    }
}