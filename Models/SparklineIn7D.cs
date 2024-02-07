using System.Text.Json.Serialization;

namespace Models
{
    public class SparklineIn7D
    {
        [JsonPropertyName("price")]
        public decimal[] Price { get; set; }
    }
}
