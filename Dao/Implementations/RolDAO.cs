using System.Data;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Models;
using DistribuidoraAseo.DAO.Interfaces;

namespace DistribuidoraAseo.DAO.Implementations
{
    public class RolDAO : IRolDAO
    {
        private readonly DatabaseConnection _db;
        public RolDAO(DatabaseConnection db) => _db = db;

        public IEnumerable<Rol> GetAll()
        {
            var lista = new List<Rol>();
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_rol, nombre FROM rol";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(new Rol { IdRol = reader.GetInt32(0), Nombre = reader.GetString(1) });
            return lista;
        }

        public Rol? GetById(int id)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_rol, nombre FROM rol WHERE id_rol = @id";
            AddParam(cmd, "@id", id);
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? new Rol { IdRol = reader.GetInt32(0), Nombre = reader.GetString(1) } : null;
        }

        public void Insert(Rol rol)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO rol (id_rol, nombre) VALUES (@id, @nombre)";
            AddParam(cmd, "@id",     rol.IdRol);
            AddParam(cmd, "@nombre", rol.Nombre);
            cmd.ExecuteNonQuery();
        }

        public void Update(Rol rol)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE rol SET nombre = @nombre WHERE id_rol = @id";
            AddParam(cmd, "@nombre", rol.Nombre);
            AddParam(cmd, "@id",     rol.IdRol);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM rol WHERE id_rol = @id";
            AddParam(cmd, "@id", id);
            cmd.ExecuteNonQuery();
        }

        private static void AddParam(IDbCommand cmd, string name, object value)
        {
            var p = cmd.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            cmd.Parameters.Add(p);
        }
    }
}