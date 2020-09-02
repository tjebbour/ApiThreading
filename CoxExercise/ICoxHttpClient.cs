using System.Threading.Tasks;

namespace CoxExercise
{
    public interface ICoxHttpClient
    {
        Task<string> Get(string endpoint);
        Task<string> Post(string apiEndPoint, string body);

    }
}
