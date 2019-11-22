using InteractiveSeven.Core.Battle;

namespace InteractiveSeven.Core.FinalFantasy.Models
{
    public class Accessory
    {
        public ushort Id { get; set; }
        public string Name { get; set; }
        public StatusEffects StatusDefense { get; set; }

        public bool ProtectsFrom(StatusEffects statusEffect)
            => ((int)StatusDefense & (int)statusEffect) > 0;
    }
}
