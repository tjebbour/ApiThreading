using CoxExercise.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NJsonSchema.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoxExercise
{
    public class ApiExercise
    {
        public event EventHandler<string> Notify;

        private readonly ICoxAPI coxAPI;
        private readonly ITimeTracker timeTracker;
        public ApiExercise(ICoxAPI api, ITimeTracker tracker)
        {
            coxAPI = api;
            timeTracker = tracker;
        }

        protected virtual void NotifyClients(string e)
        {
            Notify?.Invoke(this, e);
        }
        public void Run()
        {
            Task.Run(async () =>
            {
                timeTracker.Start();

                var datasetId = await coxAPI.GetDataSetId();
                NotifyClients($"Dataset # {datasetId} created sucessfully");

                var vehicleIds = await coxAPI.GetVehiclesIds(datasetId);

                if (vehicleIds == null )
                    return;

                NotifyClients($"Found {vehicleIds.VehicleIds.Count()} vehicles for that dataset");

                var vehicles = new ConcurrentBag<Vehicle>();
                var dealers = new ConcurrentBag<Dealer>();

                var vehicleFetchingTasks = vehicleIds.VehicleIds.Distinct().Select(async vehicleId =>
                {
                    vehicles.Add(await coxAPI.GetVehicle(datasetId, vehicleId));
                });
              
                await Task.WhenAll(vehicleFetchingTasks);
                NotifyClients($"Vehicle data returned successfully");

                var dealerFetchingTasks = vehicles.Select(p => p.DealerId).Distinct().Select(async dealerId =>
                {
                    dealers.Add(await coxAPI.GetDealer(datasetId, dealerId));
                });

                await Task.WhenAll(dealerFetchingTasks);
                NotifyClients($"Dealers data returned successfully");

                var answer = new Answer();

                if (dealers == null)
                    return;

                foreach (var dealer in dealers)
                {
                    dealer.Vehicles = vehicles.Where(p => p.DealerId == dealer.DealerId).ToList();
                    answer.Dealers.Add(dealer);
                }

                NotifyClients($"Posting answer to the server: ");
                var finalResponse = await coxAPI.Save(datasetId, answer);

                timeTracker.Stop();
                NotifyClients($"Total processing time: {timeTracker.TotalSeconds} seconds.");
                NotifyClients($"Final response: {finalResponse}");
            }
            );


        }

    }
}
