using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DistribuidoraAseo.Models
{
    [Table("producto")]
    public class Producto
    {
        [Key]
        [Column("id_producto")]
        public int IdProducto { get; set; }

        [Column("nombre")]
        public required string Nombre { get; set; }

        [Column("tipo")]
        public required string Tipo { get; set; }

        [Column("precio_base")]
        public decimal PrecioBase { get; set; }

        [Column("stock")]
        public int? Stock { get; set; }

        [Column("id_fabricador")]
        public int? IdFabricador { get; set; }
    }
}