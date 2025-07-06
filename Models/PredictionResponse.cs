using System.Text.Json.Serialization;

namespace DiabetesApi.Models
{
    public class PredictionResponse
    {
       
            [JsonPropertyName("prediction_percentage")]
            public float PredictionPercentage { get; set; }

            [JsonPropertyName("recommendation")]
            public Recommendation Recommendation { get; set; }
    }
}
