using System.Data;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Models;
using DistribuidoraAseo.DAO.Interfaces;

namespace DistribuidoraAseo.DAO.Implementations
{
    public class UsuarioDAO : IUsuarioDAO
    {
        private readonly DatabaseConnection _db;
        public UsuarioDAO(DatabaseConnection db) => _db = db;

        public IEnumerable<Usuario> GetAll()
        {
            var lista = new List<Usuario>();
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_usuario, nombre, correo, contrasena, id_rol FROM usuario";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(MapUsuario(reader));
            return lista;
        }

        public Usuario? GetById(int id)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_usuario, nombre, correo, contrasena, id_rol FROM usuario WHERE id_usuario = @id";
            AddParam(cmd, "@id", id);
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapUsuario(reader) : null;
        }

        public Usuario? GetByCorreo(string correo)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_usuario, nombre, correo, contrasena, id_rol FROM usuario WHERE correo = @correo";
            AddParam(cmd, "@correo", correo);
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapUsuario(reader) : null;
        }

        public void Insert(Usuario u)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO usuario (id_usuario, nombre, correo, contrasena, id_rol)
                                VALUES (@id, @nombre, @correo, @contrasena, @idRol)";
            AddParam(cmd, "@id",         u.IdUsuario);
            AddParam(cmd, "@nombre",     u.Nombre);
            AddParam(cmd, "@correo",     u.Correo);
            AddParam(cmd, "@contrasena", u.Contrasena);
            AddParam(cmd, "@idRol",      u.IdRol);
            cmd.ExecuteNonQuery();
        }

        public void Update(Usuario u)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE usuario
                                SET nombre=@nombre, correo=@correo,
                                    contrasena=@contrasena, id_rol=@idRol
                                WHERE id_usuario=@id";
            AddParam(cmd, "@nombre",     u.Nombre);
            AddParam(cmd, "@correo",     u.Correo);
            AddParam(cmd, "@contrasena", u.Contrasena);
            AddParam(cmd, "@idRol",      u.IdRol);
            AddParam(cmd, "@id",         u.IdUsuario);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM usuario WHERE id_usuario = @id";
            AddParam(cmd, "@id", id);
            cmd.ExecuteNonQuery();
        }

        private static Usuario MapUsuario(IDataReader r) => new()
        {
            IdUsuario  = r.GetInt32(0),
            Nombre     = r.GetString(1),
            Correo     = r.GetString(2),
            Contrasena = r.GetString(3),
            IdRol      = r.GetInt32(4)
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