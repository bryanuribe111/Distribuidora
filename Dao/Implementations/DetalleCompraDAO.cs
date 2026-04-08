using System.Data;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Models;
using DistribuidoraAseo.DAO.Interfaces;

namespace DistribuidoraAseo.DAO.Implementations
{
    public class DetalleCompraDAO : IDetalleCompraDAO
    {
        private readonly DatabaseConnection _db;
        public DetalleCompraDAO(DatabaseConnection db) => _db = db;

        public IEnumerable<DetalleCompra> GetByCompraId(int compraId)
        {
            var lista = new List<DetalleCompra>();
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT id_detalle_compra, id_compra, id_producto, cantidad, costo_unitario
                                FROM detalle_compra WHERE id_compra = @id";
            AddParam(cmd, "@id", compraId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(new DetalleCompra
                {
                    IdDetalleCompra = reader.GetInt32(0),
                    IdCompra        = reader.GetInt32(1),
                    IdProducto      = reader.GetInt32(2),
                    Cantidad        = reader.GetInt32(3),
                    CostoUnitario   = reader.GetDecimal(4)
                });
            return lista;
        }

        public void Insert(DetalleCompra d)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO detalle_compra (id_detalle_compra, id_compra, id_producto, cantidad, costo_unitario)
                                VALUES (@id, @idCompra, @idProducto, @cantidad, @costoUnitario)";
            AddParam(cmd, "@id",            d.IdDetalleCompra);
            AddParam(cmd, "@idCompra",      d.IdCompra);
            AddParam(cmd, "@idProducto",    d.IdProducto);
            AddParam(cmd, "@cantidad",      d.Cantidad);
            AddParam(cmd, "@costoUnitario", d.CostoUnitario);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM detalle_compra WHERE id_detalle_compra = @id";
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