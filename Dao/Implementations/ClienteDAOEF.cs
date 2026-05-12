using DistribuidoraAseo.DAO.Interfaces;
using DistribuidoraAseo.Models;
using DistribuidoraAseo.Data;
using Microsoft.EntityFrameworkCore;

namespace DistribuidoraAseo.DAO.Implementations
{
    public class ClienteDAOEF : IClienteDAO
    {
        private readonly AppDbContext _context;

        public ClienteDAOEF(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Cliente> GetAll()
        {
            return _context.Clientes.ToList();
        }

        public Cliente? GetById(int id)
        {
            return _context.Clientes.Find(id);
        }

        public void Insert(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }

        public void Update(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
            }
        }
    }
}