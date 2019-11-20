using Newtonsoft.Json;
using System;
using System.IO;

namespace InteractiveSeven.Core.Settings
{
    public class SettingsStore : ISettingsStore
    {
        const string SETTINGS_FILE_NAME = "i7.json";

        public void EnsureExists(Action<Exception> errorLogging = null)
        {
            if (File.Exists(SETTINGS_FILE_NAME))
            {
                LoadSettings(errorLogging);
            }
            else
            {
                SaveSettings();
            }
        }

        public void LoadSettings(Action<Exception> errorLogging = null)
        {
            string json = File.ReadAllText(SETTINGS_FILE_NAME);
            ApplicationSettings.LoadFromJson(json, errorLogging);
            ApplicationSettings.Instance.CleanUpCollections();
        }

        public void SaveSettings()
        {
            string text = JsonConvert.SerializeObject(ApplicationSettings.Instance);
            File.WriteAllText(SETTINGS_FILE_NAME, text);
        }
    }
}