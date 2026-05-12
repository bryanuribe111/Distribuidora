using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.DAO.Interfaces
{
    public interface IClienteDAO
    {
        IEnumerable<Cliente> GetAll();
        Cliente? GetById(int id);
        void Insert(Cliente cliente);
        void Update(Cliente cliente);
        void Delete(int id);
    }
}