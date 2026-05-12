using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.DAO.Interfaces
{
    public interface IDetallePedidoDAO
    {
        IEnumerable<DetallePedido> GetByPedidoId(int pedidoId);
        DetallePedido? GetById(int id);
        void Insert(DetallePedido detalle);
        void Delete(int id);
    }
}