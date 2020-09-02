using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoxExercise.Models
{
    public class Dealer
    {
        [JsonProperty(PropertyName = "dealerId")]
        public int DealerId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "vehicles")]
        public List<Vehicle> Vehicles { get; set; }

        public Dealer()
        {
            Vehicles = new List<Vehicle>();
        }
    }


}
