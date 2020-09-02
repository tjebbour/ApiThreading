using Newtonsoft.Json;

namespace CoxExercise.Models
{
    public class Vehicle
    {
        [JsonProperty(PropertyName = "dealerId")]
        public int DealerId { get; set; }

        [JsonProperty(PropertyName = "vehicleId")]
        public int VehicleId { get; set; }

        [JsonProperty(PropertyName = "year")]
        public int Year { get; set; }

        [JsonProperty(PropertyName = "make")]
        public string Make { get; set; }

        [JsonProperty(PropertyName = "model")]
        public string Model { get; set; }
    }
}
