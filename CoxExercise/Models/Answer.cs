using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoxExercise.Models
{
    public class Answer
    {
        [JsonProperty(PropertyName = "dealers")]
        public List<Dealer> Dealers { get; set; }

        public Answer()
        {
            Dealers = new List<Dealer>();
        }
    }
}
