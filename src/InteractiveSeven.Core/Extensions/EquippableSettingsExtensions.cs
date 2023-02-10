using System.Collections.Generic;
using System.Linq;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core
{
    public static class EquippableSettingsExtensions
    {

        public static List<EquippableSettings> AllByName(this List<EquippableSettings> list, string name, CharNames charName)
        {
            IEnumerable<EquippableSettings> enabled = list.Where(x => x.Enabled);
            if (string.IsNullOrWhiteSpace(name))
            {
                return enabled.Where(x => x.Item.IsMatchByCharacter(charName)).ToList();
            }

            return enabled.Where(x => x.Item.IsMatchByName(name, charName)).ToList();
        }
    }
}