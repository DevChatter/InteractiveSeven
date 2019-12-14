using InteractiveSeven.Core.Battle;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Settings
{
    public class BattleSettings : ObservableSettingsBase
    {
        private bool _allowStatusEffects = false;
        private bool _allowEsunaCommand = true;
        private int _esunaCost = 500;
        private bool _allowModOverride = true;

        public BattleSettings()
        {
            AllStatusEffects = new List<StatusEffectSettings>
            {
                new StatusEffectSettings("Barrier", StatusEffects.Barrier, true, 200, 100, "barrier"),
                new StatusEffectSettings("Berserk", StatusEffects.Berserk, true, 200, 100, "berserk"),
                new StatusEffectSettings("Confusion", StatusEffects.Confusion, false, 200, 100, "conf", "confusion"),
                new StatusEffectSettings("Darkness", StatusEffects.Darkness, true, 100, 100, "darkness"),
                new StatusEffectSettings("DeathSentence", StatusEffects.DeathSentence, true, 200, 100, "deathsentence"),
                //new StatusEffectSettings("Dual", StatusEffects.Dual, false, 1000, 100, "dual"),
                new StatusEffectSettings("Frog", StatusEffects.Frog, true, 200, 100, "frog", "toad"),
                new StatusEffectSettings("Fury", StatusEffects.Fury, true, 100, 100, "fury", "hyper"),
                new StatusEffectSettings("Haste", StatusEffects.Haste, true, 200, 100, "haste", "hasted"),
                new StatusEffectSettings("MBarrier", StatusEffects.MBarrier, true, 200, 100, "mbarrier"),
                new StatusEffectSettings("Paralyze", StatusEffects.Paralyzed, false, 1000, 100, "paralyze", "paralyzed"),
                new StatusEffectSettings("Peerless", StatusEffects.Peerless, false, 500, 100, "peerless"),
                new StatusEffectSettings("Petrify", StatusEffects.Petrify, false, 1000, 100, "petrify"),
                new StatusEffectSettings("Poison", StatusEffects.Poison, true, 100, 100, "psn", "poison"),
                new StatusEffectSettings("Reflect", StatusEffects.Reflect, true, 200, 100, "reflect"),
                new StatusEffectSettings("Regen", StatusEffects.Regen, true, 200, 100, "regen"),
                new StatusEffectSettings("Sadness", StatusEffects.Sadness, true, 100, 100, "sad", "sadness"),
                new StatusEffectSettings("Shield", StatusEffects.Shield, true, 200, 100, "shield"),
                new StatusEffectSettings("Silence", StatusEffects.Silence, true, 200, 100, "sil", "silence"),
                new StatusEffectSettings("Sleep", StatusEffects.Sleep, true, 100, 100, "sleep", "slp", "sleeping"),
                new StatusEffectSettings("Slow", StatusEffects.Slow, true, 100, 100, "slow", "slowed"),
                new StatusEffectSettings("Small", StatusEffects.Small, true, 200, 100, "small"),
                new StatusEffectSettings("Stop", StatusEffects.Stop, false, 1000, 100, "stop", "stopped"),
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

        public bool AllowEsunaCommand
        {
            get => _allowEsunaCommand;
            set
            {
                _allowEsunaCommand = value;
                OnPropertyChanged();
            }
        }

        public int EsunaCost
        {
            get => _esunaCost;
            set
            {
                _esunaCost = value;
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

        public List<StatusEffectSettings> AllStatusEffects { get; set; }

        public StatusEffectSettings ByWord(string word)
        {
            return AllStatusEffects.FirstOrDefault(x => x.Words.Any(w => w.EqualsIns(word)));
        }
    }
}