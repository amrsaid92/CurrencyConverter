using System.Text.Json.Serialization;

namespace CurrencyConverter.DomainEntities
{
    public class CurrencyRangeBaseResponseDto
    {
        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; }

        [JsonPropertyName("start_date")]
        public string StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public string EndDate { get; set; }

        [JsonPropertyName("rates")]
        public Dictionary<string, Rates> Rates { get; set; }
    }
}
