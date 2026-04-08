using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.DAO.Interfaces
{
    public interface IComposicionProductoDAO
    {
        IEnumerable<ComposicionProducto> GetByProductoId(int productoId);
        void Insert(ComposicionProducto composicion);
        void Delete(int productoId, int materialId);
    }
}