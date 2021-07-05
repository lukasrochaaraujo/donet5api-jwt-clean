using System.Text.Json.Serialization;

namespace WebApiJwt.Model
{
    public class AuthenticationModel
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}