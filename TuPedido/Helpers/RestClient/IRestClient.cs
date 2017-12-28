using System.Threading.Tasks;

namespace TuPedido.Helpers
{
    public interface IRestClient
    {
        Task<TResult> GetAsync<TResult>(string uri);
    }
}
