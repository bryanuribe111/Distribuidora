namespace DistribuidoraAseo.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Direccion { get; set; }
        public string? TipoCliente { get; set; }
    }
}