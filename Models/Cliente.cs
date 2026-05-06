using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DistribuidoraAseo.Models
{
    [Table("cliente")]
    public class Cliente
    {
        [Key]
        [Column("id_cliente")]
        public int IdCliente { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("direccion")]
        public string? Direccion { get; set; }

        [Column("tipo_cliente")]
        public string? TipoCliente { get; set; }
    }
}