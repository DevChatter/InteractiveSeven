namespace InteractiveSeven.Core.Settings
{
    public interface ISettingsStore
    {
        void LoadSettings();
        void SaveSettings();
    }
}