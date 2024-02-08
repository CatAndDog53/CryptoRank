using System.Text.Json.Serialization;

namespace Models
{
    public class CoinShortData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
