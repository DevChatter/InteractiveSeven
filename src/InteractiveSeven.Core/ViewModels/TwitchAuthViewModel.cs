using System;

namespace InteractiveSeven.Core.ViewModels
{
    public class TwitchAuthViewModel
    {
        public const string ClientId = "819i0gu4u7q0gts6y44wd88uuuhtpd";
        public const string RedirectUrl = "http://localhost:7777";
        public static string[] Scopes = {
            "bits:read",
            "channel:read:subscriptions",
            "chat:edit",
            "chat:read",
            "whispers:read",
            "whispers:edit"};
        public static string ScopeString = string.Join("+", value: Scopes);

        public static readonly string State = Guid.NewGuid().ToString();

        public string AuthRequestUrl =>
            $"https://id.twitch.tv/oauth2/authorize?client_id={ClientId}&redirect_uri={RedirectUrl}&response_type=token&scope={ScopeString}&state={State}";
    }
}