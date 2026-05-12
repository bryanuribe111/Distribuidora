namespace DistribuidoraAseo.Models
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public string Proveedor { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public decimal? CostoTotal { get; set; }
    }
}