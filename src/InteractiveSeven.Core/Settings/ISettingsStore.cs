namespace InteractiveSeven.Core.Settings
{
    public interface ISettingsStore
    {
        void EnsureExists();
        void LoadSettings();
        void SaveSettings();
    }
}