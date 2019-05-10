using Newtonsoft.Json;
using System;

namespace InteractiveSeven.Core.Settings
{
    public class ApplicationSettings
    {
        public static ApplicationSettings Instance { get; private set; }
        static ApplicationSettings()
        {
            Instance = new ApplicationSettings();
        }

        public string ProcessName { get; set; } = "ff7_en";
        public MenuColorSettings MenuSettings { get; set; } = new MenuColorSettings();
        public NameBiddingSettings NameBiddingSettings { get; set; } = new NameBiddingSettings();

        public static void LoadFromJson(string json)
        {
            try
            {
                JsonConvert.PopulateObject(json, Instance);
            }
            catch (Exception)
            {
                Instance = new ApplicationSettings();
                // gulp
            }
        }
    }

    public class MenuColorSettings
    {
        public bool Enabled { get; set; } = true;
        public int BitCost { get; set; }
    }

    public class NameBiddingSettings
    {
        public bool Enabled { get; set; }
        public NamingSettings NamingCloud { get; set; } = new NamingSettings();
        public NamingSettings NamingBarret { get; set; } = new NamingSettings();
        public NamingSettings NamingTifa { get; set; } = new NamingSettings();
        public NamingSettings NamingAeris { get; set; } = new NamingSettings();
        public NamingSettings NamingCaitSith { get; set; } = new NamingSettings();
        public NamingSettings NamingCid { get; set; } = new NamingSettings();
        public NamingSettings NamingRed { get; set; } = new NamingSettings();
        public NamingSettings NamingVincent { get; set; } = new NamingSettings();
        public NamingSettings NamingYuffie { get; set; } = new NamingSettings();
    }

    public class NamingSettings
    {
        public bool Enabled { get; set; } = true;
    }
}