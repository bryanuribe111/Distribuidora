using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.DAO.Interfaces
{
    public interface IRolDAO
    {
        IEnumerable<Rol> GetAll();
        Rol? GetById(int id);
        void Insert(Rol rol);
        void Update(Rol rol);
        void Delete(int id);
    }
}