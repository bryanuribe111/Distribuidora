using System.Data;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Models;
using DistribuidoraAseo.DAO.Interfaces;

namespace DistribuidoraAseo.DAO.Implementations
{
    public class PreciosEspecialesDAO : IPreciosEspecialesDAO
    {
        private readonly DatabaseConnection _db;
        public PreciosEspecialesDAO(DatabaseConnection db) => _db = db;

        public IEnumerable<PrecioEspecial> GetAll()
        {
            var lista = new List<PrecioEspecial>();
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_precio, id_cliente, id_producto, precio_especial FROM precios_especiales";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(MapPrecio(reader));
            return lista;
        }

        public IEnumerable<PrecioEspecial> GetByClienteId(int clienteId)
        {
            var lista = new List<PrecioEspecial>();
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_precio, id_cliente, id_producto, precio_especial FROM precios_especiales WHERE id_cliente = @id";
            AddParam(cmd, "@id", clienteId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(MapPrecio(reader));
            return lista;
        }

        public PrecioEspecial? GetByClienteYProducto(int clienteId, int productoId)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT id_precio, id_cliente, id_producto, precio_especial
                                FROM precios_especiales
                                WHERE id_cliente = @idCliente AND id_producto = @idProducto";
            AddParam(cmd, "@idCliente",  clienteId);
            AddParam(cmd, "@idProducto", productoId);
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapPrecio(reader) : null;
        }

        public void Insert(PrecioEspecial pe)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO precios_especiales (id_precio, id_cliente, id_producto, precio_especial)
                                VALUES (@id, @idCliente, @idProducto, @precioEspecial)";
            AddParam(cmd, "@id",            pe.IdPrecio);
            AddParam(cmd, "@idCliente",     pe.IdCliente);
            AddParam(cmd, "@idProducto",    pe.IdProducto);
            AddParam(cmd, "@precioEspecial",pe.Precio);
            cmd.ExecuteNonQuery();
        }

        public void Update(PrecioEspecial pe)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE precios_especiales
                                SET precio_especial = @precioEspecial
                                WHERE id_precio = @id";
            AddParam(cmd, "@precioEspecial", pe.Precio);
            AddParam(cmd, "@id",             pe.IdPrecio);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM precios_especiales WHERE id_precio = @id";
            AddParam(cmd, "@id", id);
            cmd.ExecuteNonQuery();
        }

        private static PrecioEspecial MapPrecio(IDataReader r) => new()
        {
            IdPrecio   = r.GetInt32(0),
            IdCliente  = r.GetInt32(1),
            IdProducto = r.GetInt32(2),
            Precio     = r.GetDecimal(3)
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