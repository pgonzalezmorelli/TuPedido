using System.Threading.Tasks;
using Xamarin.Forms;

namespace TuPedido.Services
{
    public interface INavigationService
    {
        Task InitializeAsync();

        Task NavigateToAsync(Page page);

        Task NavigateToAsync<T>(Page page, T navigationData);

        Task BackAsync();
    }
}
