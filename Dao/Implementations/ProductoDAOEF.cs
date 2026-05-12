using DistribuidoraAseo.DAO.Interfaces;
using DistribuidoraAseo.Models;
using DistribuidoraAseo.Data;
using Microsoft.EntityFrameworkCore;

namespace DistribuidoraAseo.DAO.Implementations
{
    public class ProductoDAOEF : IProductoDAO
    {
        private readonly AppDbContext _context;


        public ProductoDAOEF(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Producto> GetAll()
        {
            return _context.Productos.ToList();
        }

        public Producto? GetById(int id)
{
    return _context.Productos
        .AsNoTracking()
        .FirstOrDefault(p => p.IdProducto == id);
}

        public void Insert(Producto producto)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();
        }

 public void Update(Producto producto)
{
    var entity = _context.Productos
        .FirstOrDefault(p => p.IdProducto == producto.IdProducto);

    if (entity == null)
        throw new Exception("Producto no encontrado");

    entity.Nombre = producto.Nombre;
    entity.Tipo = producto.Tipo;
    entity.PrecioBase = producto.PrecioBase;
    entity.Stock = producto.Stock;
    entity.IdFabricador = producto.IdFabricador;

    _context.SaveChanges();
}

        public void Delete(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                _context.SaveChanges();
            }
        }


        public void UpdateStock(int id, int cantidad)
{
    var producto = _context.Productos.Find(id);

    if (producto != null)
    {
        var nuevoStock = (producto.Stock ?? 0) - cantidad;
        producto.Stock = nuevoStock < 0 ? 0 : nuevoStock;

        _context.SaveChanges();
    }
}
    }
}