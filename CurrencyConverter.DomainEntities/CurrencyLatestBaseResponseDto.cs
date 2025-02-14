using System.Text.Json.Serialization;

namespace CurrencyConverter.DomainEntities
{
    public class CurrencyLatestBaseResponseDto
    {
        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("rates")]
        public Rates Rates { get; set; }
    }

}
