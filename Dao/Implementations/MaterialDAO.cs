using System.Data;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Models;
using DistribuidoraAseo.DAO.Interfaces;

namespace DistribuidoraAseo.DAO.Implementations
{
    public class MaterialDAO : IMaterialDAO
    {
        private readonly DatabaseConnection _db;
        public MaterialDAO(DatabaseConnection db) => _db = db;

        public IEnumerable<Material> GetAll()
        {
            var lista = new List<Material>();
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_material, nombre, stock, costo FROM material";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(MapMaterial(reader));
            return lista;
        }

        public Material? GetById(int id)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_material, nombre, stock, costo FROM material WHERE id_material = @id";
            AddParam(cmd, "@id", id);
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapMaterial(reader) : null;
        }

        public void Insert(Material m)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO material (id_material, nombre, stock, costo) VALUES (@id, @nombre, @stock, @costo)";
            AddParam(cmd, "@id",     m.IdMaterial);
            AddParam(cmd, "@nombre", m.Nombre);
            AddParam(cmd, "@stock",  m.Stock);
            AddParam(cmd, "@costo",  m.Costo);
            cmd.ExecuteNonQuery();
        }

        public void Update(Material m)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE material SET nombre=@nombre, stock=@stock, costo=@costo WHERE id_material=@id";
            AddParam(cmd, "@nombre", m.Nombre);
            AddParam(cmd, "@stock",  m.Stock);
            AddParam(cmd, "@costo",  m.Costo);
            AddParam(cmd, "@id",     m.IdMaterial);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM material WHERE id_material = @id";
            AddParam(cmd, "@id", id);
            cmd.ExecuteNonQuery();
        }

        public void UpdateStock(int id, int cantidad)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE material SET stock = stock - @cantidad WHERE id_material = @id";
            AddParam(cmd, "@cantidad", cantidad);
            AddParam(cmd, "@id",       id);
            cmd.ExecuteNonQuery();
        }

        private static Material MapMaterial(IDataReader r) => new()
        {
            IdMaterial = r.GetInt32(0),
            Nombre     = r.GetString(1),
            Stock      = r.GetInt32(2),
            Costo      = r.GetDecimal(3)
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