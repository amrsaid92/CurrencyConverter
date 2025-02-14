using System.Text.Json.Serialization;

namespace CurrencyConverter.DomainEntities.Account
{
    public class JwtTokenDto
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
    }
}
