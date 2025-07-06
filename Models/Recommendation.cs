using System.Text.Json.Serialization;

namespace DiabetesApi.Models
{
    public class Recommendation
    {
        [JsonPropertyName("risk_level")]
        public string RiskLevel { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("actions")]
        public List<string> Actions { get; set; }
    }
}
