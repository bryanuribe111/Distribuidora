using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.DAO.Interfaces
{
    public interface IProductoDAO
    {
        IEnumerable<Producto> GetAll();
        Producto? GetById(int id);
        void Insert(Producto producto);
        void Update(Producto producto);
        void Delete(int id);
        void UpdateStock(int id, int cantidad);
    }
}