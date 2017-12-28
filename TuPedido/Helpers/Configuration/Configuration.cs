namespace TuPedido.Helpers
{
    class Configuration : IConfiguration
    {
        public AppCenterConfig AppCenter => new AppCenterConfig
        {
            iOSSecretKey = "44e7545a-10ba-4ae2-a5c6-23e58acda9e5",
            AndroidSecretKey = "21382bd2-0f1d-471c-a089-eaaa95771abc",
            UwpSecretKey = null
        };
        public EndpointsConfig Endpoints => new EndpointsConfig
        {
            GetUsersEndpoint = @"http://172.20.3.161/users"
        };
        public DatabaseConfig Database => new DatabaseConfig
        {
            ConnectionString = "tupedido_db.db3",
        };
    }
}