using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using InteractiveSeven.Core.Commands.Battle;

namespace InteractiveSeven.Core.Settings;

public partial class BattleSettings : ObservableObject
{
    [ObservableProperty]
    private bool _allowStatusEffects = false;
    [ObservableProperty]
    private bool _allowEsunaCommand = true;
    [ObservableProperty]
    private int _esunaCost = 500;
    [ObservableProperty]
    private bool _allowModOverride = true;

    public BattleSettings()
    {
        AllStatusEffects = new List<StatusEffectSettings>
            {
                new ("Barrier", StatusEffects.Barrier, true, 200, 100, "barrier", "berrier"),
                new ("Berserk", StatusEffects.Berserk, true, 200, 100, "berserk", "brsrk"),
                new ("Confusion", StatusEffects.Confusion, false, 200, 100, "confusion", "conf", "confuse", "confuze", "confused"),
                new ("Darkness", StatusEffects.Darkness, true, 100, 100, "darkness", "dark", "blind", "blindness"),
                new ("DeathSentence", StatusEffects.DeathSentence, true, 200, 100, "deathsentence"),
                //new ("Dual", StatusEffects.Dual, false, 1000, 100, "dual"),
                new ("Frog", StatusEffects.Frog, true, 200, 100, "frog", "toad"),
                new ("Fury", StatusEffects.Fury, true, 100, 100, "fury", "hyper"),
                new ("Haste", StatusEffects.Haste, true, 200, 100, "haste", "hasted"),
                new ("MBarrier", StatusEffects.MBarrier, true, 200, 100, "mbarrier", "magicbarrier"),
                new ("Paralyze", StatusEffects.Paralyzed, false, 1000, 100, "paralyze", "paralyzed", "para", "paralysis"),
                new ("Peerless", StatusEffects.Peerless, false, 500, 100, "peerless"),
                new ("Petrify", StatusEffects.Petrify, false, 1000, 100, "petrify", "petrified", "petrification"),
                new ("Poison", StatusEffects.Poison, true, 100, 100, "psn", "poison"),
                new ("Reflect", StatusEffects.Reflect, true, 200, 100, "reflect"),
                new ("Regen", StatusEffects.Regen, true, 200, 100, "regen", "regenerate"),
                new ("Sadness", StatusEffects.Sadness, true, 100, 100, "sad", "sadness"),
                new ("Shield", StatusEffects.Shield, true, 200, 100, "shield"),
                new ("Silence", StatusEffects.Silence, true, 200, 100, "sil", "silence"),
                new ("Sleep", StatusEffects.Sleep, true, 100, 100, "sleep", "slp", "sleeping"),
                new ("Slow", StatusEffects.Slow, true, 100, 100, "slow", "slowed"),
                new ("Small", StatusEffects.Small, true, 200, 100, "small", "tiny"),
                new ("Stop", StatusEffects.Stop, false, 1000, 100, "stop", "stopped"),
            };
    }

    public List<StatusEffectSettings> AllStatusEffects { get; set; }

    public StatusEffectSettings ByWord(string word)
    {
        return AllStatusEffects.FirstOrDefault(x => x.Words.Any(w => w.EqualsIns(word)));
    }
}
