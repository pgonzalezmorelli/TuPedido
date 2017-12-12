using Xamarin.Forms;

[assembly: Dependency(typeof(TuPedido.Configuration))]
[assembly: Dependency(typeof(TuPedido.AppCenterConfig))]
namespace TuPedido
{
    class Configuration : IConfiguration
    {
        public IAppCenter AppCenter => DependencyService.Get<IAppCenter>();
    }

    class AppCenterConfig : IAppCenter
    {
        public string iOSSecretKey => "44e7545a-10ba-4ae2-a5c6-23e58acda9e5";

        public string AndroidSecretKey => "21382bd2-0f1d-471c-a089-eaaa95771abc";

        public string UwpSecretKey => null;
    }
}