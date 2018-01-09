using Splat;
using System;
using System.Net.Http;
using TuPedido.Helpers;
using TuPedido.Managers;
using TuPedido.Models;
using TuPedido.Repositories;
using TuPedido.Services;
using TuPedido.ViewModels;

namespace TuPedido
{
    public class DependencyContainer
    {
        public static void RegisterDependencies()
        {
            Register( new Configuration(), typeof(IConfiguration) );
            Register( new NavigationService<Views.LoginView, Views.OrdersListView>(), typeof(INavigationService) );
            Register( new RestClient(new HttpClient()), typeof(IRestClient) );
            Register( new Database<User>(Resolve<IFileHelper>(), Resolve<IConfiguration>()), typeof(IDatabase<User>) );
            Register( new DropboxServiceClient(Resolve<IConfiguration>()), typeof(IDropboxServiceClient) );
            Register( new ExcelHelper(Resolve<IConfiguration>()), typeof(IExcelHelper) );
            
            Register( new UserRepository(Resolve<IDatabase<User>>()), typeof(IUserRepository) );
            Register( new UserService(Resolve<IRestClient>(), Resolve<IConfiguration>()), typeof(IUserService) );
            Register( new UserManager(Resolve<IUserService>(), Resolve<IUserRepository>()), typeof(IUserManager) );
            Register( new LoginViewModel(Resolve<IUserManager>(), Resolve<INavigationService>()), typeof(LoginViewModel) );

            Register( new OrderService(Resolve<IDropboxServiceClient>(), Resolve<IExcelHelper>(), Resolve<IConfiguration>()), typeof(IOrderService) );
            Register( new OrderManager(Resolve<IOrderService>()), typeof(IOrderManager) );
            Register( new NotificationService(Resolve<IRestClient>(), Resolve<IConfiguration>()), typeof(INotificationService) );
            Register( new NotificationManager(Resolve<INotificationService>()), typeof(INotificationManager));
            Register( new OrdersListViewModel(Resolve<INavigationService>(), Resolve<IOrderManager>(), Resolve<INotificationManager>()), typeof(OrdersListViewModel) );

            Register( new OrderDetailViewModel(), typeof(OrderDetailViewModel) );
        }

        public static void Register(object value, Type type)
        {
            Locator.CurrentMutable.RegisterConstant(value, type);
        }

        public static T Resolve<T>()
        {
            return Locator.CurrentMutable.GetService<T>();
        }

        internal static object Resolve(Type viewModelType)
        {
            return Locator.CurrentMutable.GetService(viewModelType);
        }
    }
}
