using Newtonsoft.Json;

namespace CoxExercise.Models
{
    public class FinalResponse
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "totalMilliseconds")]
        public decimal? TotalMilliseconds { get; set; }
    }
}
