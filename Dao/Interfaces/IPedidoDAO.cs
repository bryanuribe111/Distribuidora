using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.DAO.Interfaces
{
    public interface IPedidoDAO
    {
        IEnumerable<Pedido> GetAll();
        Pedido? GetById(int id);
        IEnumerable<Pedido> GetByClienteId(int clienteId);
        IEnumerable<Pedido> GetByVendedorId(int vendedorId);
        void Insert(Pedido pedido);
        void Update(Pedido pedido);
        void UpdateEstado(int id, string estado);
        void Delete(int id);
    }
}