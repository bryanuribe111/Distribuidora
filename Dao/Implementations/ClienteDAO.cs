using System.Data;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Models;
using DistribuidoraAseo.DAO.Interfaces;

namespace DistribuidoraAseo.DAO.Implementations
{
    public class ClienteDAO : IClienteDAO
    {
        private readonly DatabaseConnection _db;
        public ClienteDAO(DatabaseConnection db) => _db = db;

        public IEnumerable<Cliente> GetAll()
        {
            var lista = new List<Cliente>();
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_cliente, nombre, direccion, tipo_cliente FROM cliente";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(MapCliente(reader));
            return lista;
        }

        public Cliente? GetById(int id)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_cliente, nombre, direccion, tipo_cliente FROM cliente WHERE id_cliente = @id";
            AddParam(cmd, "@id", id);
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapCliente(reader) : null;
        }

        public void Insert(Cliente c)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO cliente (id_cliente, nombre, direccion, tipo_cliente)
                                VALUES (@id, @nombre, @direccion, @tipoCliente)";
            AddParam(cmd, "@id",          c.IdCliente);
            AddParam(cmd, "@nombre",      c.Nombre);
            AddParam(cmd, "@direccion",   c.Direccion ?? (object)DBNull.Value);
            AddParam(cmd, "@tipoCliente", c.TipoCliente ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Update(Cliente c)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE cliente
                                SET nombre=@nombre, direccion=@direccion, tipo_cliente=@tipoCliente
                                WHERE id_cliente=@id";
            AddParam(cmd, "@nombre",      c.Nombre);
            AddParam(cmd, "@direccion",   c.Direccion ?? (object)DBNull.Value);
            AddParam(cmd, "@tipoCliente", c.TipoCliente ?? (object)DBNull.Value);
            AddParam(cmd, "@id",          c.IdCliente);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM cliente WHERE id_cliente = @id";
            AddParam(cmd, "@id", id);
            cmd.ExecuteNonQuery();
        }

        private static Cliente MapCliente(IDataReader r) => new()
        {
            IdCliente   = r.GetInt32(0),
            Nombre      = r.GetString(1),
            Direccion   = r.IsDBNull(2) ? null : r.GetString(2),
            TipoCliente = r.IsDBNull(3) ? null : r.GetString(3)
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