namespace WebApiJwt.Models
{
    public class AuthorizationToken
    {
        public AuthorizationToken(string token)
        {
            AccessToken = token;
        }

        public string AccessToken { get; private set; }
    }
}