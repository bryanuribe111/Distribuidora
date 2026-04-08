using System.Data;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Models;
using DistribuidoraAseo.DAO.Interfaces;

namespace DistribuidoraAseo.DAO.Implementations
{
    public class ProductoDAO : IProductoDAO
    {
        private readonly DatabaseConnection _db;
        public ProductoDAO(DatabaseConnection db) => _db = db;

        public IEnumerable<Producto> GetAll()
        {
            var lista = new List<Producto>();
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_producto, nombre, tipo, precio_base, stock, id_fabricador FROM producto";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(MapProducto(reader));
            return lista;
        }

        public Producto? GetById(int id)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_producto, nombre, tipo, precio_base, stock, id_fabricador FROM producto WHERE id_producto = @id";
            AddParam(cmd, "@id", id);
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapProducto(reader) : null;
        }

        public void Insert(Producto p)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO producto (id_producto, nombre, tipo, precio_base, stock, id_fabricador)
                                VALUES (@id, @nombre, @tipo, @precioBase, @stock, @idFabricador)";
            AddParam(cmd, "@id",           p.IdProducto);
            AddParam(cmd, "@nombre",       p.Nombre);
            AddParam(cmd, "@tipo",         p.Tipo);
            AddParam(cmd, "@precioBase",   p.PrecioBase);
            AddParam(cmd, "@stock",        p.Stock);
            AddParam(cmd, "@idFabricador", p.IdFabricador ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Update(Producto p)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE producto
                                SET nombre=@nombre, tipo=@tipo, precio_base=@precioBase,
                                    stock=@stock, id_fabricador=@idFabricador
                                WHERE id_producto=@id";
            AddParam(cmd, "@nombre",       p.Nombre);
            AddParam(cmd, "@tipo",         p.Tipo);
            AddParam(cmd, "@precioBase",   p.PrecioBase);
            AddParam(cmd, "@stock",        p.Stock);
            AddParam(cmd, "@idFabricador", p.IdFabricador ?? (object)DBNull.Value);
            AddParam(cmd, "@id",           p.IdProducto);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM producto WHERE id_producto = @id";
            AddParam(cmd, "@id", id);
            cmd.ExecuteNonQuery();
        }

        public void UpdateStock(int id, int cantidad)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE producto SET stock = stock - @cantidad WHERE id_producto = @id";
            AddParam(cmd, "@cantidad", cantidad);
            AddParam(cmd, "@id",       id);
            cmd.ExecuteNonQuery();
        }

        private static Producto MapProducto(IDataReader r) => new()
        {
            IdProducto   = r.GetInt32(0),
            Nombre       = r.GetString(1),
            Tipo         = r.GetString(2),
            PrecioBase   = r.GetDecimal(3),
            Stock        = r.GetInt32(4),
            IdFabricador = r.IsDBNull(5) ? null : r.GetInt32(5)
        };

        private static void AddParam(IDbCommand cmd, string name, object value)
        {
            var p = cmd.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            cmd.Parameters.Add(p);
        }
    }
}