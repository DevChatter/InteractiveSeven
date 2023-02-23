using InteractiveSeven.Core.Settings;
using StreamingClient.Base.Model.OAuth;

namespace Chat.YouTube
{
    public static class OAuthTokenModelExtensions
    {
        public static void MapOntoSettings(this OAuthTokenModel token, YouTubeSettings settings)
        {
            settings.AccessToken = token.accessToken;
            settings.AcquiredDateTime = token.AcquiredDateTime;
            settings.AuthorizationCode = token.authorizationCode;
            settings.ClientId = token.clientID;
            settings.ClientSecret = token.clientSecret;
            settings.ExpiresIn = token.expiresIn;
            settings.ExpiresTimeStamp = token.expiresTimeStamp;
            settings.RedirectUrl = token.redirectUrl;
            settings.RefreshToken = token.refreshToken;
        }

        public static OAuthTokenModel ToToken(this YouTubeSettings token)
        {
            return new OAuthTokenModel
            {
                accessToken = token.AccessToken,
                AcquiredDateTime = token.AcquiredDateTime,
                authorizationCode = token.AuthorizationCode,
                clientID = token.ClientId,
                clientSecret = token.ClientSecret,
                expiresIn = token.ExpiresIn,
                expiresTimeStamp = token.ExpiresTimeStamp,
                redirectUrl = token.RedirectUrl,
                refreshToken = token.RefreshToken,
            };
        }
    }
}
