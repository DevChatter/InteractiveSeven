namespace InteractiveSeven.Twitch.Model
{
    public class ChatUserCheck
    {
        public string _id { get; set; }
        public string login { get; set; }
        public string display_name { get; set; }
        public string color { get; set; }
        public bool is_verified_bot { get; set; }
        public bool is_known_bot { get; set; }
        public Badge[] badges { get; set; }
    }

    public class Badge
    {
        public string id { get; set; }
        public string version { get; set; }
    }

}