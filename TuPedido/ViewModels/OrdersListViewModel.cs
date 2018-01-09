using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly INotificationManager notificationManager;
        private ObservableCollection<OrderGrouping> orders;

        public ObservableCollection<OrderGrouping> Orders { set { SetPropertyValue(ref orders, value); } get { return orders; } }
        public ICommand ViewDetailCommand { get; set; }
        public ICommand NotifyCommand { get; set; }
        public ICommand LoadCommand { get; set; }

        #endregion

        public OrdersListViewModel(INavigationService navigationService, IOrderManager orderManager, INotificationManager notificationManager) : base(navigationService)
        {
            this.orderManager = orderManager;
            this.notificationManager = notificationManager;

            ViewDetailCommand = new Command<Order>(async (Order item) => await ViewDetailAsync(item));
            NotifyCommand = new Command<Order>(async (Order item) => await NotifyAsync(item));
            LoadCommand = new Command(async () => await LoadOrdersAsync());
        }

        public override Task InitializeAsync(object navigationData)
        {
            return LoadOrdersAsync();
        }

        private Task LoadOrdersAsync()
        {
            return Task.Run(async() => 
            {
                IEnumerable<Order> orders = new List<Order>();
                await TryExecute(async () =>
                {
                    orders = await orderManager.GetOrdersAsync();
                });

                Orders = new ObservableCollection<OrderGrouping>(new List<OrderGrouping>
                {
                    new OrderGrouping("Pendientes", orders.Where(o => !o.Received).OrderBy(o => o.Date), "No existen pedidos pendientes"),
                    new OrderGrouping("Recibidos", orders.Where(o => o.Received).OrderByDescending(o => o.ReceivedDate), "No existen pedidos recibidos"),
                });
            });
        }

        private Task ViewDetailAsync(Order item)
        {
            return TryExecute(async () =>
            {
                await navigationService.NavigateToAsync(new OrderDetailView());
            });
        }

        private Task NotifyAsync(Order item)
        {
            return TryExecute(() => notificationManager.Notify(item));
        }
    }
}
