using CoxExercise.Models;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace CoxExercise
{
    public class ApiExercise
    {
        private readonly ICoxAPI coxAPI;
        private readonly ITimeTracker timeTracker;
        public event EventHandler<string> Notify;

        public ApiExercise(ICoxAPI api, ITimeTracker tracker)
        {
            coxAPI = api;
            timeTracker = tracker;
        }

        protected virtual void NotifyClients(string e)
        {
            Notify?.Invoke(this, e);
        }
        public async Task<bool> Run()
        {
            try
            {
                timeTracker.Start();

                var datasetId = await coxAPI.GetDataSetId();
                NotifyClients($"Dataset # {datasetId} created sucessfully");

                var vehicleIds = await coxAPI.GetVehiclesIds(datasetId);

                if (vehicleIds == null)
                    return false;

                NotifyClients($"Found {vehicleIds.VehicleIds.Count()} vehicles for that dataset");

                var vehicles = new ConcurrentBag<Vehicle>();
                var dealers = new ConcurrentBag<Dealer>();

                var vehicleFetchingTasks = vehicleIds.VehicleIds.Distinct().Select(async vehicleId => { vehicles.Add(await coxAPI.GetVehicle(datasetId, vehicleId)); });

                await Task.WhenAll(vehicleFetchingTasks);
                NotifyClients($"Vehicle data returned successfully");

                var dealerFetchingTasks = vehicles.Select(p => p.DealerId).Distinct().Select(async dealerId => { dealers.Add(await coxAPI.GetDealer(datasetId, dealerId)); });

                await Task.WhenAll(dealerFetchingTasks);
                NotifyClients($"Dealers data returned successfully");

                var answer = new Answer();

                if (dealers == null)
                    return false;

                foreach (var dealer in dealers)
                {
                    dealer.Vehicles = vehicles.Where(p => p.DealerId == dealer.DealerId).ToList();
                    answer.Dealers.Add(dealer);
                }

                NotifyClients($"Posting the answer to the server");
                var finalResponse = await coxAPI.Save(datasetId, answer);

                timeTracker.Stop();

                NotifyClients($"\nTotal processing time: {timeTracker.TotalSeconds} seconds.");
                NotifyClients($"Final response: {finalResponse}");

                return true;

            }
            catch (Exception ex)
            {
                NotifyClients($"Error: {ex}");
                return false;
            }
        }
    }
}
