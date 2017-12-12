namespace TuPedido
{
    public interface IConfiguration
    {
        IAppCenter AppCenter { get; }
    }

    public interface IAppCenter
    {
        string iOSSecretKey { get; }
        string AndroidSecretKey { get; }
        string UwpSecretKey { get; }
    }
}
