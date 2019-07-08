using InteractiveSeven.Core.Battle;
using System.Linq;

namespace InteractiveSeven.Core.Settings
{
    public class BattleSettings : ObservableSettingsBase
    {
        private bool _allowStatusEffects = false;
        private bool _allowModOverride = true;

        public BattleSettings()
        {
            AllStatusEffects = new[]
            {
                new StatusEffectSettings("Barrier", StatusEffects.Barrier, true, 200, "barrier"),
                new StatusEffectSettings("Berserk", StatusEffects.Berserk, true, 200, "berserk"),
                new StatusEffectSettings("Confusion", StatusEffects.Confusion, false, 200, "conf", "confusion"),
                new StatusEffectSettings("Darkness", StatusEffects.Darkness, true, 100, "darkness"),
                new StatusEffectSettings("DeathSentence", StatusEffects.DeathSentence, true, 200, "deathsentence"),
                new StatusEffectSettings("Dual", StatusEffects.Dual, false, 1000, "dual"),
                new StatusEffectSettings("Frog", StatusEffects.Frog, true, 200, "frog", "toad"),
                new StatusEffectSettings("Fury", StatusEffects.Fury, true, 100, "fury", "hyper"),
                new StatusEffectSettings("Haste", StatusEffects.Haste, true, 200, "haste", "hasted"),
                new StatusEffectSettings("MBarrier", StatusEffects.MBarrier, true, 200, "mbarrier"),
                new StatusEffectSettings("Paralyze", StatusEffects.Paralyze, false, 1000, "paralyze"),
                new StatusEffectSettings("Peerless", StatusEffects.Peerless, false, 500, "peerless"),
                new StatusEffectSettings("Petrify", StatusEffects.Petrify, false, 1000, "petrify"),
                new StatusEffectSettings("Poison", StatusEffects.Poison, true, 100, "psn", "poison"),
                new StatusEffectSettings("Reflect", StatusEffects.Reflect, true, 200, "reflect"),
                new StatusEffectSettings("Regen", StatusEffects.Regen, true, 200, "regen"),
                new StatusEffectSettings("Sadness", StatusEffects.Sadness, true, 100, "sad", "sadness"),
                new StatusEffectSettings("Shield", StatusEffects.Shield, true, 200, "shield"),
                new StatusEffectSettings("Silence", StatusEffects.Silence, true, 200, "sil", "silence"),
                new StatusEffectSettings("Sleep", StatusEffects.Sleep, true, 100, "sleep", "slp", "sleeping"),
                new StatusEffectSettings("Slow", StatusEffects.Slow, true, 100, "slow", "slowed"),
                new StatusEffectSettings("Small", StatusEffects.Small, true, 200, "small"),
                new StatusEffectSettings("Stop", StatusEffects.Stop, false, 1000, "stop", "stopped"),
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

        public bool AllowModOverride
        {
            get => _allowModOverride;
            set
            {
                _allowModOverride = value;
                OnPropertyChanged();
            }
        }

        public StatusEffectSettings[] AllStatusEffects { get; set; }

        public StatusEffectSettings ByWord(string word)
        {
            return AllStatusEffects.FirstOrDefault(x => x.Words.Any(w => w.EqualsIns(word)));
        }
    }
}