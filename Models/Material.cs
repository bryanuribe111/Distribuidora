namespace DistribuidoraAseo.Models
{
    public class Material
    {
        public int IdMaterial { get; set; }
        public string Nombre { get; set; } = null!;
        public int Stock { get; set; }
        public decimal Costo { get; set; }
    }
}