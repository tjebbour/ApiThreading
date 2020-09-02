using CoxExercise.Models;
using System.Threading.Tasks;

namespace CoxExercise
{
    public interface ICoxAPI
    {
        /// <summary>
        /// Creates a dataset and returns its Id
        /// </summary>
        /// <returns></returns>
        Task<string> GetDataSetId();

        /// <summary>
        /// Gets all vehicles in the dataset
        /// </summary>
        /// <param name="datasetId"></param>
        /// <returns></returns>
        Task<Vehicles> GetVehiclesIds(string datasetId);

        /// <summary>
        /// Gets vehicle info for a given dataset and vehicle Id
        /// </summary>
        /// <param name="datasetId"></param>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        Task<Vehicle> GetVehicle(string datasetId, int vehicleId);

        /// <summary>
        /// Get dealer info for a given dataset and vehicle
        /// </summary>
        /// <param name="datasetId"></param>
        /// <param name="dealerId"></param>
        /// <returns></returns>
        Task<Dealer> GetDealer(string datasetId, int dealerId);

        /// <summary>
        /// Saves the answer
        /// </summary>
        /// <param name="datasetId"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        Task<string> Save(string datasetId, Answer answer);
    }
}
