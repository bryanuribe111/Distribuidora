using System.Data;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Models;
using DistribuidoraAseo.DAO.Interfaces;

namespace DistribuidoraAseo.DAO.Implementations
{
    public class ComposicionProductoDAO : IComposicionProductoDAO
    {
        private readonly DatabaseConnection _db;
        public ComposicionProductoDAO(DatabaseConnection db) => _db = db;

        public IEnumerable<ComposicionProducto> GetByProductoId(int productoId)
        {
            var lista = new List<ComposicionProducto>();
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_producto, id_material, cantidad FROM composicion_producto WHERE id_producto = @id";
            AddParam(cmd, "@id", productoId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(new ComposicionProducto
                {
                    IdProducto  = reader.GetInt32(0),
                    IdMaterial  = reader.GetInt32(1),
                    Cantidad    = reader.GetInt32(2)
                });
            return lista;
        }

        public void Insert(ComposicionProducto c)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO composicion_producto (id_producto, id_material, cantidad)
                                VALUES (@idProducto, @idMaterial, @cantidad)";
            AddParam(cmd, "@idProducto", c.IdProducto);
            AddParam(cmd, "@idMaterial", c.IdMaterial);
            AddParam(cmd, "@cantidad",   c.Cantidad);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int productoId, int materialId)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM composicion_producto WHERE id_producto=@idProducto AND id_material=@idMaterial";
            AddParam(cmd, "@idProducto", productoId);
            AddParam(cmd, "@idMaterial", materialId);
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