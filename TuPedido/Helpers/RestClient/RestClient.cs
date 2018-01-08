using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Threading.Tasks;
using TuPedido.Exceptions;

namespace TuPedido.Helpers
{
    public class RestClient : IRestClient
    {
        private readonly HttpClient client;
        private readonly JsonSerializerSettings serializerSettings;

        public RestClient(HttpClient httpClient)
        {
            this.client = httpClient;
            serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public Task<T> GetAsync<T>(string uri)
        {
            return ErrorHelper.TryExecuteServiceAsync(async () =>
            {
                var response = await client.GetAsync(uri);
                return await HandleResponse<T>(response);
            });
        }

        private Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            return ErrorHelper.TryExecuteServiceAsync(async() => 
            {
                var content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(content, serializerSettings);
                }
                throw new ServiceErrorException((int)response.StatusCode, response.ReasonPhrase);
            });
        }
    }
}
