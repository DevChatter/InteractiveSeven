using InteractiveSeven.Core.Data;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveSeven.Core.Settings
{
    public class ApplicationSettings
    {
        public static ApplicationSettings Instance { get; }
        static ApplicationSettings()
        {
            Instance = new ApplicationSettings();
        }

        public Dictionary<string, Setting> Settings { get; private set; }

        public void Initialize(List<Setting> settings)
        {
            Settings = settings.ToDictionary(k => k.Name);
        }
    }
}