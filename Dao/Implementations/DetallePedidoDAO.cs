using System.Data;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Models;
using DistribuidoraAseo.DAO.Interfaces;

namespace DistribuidoraAseo.DAO.Implementations
{
    public class DetallePedidoDAO : IDetallePedidoDAO
    {
        private readonly DatabaseConnection _db;
        public DetallePedidoDAO(DatabaseConnection db) => _db = db;

        public IEnumerable<DetallePedido> GetByPedidoId(int pedidoId)
        {
            var lista = new List<DetallePedido>();
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT id_detalle, id_pedido, id_producto, cantidad, precio_unitario
                                FROM detalle_pedido WHERE id_pedido = @id";
            AddParam(cmd, "@id", pedidoId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(MapDetalle(reader));
            return lista;
        }

        public DetallePedido? GetById(int id)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT id_detalle, id_pedido, id_producto, cantidad, precio_unitario
                                FROM detalle_pedido WHERE id_detalle = @id";
            AddParam(cmd, "@id", id);
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapDetalle(reader) : null;
        }

        public void Insert(DetallePedido d)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO detalle_pedido (id_detalle, id_pedido, id_producto, cantidad, precio_unitario)
                                VALUES (@id, @idPedido, @idProducto, @cantidad, @precioUnitario)";
            AddParam(cmd, "@id",             d.IdDetalle);
            AddParam(cmd, "@idPedido",       d.IdPedido);
            AddParam(cmd, "@idProducto",     d.IdProducto);
            AddParam(cmd, "@cantidad",       d.Cantidad);
            AddParam(cmd, "@precioUnitario", d.PrecioUnitario);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM detalle_pedido WHERE id_detalle = @id";
            AddParam(cmd, "@id", id);
            cmd.ExecuteNonQuery();
        }

        private static DetallePedido MapDetalle(IDataReader r) => new()
        {
            IdDetalle      = r.GetInt32(0),
            IdPedido       = r.GetInt32(1),
            IdProducto     = r.GetInt32(2),
            Cantidad       = r.GetInt32(3),
            PrecioUnitario = r.GetDecimal(4)
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