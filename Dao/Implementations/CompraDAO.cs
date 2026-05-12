using System.Data;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Models;
using DistribuidoraAseo.DAO.Interfaces;

namespace DistribuidoraAseo.DAO.Implementations
{
    public class CompraDAO : ICompraDAO
    {
        private readonly DatabaseConnection _db;
        public CompraDAO(DatabaseConnection db) => _db = db;

        public IEnumerable<Compra> GetAll()
        {
            var lista = new List<Compra>();
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_compra, proveedor, fecha, costo_total FROM compra";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(MapCompra(reader));
            return lista;
        }

        public Compra? GetById(int id)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_compra, proveedor, fecha, costo_total FROM compra WHERE id_compra = @id";
            AddParam(cmd, "@id", id);
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapCompra(reader) : null;
        }

        public void Insert(Compra c)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO compra (id_compra, proveedor, fecha, costo_total)
                                VALUES (@id, @proveedor, @fecha, @costoTotal)";
            AddParam(cmd, "@id",         c.IdCompra);
            AddParam(cmd, "@proveedor",  c.Proveedor);
            AddParam(cmd, "@fecha",      c.Fecha);
            AddParam(cmd, "@costoTotal", c.CostoTotal ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Update(Compra c)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE compra
                                SET proveedor=@proveedor, fecha=@fecha, costo_total=@costoTotal
                                WHERE id_compra=@id";
            AddParam(cmd, "@proveedor",  c.Proveedor);
            AddParam(cmd, "@fecha",      c.Fecha);
            AddParam(cmd, "@costoTotal", c.CostoTotal ?? (object)DBNull.Value);
            AddParam(cmd, "@id",         c.IdCompra);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM compra WHERE id_compra = @id";
            AddParam(cmd, "@id", id);
            cmd.ExecuteNonQuery();
        }

        private static Compra MapCompra(IDataReader r) => new()
        {
            IdCompra   = r.GetInt32(0),
            Proveedor  = r.GetString(1),
            Fecha      = r.GetDateTime(2),
            CostoTotal = r.IsDBNull(3) ? null : r.GetDecimal(3)
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