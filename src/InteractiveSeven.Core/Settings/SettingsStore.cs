using System;
using System.IO;
using Newtonsoft.Json;

namespace InteractiveSeven.Core.Settings
{
    public class SettingsStore : ISettingsStore
    {
        const string SETTINGS_FILE_NAME = "i7.json";
        const string TWITCH_SETTINGS_FILE_NAME = "i7-twitch.json";

        public void EnsureExists(Action<Exception> errorLogging = null)
        {
            if (File.Exists(SETTINGS_FILE_NAME))
            {
                LoadMainSettings(errorLogging);
            }
            else
            {
                SaveMainSettings();
            }
            if (File.Exists(TWITCH_SETTINGS_FILE_NAME))
            {
                LoadTwitchSettings(errorLogging);
            }
            else
            {
                SaveTwitchSettings();
            }
        }

        private void LoadMainSettings(Action<Exception> errorLogging = null)
        {
            string json = File.ReadAllText(SETTINGS_FILE_NAME);
            ApplicationSettings.LoadFromJson(json, errorLogging);
            ApplicationSettings.Instance.CleanUpCollections();
        }

        private void SaveMainSettings()
        {
            string text = JsonConvert.SerializeObject(ApplicationSettings.Instance);
            File.WriteAllText(SETTINGS_FILE_NAME, text);
        }

        private void LoadTwitchSettings(Action<Exception> errorLogging = null)
        {
            string json = File.ReadAllText(TWITCH_SETTINGS_FILE_NAME);
            TwitchSettings.LoadFromJson(json, errorLogging);
        }

        private void SaveTwitchSettings()
        {
            string text = JsonConvert.SerializeObject(TwitchSettings.Instance);
            File.WriteAllText(TWITCH_SETTINGS_FILE_NAME, text);
        }

        public void LoadSettings(Action<Exception> errorLogging = null)
        {
            LoadTwitchSettings(errorLogging);
            LoadMainSettings(errorLogging);
        }

        public void SaveSettings()
        {
            SaveTwitchSettings();
            SaveMainSettings();
        }
    }
}