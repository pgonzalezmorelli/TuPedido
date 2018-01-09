namespace TuPedido.Helpers
{
    class Configuration : IConfiguration
    {
        public AppCenterConfig AppCenter => new AppCenterConfig
        {
            iOSSecretKey = "44e7545a-10ba-4ae2-a5c6-23e58acda9e5",
            AndroidSecretKey = "21382bd2-0f1d-471c-a089-eaaa95771abc",
            UwpSecretKey = null,
            ApiToken = "ddb3c615cef58bbd796daa6853d21a9affb108e2",
            iOSAppName = "TuPedido",
            AndroidAppName = "TuPedido-1",
            iOSAppOwner = "pablo.gonzalez-1",
            AndriodAppOwner = "pablo.gonzalez-1",
            iOSSenderId = "",
            AndroidSenderId = "1508873952"
        };
        public EndpointsConfig Endpoints => new EndpointsConfig
        {
            GetUsersEndpoint = @"http://172.20.3.161/users",
            PostNotificationEndpoint = @"https://api.appcenter.ms/v0.1/apps/{0}/{1}/push/notifications"
        };
        public DatabaseConfig Database => new DatabaseConfig
        {
            ConnectionString = "tupedido_db.db3",
        };
        public DropboxConfig Dropbox => new DropboxConfig
        {
            AccessToken = "bPPNAG-RyBAAAAAAAAAAIv1Bi2FvZurEGSXcdYoIVFMzHxD4BVYwi7g2IvSxl6Lp",
            FileName = "/Pedidos.xlsx"
        };
    }
}