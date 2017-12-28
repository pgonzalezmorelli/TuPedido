namespace TuPedido.Models
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public bool IsExternal { get; set; }
    }
}
