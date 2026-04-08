namespace DistribuidoraAseo.Models
{
    public class PrecioEspecial
    {
        public int IdPrecio { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public decimal Precio { get; set; }
    }
}