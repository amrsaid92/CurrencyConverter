using System.Text.Json.Serialization;

namespace CurrencyConverter.DomainEntities
{
    public class Rates
    {
        [JsonPropertyName("AUD")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double AUD { get; set; }

        [JsonPropertyName("EUR")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double EUR { get; set; }

        [JsonPropertyName("BGN")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double BGN { get; set; }

        [JsonPropertyName("BRL")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double BRL { get; set; }

        [JsonPropertyName("CAD")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double CAD { get; set; }

        [JsonPropertyName("CHF")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double CHF { get; set; }

        [JsonPropertyName("CNY")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double CNY { get; set; }

        [JsonPropertyName("CZK")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double CZK { get; set; }

        [JsonPropertyName("DKK")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double DKK { get; set; }

        [JsonPropertyName("GBP")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double GBP { get; set; }

        [JsonPropertyName("HKD")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double HKD { get; set; }

        [JsonPropertyName("HUF")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double HUF { get; set; }

        [JsonPropertyName("IDR")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double IDR { get; set; }

        [JsonPropertyName("ILS")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double ILS { get; set; }

        [JsonPropertyName("INR")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double INR { get; set; }

        [JsonPropertyName("ISK")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double ISK { get; set; }

        [JsonPropertyName("JPY")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double JPY { get; set; }

        [JsonPropertyName("KRW")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double KRW { get; set; }

        [JsonPropertyName("MYR")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double MYR { get; set; }

        [JsonPropertyName("NOK")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double NOK { get; set; }

        [JsonPropertyName("NZD")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double NZD { get; set; }

        [JsonPropertyName("PHP")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double PHP { get; set; }


        [JsonPropertyName("RON")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double RON { get; set; }

        [JsonPropertyName("SEK")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double SEK { get; set; }

        [JsonPropertyName("SGD")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double SGD { get; set; }

        [JsonPropertyName("USD")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double USD { get; set; }

        [JsonPropertyName("ZAR")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double ZAR { get; set; }
    }
}
