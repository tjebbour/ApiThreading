using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CoxExercise
{
    public class CoxHttpClient: ICoxHttpClient
    {
        private string baseUrl = string.Empty;

        public CoxHttpClient(IConfiguration configuration)
        {
            baseUrl = configuration.GetSection("baseurl").Value;
        }
        public async Task<string> Get(string endPoint)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"{baseUrl}{endPoint}");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStringAsync();

                throw new ApplicationException($"Something bad happened! Got a {response.StatusCode} from the server.");
            }
        }
        public async Task<string> Post(string endPoint, string body)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(body, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{baseUrl}{endPoint}", content);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStringAsync();

                throw new ApplicationException($"Something bad happened! Got a {response.StatusCode} from the server.");
            }

        }
    }
}
