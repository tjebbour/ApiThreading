using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoxExercise.Models
{
    public class Vehicles
    {
        [JsonProperty(PropertyName = "vehicleIds")]
        public IEnumerable<int> VehicleIds { get; set; }
    }
}
