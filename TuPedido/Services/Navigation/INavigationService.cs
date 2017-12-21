using System.Threading.Tasks;
using Xamarin.Forms;

namespace TuPedido.Services
{
    public interface INavigationService
    {
        Task InitializeAsync();

        Task NavigateToAsync(Page page);

        Task BackAsync();

        Task<bool> CanGoBackAsync();
    }
}
