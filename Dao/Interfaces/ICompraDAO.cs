using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.DAO.Interfaces
{
    public interface ICompraDAO
    {
        IEnumerable<Compra> GetAll();
        Compra? GetById(int id);
        void Insert(Compra compra);
        void Update(Compra compra);
        void Delete(int id);
    }
}