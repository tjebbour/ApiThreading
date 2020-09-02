using CoxExercise.Models;
using System.Threading.Tasks;

namespace CoxExercise
{
    public interface ICoxAPI
    {
        Task<string> GetDataSetId();
        Task<Vehicles> GetVehiclesIds(string datasetId);
        Task<Vehicle> GetVehicle(string datasetId, int vehicleId);
        Task<Dealer> GetDealer(string datasetId, int dealerId);
        Task<string> Save(string datasetId, Answer answer);
    }
}
