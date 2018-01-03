using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TuPedido.Managers;
using TuPedido.Models;
using TuPedido.Services;
using TuPedido.Views;
using Xamarin.Forms;

namespace TuPedido.ViewModels
{
    public class OrdersListViewModel : ViewModelBase
    {
        #region Attributes & Properties

        private readonly IOrderManager orderManager;
        private IEnumerable<OrderGrouping> orders;

        public IEnumerable<OrderGrouping> Orders { set { SetPropertyValue(ref orders, value); } get { return orders; } }
        public ICommand ViewDetailCommand { get; set; }
        public ICommand NotifyCommand { get; set; }
        public ICommand LoadCommand { get; set; }

        #endregion

        public OrdersListViewModel(INavigationService navigationService, IOrderManager orderManager) : base(navigationService)
        {
            this.orderManager = orderManager;
            ViewDetailCommand = new Command<Order>(async (Order item) => await ViewDetail(item));
            NotifyCommand = new Command<Order>(async (Order item) => await Notify(item));
            LoadCommand = new Command(async () => await LoadOrders());
        }

        public override Task InitializeAsync(object navigationData)
        {
            return LoadOrders();
        }

        private Task LoadOrders()
        {
            return TryExecute(async () =>
            {
                var orders = await orderManager.GetOrders();
                Orders = new List<OrderGrouping>
                {
                    new OrderGrouping("Pendientes", orders.Where(o => !o.Received), "No existen pedidos pendientes"),
                    new OrderGrouping("Recibidos", orders.Where(o => o.Received), "No existen pedidos recibidos"),
                };
            });
        }

        private Task ViewDetail(Order item)
        {
            return TryExecute(async () =>
            {
                await navigationService.NavigateToAsync(new OrderDetailView());
            });
        }

        private Task Notify(Order item)
        {
            return TryExecute(async () =>
            {
                await navigationService.NavigateToAsync(new OrderDetailView());
            });
        }
    }
}
