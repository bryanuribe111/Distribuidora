using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.DAO.Interfaces
{
    public interface IUsuarioDAO
    {
        IEnumerable<Usuario> GetAll();
        Usuario? GetById(int id);
        Usuario? GetByCorreo(string correo);
        void Insert(Usuario usuario);
        void Update(Usuario usuario);
        void Delete(int id);
    }
}