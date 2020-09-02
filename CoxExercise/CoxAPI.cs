using CoxExercise.Models;
using Newtonsoft.Json;
using NJsonSchema.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoxExercise
{
    public class CoxAPI : ICoxAPI
    {
        private ICoxHttpClient _httpClient;
        public CoxAPI(ICoxHttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetDataSetId()
        {
            var data = await _httpClient.Get("datasetId");
            var dataset = JsonConvert.DeserializeObject<DataSet>(data);
            return dataset.DatasetId;
        }

        public async Task<Vehicles> GetVehiclesIds(string datasetId)
        {
            var data = await _httpClient.Get($"{datasetId}/vehicles");
            var vehicleIds = JsonConvert.DeserializeObject<Vehicles>(data);
            return vehicleIds;
        }

        public async Task<Vehicle> GetVehicle(string datasetId, int vehicleId)
        {
            var data = await _httpClient.Get($"{datasetId}/vehicles/{vehicleId}");
            var vehicle = JsonConvert.DeserializeObject<Vehicle>(data);
            return vehicle;
        }


        public async Task<string> Save(string datasetId, Answer answer)
        {
            var results =  await _httpClient.Post($"{datasetId}/answer", SerializeAnswer(answer));
            return results;
        }

        public async Task<Dealer> GetDealer(string datasetId, int dealerId)
        {
            var data = await _httpClient.Get($"{datasetId}/dealers/{dealerId}");
            var dealer = JsonConvert.DeserializeObject<Dealer>(data);
            return dealer;
        }

        private string SerializeAnswer(Answer answerViewModel)
        {
            var jsonResolver = new PropertyRenameAndIgnoreSerializerContractResolver();
            jsonResolver.IgnoreProperty(typeof(Vehicle), "dealerId");

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = jsonResolver;

            var json = JsonConvert.SerializeObject(answerViewModel, serializerSettings);
            return json;
        }
    }
}
