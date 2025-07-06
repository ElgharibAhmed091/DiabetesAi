using System.Text.Json.Serialization;

namespace DiabetesApi.Models
{
    public class PatientData
    {
        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("age")]
        public float Age { get; set; }

        [JsonPropertyName("hypertension")]
        public int Hypertension { get; set; }

        [JsonPropertyName("heart_disease")]
        public int Heart_Disease { get; set; }

        [JsonPropertyName("smoking_history")]
        public string Smoking_History { get; set; }

        [JsonPropertyName("bmi")]
        public float Bmi { get; set; }

        [JsonPropertyName("HbA1c_level")]
        public float HbA1c_Level { get; set; }

        [JsonPropertyName("blood_glucose_level")]
        public int Blood_Glucose_Level { get; set; }
    }
}
