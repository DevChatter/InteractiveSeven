using System;

namespace InteractiveSeven.Core.Settings
{
    public interface ISettingsStore
    {
        void LoadSettings(Action<Exception> errorLogging = null);
        void SaveSettings();
    }
}