using System.Collections.Generic;

namespace InteractiveSeven.Core.Data
{
    public interface IDataStore<T>
    {
        void SaveData(List<T> items);
        List<T> LoadData();
    }
}