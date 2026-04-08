namespace DistribuidoraAseo.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = null!;
        public int IdCliente { get; set; }
        public int IdVendedor { get; set; }
    }
}