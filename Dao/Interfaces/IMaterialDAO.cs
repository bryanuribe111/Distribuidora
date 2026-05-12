using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.DAO.Interfaces
{
    public interface IMaterialDAO
    {
        IEnumerable<Material> GetAll();
        Material? GetById(int id);
        void Insert(Material material);
        void Update(Material material);
        void Delete(int id);
        void UpdateStock(int id, int cantidad);
    }
}