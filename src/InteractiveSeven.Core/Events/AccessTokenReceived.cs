namespace InteractiveSeven.Core.Events
{
    public class AccessTokenReceived : BaseDomainEvent
    {
        public string AccessToken { get; set; }
        public string Scope { get; set; }
        public string State { get; set; }
        public string TokenType { get; set; }

        public AccessTokenReceived()
        {
        }

        public AccessTokenReceived(string accessToken, string scope, string state, string tokenType)
        {
            AccessToken = accessToken;
            Scope = scope;
            State = state;
            TokenType = tokenType;
        }
    }
}