using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Data.Items
{
    public class EquipmentData<T> where T : Equipment
    {
        private List<T> All { get; } = Items.All.OfType<T>().ToList();

        public T GetById(ushort id, CharNames charName = null)
            => All.SingleOrDefault(x => x.IsMatchById(id, charName));

        public T GetByItemId(ushort itemId, CharNames charName = null)
            => All.SingleOrDefault(x => x.IsMatchByItemId(itemId, charName));

        public T GetByEquipId(ushort equipId, CharNames charName = null)
            => All.SingleOrDefault(x => x.IsMatchByEquipId(equipId, charName));
    }
}