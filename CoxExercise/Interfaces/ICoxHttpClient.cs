using System.Threading.Tasks;

namespace CoxExercise
{
    public interface ICoxHttpClient
    {
        /// <summary>
        /// Performs a GET HTTP operation and returns the results
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        Task<string> Get(string endpoint);

        /// <summary>
        /// Performs a POST HTTP operation and returns the results
        /// </summary>
        /// <param name="apiEndPoint"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task<string> Post(string apiEndPoint, string body);

    }
}
