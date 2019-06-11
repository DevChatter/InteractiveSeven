using System.Linq;

namespace InteractiveSeven.Core.Settings
{
    public class TwitchSettings
    {
        private string accessToken;

        public string Username { get; set; }
        public string AccessToken
        {
            get => accessToken;
            set
            {
                if (!value.All(x => x == '●'))
                {
                    accessToken = value;
                }
            }
        }
        public string Channel { get; set; }
    }
}