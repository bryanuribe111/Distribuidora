using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.Services.Interfaces
{
    public interface IPedidoService
    {
        void ValidarStock(int idProducto, int cantidad);

        decimal CalcularTotal(List<DetallePedido> detalles);

        void CrearPedido(Pedido pedido, List<DetallePedido> detalles);

        void ActualizarStock(int idProducto, int cantidad);
    }
}   
    