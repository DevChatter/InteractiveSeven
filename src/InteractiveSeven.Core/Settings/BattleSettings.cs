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
                DeathSentence,
                Dual,
                Frog,
                Fury,
                Haste,
                MBarrier,
                Paralyze,
                Peerless,
                Petrify,
                Poison,
                Reflect,
                Regen,
                Sadness,
                Silence,
                Sleep,
                Slow,
                Small,
                Stop,
                Shield,
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
            = new StatusEffectSettings(nameof(Sleep), true, 100);
        public StatusEffectSettings Poison { get; set; }
            = new StatusEffectSettings(nameof(Poison), true, 100);
        public StatusEffectSettings Sadness { get; set; }
            = new StatusEffectSettings(nameof(Sadness), true, 100);
        public StatusEffectSettings Fury { get; set; }
            = new StatusEffectSettings(nameof(Fury), true, 100);
        public StatusEffectSettings Confusion { get; set; }
            = new StatusEffectSettings(nameof(Confusion), false, 200);
        public StatusEffectSettings Silence { get; set; }
            = new StatusEffectSettings(nameof(Silence), true, 200);
        public StatusEffectSettings Haste { get; set; }
            = new StatusEffectSettings(nameof(Haste), true, 200);
        public StatusEffectSettings Slow { get; set; }
            = new StatusEffectSettings(nameof(Slow), true, 100);
        public StatusEffectSettings Stop { get; set; }
            = new StatusEffectSettings(nameof(Stop), false, 1000);
        public StatusEffectSettings Frog { get; set; }
            = new StatusEffectSettings(nameof(Frog), true, 200);
        public StatusEffectSettings Small { get; set; }
            = new StatusEffectSettings(nameof(Small), true, 200);
        public StatusEffectSettings Regen { get; set; }
            = new StatusEffectSettings(nameof(Regen), true, 200);
        public StatusEffectSettings Barrier { get; set; }
            = new StatusEffectSettings(nameof(Barrier), true, 200);
        public StatusEffectSettings MBarrier { get; set; }
            = new StatusEffectSettings(nameof(MBarrier), true, 200);
        public StatusEffectSettings Reflect { get; set; }
            = new StatusEffectSettings(nameof(Reflect), true, 200);
        public StatusEffectSettings Berserk { get; set; }
            = new StatusEffectSettings(nameof(Berserk), true, 200);
        public StatusEffectSettings Darkness { get; set; }
            = new StatusEffectSettings(nameof(Darkness), true, 100);
        public StatusEffectSettings Petrify { get; set; }
            = new StatusEffectSettings(nameof(Petrify), false, 1000);
        public StatusEffectSettings Dual { get; set; }
            = new StatusEffectSettings(nameof(Dual), false, 1000);
        public StatusEffectSettings Shield { get; set; }
            = new StatusEffectSettings(nameof(Shield), true, 200);
        public StatusEffectSettings DeathSentence { get; set; }
            = new StatusEffectSettings(nameof(DeathSentence), true, 200);
        public StatusEffectSettings Peerless { get; set; }
            = new StatusEffectSettings(nameof(Peerless), false, 500);
        public StatusEffectSettings Paralyze { get; set; }
            = new StatusEffectSettings(nameof(Paralyze), false, 1000);
    }
}