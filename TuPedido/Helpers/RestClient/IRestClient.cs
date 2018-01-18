using System.Collections.Generic;
using System.Threading.Tasks;

namespace TuPedido.Helpers
{
    public interface IRestClient
    {
        Task<TResult> GetAsync<TResult>(string uri);
        Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest data, Dictionary<string, string> headers = null);
    }
}
