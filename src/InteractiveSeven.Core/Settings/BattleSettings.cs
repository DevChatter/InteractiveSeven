using System.Linq;
using Newtonsoft.Json;
using InteractiveSeven.Core.Battle;

namespace InteractiveSeven.Core.Settings
{
    public class BattleSettings : ObservableSettingsBase
    {
        private bool _allowStatusEffects = false;
        public BattleSettings()
        {
            AllStatusEffects = new[]
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
        public StatusEffectSettings[] AllStatusEffects { get; set; }

        public StatusEffectSettings Sleep { get; set; }
            = new StatusEffectSettings(nameof(Sleep), StatusEffects.Sleep, true, 100, "sleep", "slp", "sleeping");
        public StatusEffectSettings Poison { get; set; }
            = new StatusEffectSettings(nameof(Poison), StatusEffects.Poison, true, 100, "psn", "poison");
        public StatusEffectSettings Sadness { get; set; }
            = new StatusEffectSettings(nameof(Sadness), StatusEffects.Sadness, true, 100, "sad", "sadness");
        public StatusEffectSettings Fury { get; set; }
            = new StatusEffectSettings(nameof(Fury), StatusEffects.Fury, true, 100, "fury", "hyper");
        public StatusEffectSettings Confusion { get; set; }
            = new StatusEffectSettings(nameof(Confusion), StatusEffects.Confusion, false, 200, "conf", "confusion");
        public StatusEffectSettings Silence { get; set; }
            = new StatusEffectSettings(nameof(Silence), StatusEffects.Silence, true, 200, "sil", "silence");
        public StatusEffectSettings Haste { get; set; }
            = new StatusEffectSettings(nameof(Haste), StatusEffects.Haste, true, 200, "haste", "hasted");
        public StatusEffectSettings Slow { get; set; }
            = new StatusEffectSettings(nameof(Slow), StatusEffects.Slow, true, 100, "slow", "slowed");
        public StatusEffectSettings Stop { get; set; }
            = new StatusEffectSettings(nameof(Stop), StatusEffects.Stop, false, 1000, "stop", "stopped");
        public StatusEffectSettings Frog { get; set; }
            = new StatusEffectSettings(nameof(Frog), StatusEffects.Frog, true, 200, "frog", "toad");
        public StatusEffectSettings Small { get; set; }
            = new StatusEffectSettings(nameof(Small), StatusEffects.Small, true, 200, "small");
        public StatusEffectSettings Regen { get; set; }
            = new StatusEffectSettings(nameof(Regen), StatusEffects.Regen, true, 200, "regen");
        public StatusEffectSettings Barrier { get; set; }
            = new StatusEffectSettings(nameof(Barrier), StatusEffects.Barrier, true, 200, "barrier");
        public StatusEffectSettings MBarrier { get; set; }
            = new StatusEffectSettings(nameof(MBarrier), StatusEffects.MBarrier, true, 200, "mbarrier");
        public StatusEffectSettings Reflect { get; set; }
            = new StatusEffectSettings(nameof(Reflect), StatusEffects.Reflect, true, 200, "reflect");
        public StatusEffectSettings Berserk { get; set; }
            = new StatusEffectSettings(nameof(Berserk), StatusEffects.Berserk, true, 200, "berserk");
        public StatusEffectSettings Darkness { get; set; }
            = new StatusEffectSettings(nameof(Darkness), StatusEffects.Darkness, true, 100, "darkness");
        public StatusEffectSettings Petrify { get; set; }
            = new StatusEffectSettings(nameof(Petrify), StatusEffects.Petrify, false, 1000, "petrify");
        public StatusEffectSettings Dual { get; set; }
            = new StatusEffectSettings(nameof(Dual), StatusEffects.Dual, false, 1000, "dual");
        public StatusEffectSettings Shield { get; set; }
            = new StatusEffectSettings(nameof(Shield), StatusEffects.Shield, true, 200, "shield");
        public StatusEffectSettings DeathSentence { get; set; }
            = new StatusEffectSettings(nameof(DeathSentence), StatusEffects.DeathSentence, true, 200, "deathsentence");
        public StatusEffectSettings Peerless { get; set; }
            = new StatusEffectSettings(nameof(Peerless), StatusEffects.Peerless, false, 500, "peerless");
        public StatusEffectSettings Paralyze { get; set; }
            = new StatusEffectSettings(nameof(Paralyze), StatusEffects.Paralyze, false, 1000, "paralyze");

        public StatusEffectSettings ByWord(string word)
        {
            return AllStatusEffects.FirstOrDefault(x => x.Words.Any(w => w.EqualsIns(word)));
        }
    }
}