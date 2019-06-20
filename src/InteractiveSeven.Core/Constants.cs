using System;

namespace InteractiveSeven.Core
{
    public static class Constants
    {
        public const string AppDataFolder = "InteractiveSeven";
        public static readonly string LocalAppData
            = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    }
}