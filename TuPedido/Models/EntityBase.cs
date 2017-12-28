using SQLite;
using System;

namespace TuPedido.Models
{
    public class EntityBase
    {
        [PrimaryKey]
        public Guid Id { get; set; }
    }
}
