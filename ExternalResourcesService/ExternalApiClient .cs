using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CityDetails.ExternalResources
{
    public class ExternalApiClient
    {
        private const string apiKey = "6c3f87b76a90dd6716cb8c3721987326";
        public async Task<JToken> CreateRestClient(string requestUrl)
        {
            var client = new RestClient($"{requestUrl}");
            var request = new RestRequest();
            request.Method = Method.Get;

            RestResponse response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);
                return content;
            }
            return response.Content;
        }

        public async Task<string> GetCountryDetails(string countryName)
        {
            // logic to get prices from external api
            var response = await CreateRestClient($"https://restcountries.com/v3.1/name/{countryName}/");
            return response.ToString();
        }

        public async Task<string> GetWeatherDetails(string cityName)
        {
            // logic to get prices from external api
            var response = await CreateRestClient($"https://api.openweathermap.org/data/2.5/weather?q={cityName}&APPID={apiKey}");
            return response.ToString();
        }
    }
}
