using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        public Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest data, Dictionary<string, string> headers = null)
        {
            return ErrorHelper.TryExecuteServiceAsync(async () =>
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                headers?.ForEach(header => content.Headers.Add(header.Key, header.Value));
                
                var response = await client.PostAsync(uri, content);
                return await HandleResponse<TResponse>(response);
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
