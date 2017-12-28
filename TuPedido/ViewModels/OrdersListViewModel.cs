using System.Threading.Tasks;
using System.Windows.Input;
using TuPedido.Services;
using TuPedido.Views;
using Xamarin.Forms;

namespace TuPedido.ViewModels
{
    public class OrdersListViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        public ICommand ViewDetailCommand { get; set; }


        public OrdersListViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            ViewDetailCommand = new Command(async() => await ViewDetail());
        }

        private Task ViewDetail()
        {
            return navigationService.NavigateToAsync(new OrderDetailView());
        }
    }
}
