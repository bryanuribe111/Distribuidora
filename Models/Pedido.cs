using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DistribuidoraAseo.Models
{
    [Table("pedido")]
    public class Pedido
    {
        [Key]
        [Column("id_pedido")]
        public int IdPedido { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Column("estado")]
        public string Estado { get; set; }

        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Column("id_vendedor")]
        public int IdVendedor { get; set; }

        public List<DetallePedido> Detalles { get; set; } = new();
    }
}