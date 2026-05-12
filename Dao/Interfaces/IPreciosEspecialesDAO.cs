using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.DAO.Interfaces
{
    public interface IPreciosEspecialesDAO
    {
        IEnumerable<PrecioEspecial> GetAll();
        IEnumerable<PrecioEspecial> GetByClienteId(int clienteId);
        PrecioEspecial? GetByClienteYProducto(int clienteId, int productoId);
        void Insert(PrecioEspecial precio);
        void Update(PrecioEspecial precio);
        void Delete(int id);
    }
}