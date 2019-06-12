using System.Linq;

namespace InteractiveSeven.Core.Settings
{
    public class TwitchSettings
    {
        private string _accessToken;

        public string Username { get; set; }
        public string AccessToken
        {
            get => _accessToken;
            set
            {
                if (!value.All(x => x == '●'))
                {
                    _accessToken = value;
                }
            }
        }
        public string Channel { get; set; }
    }
}