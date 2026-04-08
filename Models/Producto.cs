namespace DistribuidoraAseo.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public decimal PrecioBase { get; set; }
        public int Stock { get; set; }
        public int? IdFabricador { get; set; }
    }
}