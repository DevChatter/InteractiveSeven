using System.Configuration;

namespace InteractiveSeven.UI.Twitch
{
    public class TwitchSettings : ConfigurationSection
    {
        static TwitchSettings()
        {
            Settings = ConfigurationManager.GetSection("TwitchSettings") as TwitchSettings;
        }

        public static TwitchSettings Settings { get; }
            

        [ConfigurationProperty("Username", IsRequired = true)]
        //[StringValidator(MinLength = 1, MaxLength = 256)]
        public string Username
        {
            get => this["Username"].ToString();
            set => this["Username"] = value;
        }

        [ConfigurationProperty("AccessToken", IsRequired = true)]
        //[StringValidator(MinLength = 1, MaxLength = 256)]
        public string AccessToken
        {
            get => this["AccessToken"].ToString();
            set => this["AccessToken"] = value;
        }

        [ConfigurationProperty("Channel", IsRequired = true)]
        //[StringValidator(MinLength = 1, MaxLength = 256)]
        public string Channel
        {
            get => this["Channel"].ToString();
            set => this["Channel"] = value;
        }
    }
}