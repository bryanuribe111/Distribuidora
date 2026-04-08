using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.DAO.Interfaces
{
    public interface IDetalleCompraDAO
    {
        IEnumerable<DetalleCompra> GetByCompraId(int compraId);
        void Insert(DetalleCompra detalle);
        void Delete(int id);
    }
}