using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Settings;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core
{
    public static class EquippableSettingsExtensions
    {
        public static EquippableSettings FindByValue(this List<EquippableSettings> list, string value, CharNames charName)
        {
            IEnumerable<EquippableSettings> enabled = list.Where(x => x.Enabled);
            if (ushort.TryParse(value, out ushort id))
            {
                return enabled.FirstOrDefault(x => x.Item.IsMatchByEquipId(id, charName));
            }

            return enabled.FirstOrDefault(x => x.Words.Any(w => w.EqualsIns(value)));
        }
    }
}