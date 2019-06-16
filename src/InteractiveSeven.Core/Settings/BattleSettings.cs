using Newtonsoft.Json;

namespace InteractiveSeven.Core.Settings
{
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

        public StatusEffectSettings Sleep { get; set; }
            = new StatusEffectSettings{ Name = nameof(Sleep) };
        public StatusEffectSettings Poison { get; set; }
            = new StatusEffectSettings{ Name = nameof(Poison)};
        public StatusEffectSettings Sadness { get; set; }
            = new StatusEffectSettings{ Name = nameof(Sadness)};
        public StatusEffectSettings Fury { get; set; }
            = new StatusEffectSettings{ Name = nameof(Fury)};
        public StatusEffectSettings Confusion { get; set; }
            = new StatusEffectSettings{ Name = nameof(Confusion)};
        public StatusEffectSettings Silence { get; set; }
            = new StatusEffectSettings{ Name = nameof(Silence)};
        public StatusEffectSettings Haste { get; set; }
            = new StatusEffectSettings{ Name = nameof(Haste)};
        public StatusEffectSettings Slow { get; set; }
            = new StatusEffectSettings{ Name = nameof(Slow)};
        public StatusEffectSettings Stop { get; set; }
            = new StatusEffectSettings{ Name = nameof(Stop)};
        public StatusEffectSettings Frog { get; set; }
            = new StatusEffectSettings{ Name = nameof(Frog)};
        public StatusEffectSettings Small { get; set; }
            = new StatusEffectSettings{ Name = nameof(Small)};
        public StatusEffectSettings Regen { get; set; }
            = new StatusEffectSettings{ Name = nameof(Regen)};
        public StatusEffectSettings Barrier { get; set; }
            = new StatusEffectSettings{ Name = nameof(Barrier)};
        public StatusEffectSettings MBarrier { get; set; }
            = new StatusEffectSettings{ Name = nameof(MBarrier)};
        public StatusEffectSettings Reflect { get; set; }
            = new StatusEffectSettings{ Name = nameof(Reflect)};
        public StatusEffectSettings Berserk { get; set; }
            = new StatusEffectSettings{ Name = nameof(Berserk)};
        public StatusEffectSettings Darkness { get; set; }
            = new StatusEffectSettings{ Name = nameof(Darkness)};
    }
}