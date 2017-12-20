namespace TuPedido.Helpers
{
    public interface IConfiguration
    {
        AppCenterConfig AppCenter { get; }
        EndpointsConfig Endpoints { get; }
        DatabaseConfig Database { get;  }
    }

    public class AppCenterConfig
    {
        public string iOSSecretKey { get; set; }
        public string AndroidSecretKey { get; set; }
        public string UwpSecretKey { get; set; }
    }

    public class EndpointsConfig
    {
        public string GetUsersEndpoint { get; set; }
    }

    public class DatabaseConfig
    {
        public string ConnectionString { get; set; }
    }
}
