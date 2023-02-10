using System.Collections.Generic;
using InteractiveSeven.Core.Battle;

namespace InteractiveSeven.Core
{
    public static class StatusEffectsExtensions
    {
        private static StatusEffects _withBlockingOpposites = StatusEffects.Sadness | StatusEffects.Fury;
        private static StatusEffects _withOpposites = StatusEffects.Slow | StatusEffects.Stop | StatusEffects.Haste;

        private static readonly Dictionary<StatusEffects, StatusEffects> Opposites
            = new Dictionary<StatusEffects, StatusEffects>
            {
                [StatusEffects.Sadness] = StatusEffects.Fury,
                [StatusEffects.Fury] = StatusEffects.Sadness,
                [StatusEffects.Haste] = StatusEffects.Slow | StatusEffects.Stop,
                [StatusEffects.Slow] = StatusEffects.Haste | StatusEffects.Stop,
                [StatusEffects.Stop] = StatusEffects.Haste | StatusEffects.Slow,
            };

        public static bool HasBlockingOpposite(this StatusEffects statusEffect)
            => (_withBlockingOpposites & statusEffect) == statusEffect;

        public static bool HasOpposite(this StatusEffects statusEffect)
            => (_withOpposites & statusEffect) == statusEffect;

        public static StatusEffects GetOpposite(this StatusEffects statusEffect)
            => Opposites[statusEffect];
    }
}