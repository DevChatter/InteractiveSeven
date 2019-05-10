using System.IO;
using InteractiveSeven.Core.Settings;
using Newtonsoft.Json;

namespace InteractiveSeven.UI.Settings
{
    public class SettingsStore : ISettingsStore
    {
        const string SETTINGS_FILE_NAME = "appsettings.json";

        public void EnsureExists()
        {
            if (File.Exists(SETTINGS_FILE_NAME))
            {
                LoadSettings();
            }
            else
            {
                SaveSettings();
            }
        }

        public void LoadSettings()
        {
            string json = File.ReadAllText(SETTINGS_FILE_NAME);
            ApplicationSettings.LoadFromJson(json);
        }

        public void SaveSettings()
        {
            string text = JsonConvert.SerializeObject(ApplicationSettings.Instance);
            File.WriteAllText(SETTINGS_FILE_NAME, text);
        }
    }
}