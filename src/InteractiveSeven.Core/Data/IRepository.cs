using System.Collections.Generic;

namespace InteractiveSeven.Core.Data
{
    public interface IRepository
    {
        List<Setting> GetAllSettings();
    }
}