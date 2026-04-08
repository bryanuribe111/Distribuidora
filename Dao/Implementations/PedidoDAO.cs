using System.Data;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Models;
using DistribuidoraAseo.DAO.Interfaces;

namespace DistribuidoraAseo.DAO.Implementations
{
    public class PedidoDAO : IPedidoDAO
    {
        private readonly DatabaseConnection _db;
        public PedidoDAO(DatabaseConnection db) => _db = db;

        public IEnumerable<Pedido> GetAll()
        {
            var lista = new List<Pedido>();
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_pedido, fecha, estado, id_cliente, id_vendedor FROM pedido";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(MapPedido(reader));
            return lista;
        }

        public Pedido? GetById(int id)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_pedido, fecha, estado, id_cliente, id_vendedor FROM pedido WHERE id_pedido = @id";
            AddParam(cmd, "@id", id);
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapPedido(reader) : null;
        }

        public IEnumerable<Pedido> GetByClienteId(int clienteId)
        {
            var lista = new List<Pedido>();
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_pedido, fecha, estado, id_cliente, id_vendedor FROM pedido WHERE id_cliente = @id";
            AddParam(cmd, "@id", clienteId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(MapPedido(reader));
            return lista;
        }

        public IEnumerable<Pedido> GetByVendedorId(int vendedorId)
        {
            var lista = new List<Pedido>();
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT id_pedido, fecha, estado, id_cliente, id_vendedor FROM pedido WHERE id_vendedor = @id";
            AddParam(cmd, "@id", vendedorId);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(MapPedido(reader));
            return lista;
        }

        public void Insert(Pedido p)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO pedido (id_pedido, fecha, estado, id_cliente, id_vendedor)
                                VALUES (@id, @fecha, @estado, @idCliente, @idVendedor)";
            AddParam(cmd, "@id",         p.IdPedido);
            AddParam(cmd, "@fecha",      p.Fecha);
            AddParam(cmd, "@estado",     p.Estado);
            AddParam(cmd, "@idCliente",  p.IdCliente);
            AddParam(cmd, "@idVendedor", p.IdVendedor);
            cmd.ExecuteNonQuery();
        }

        public void Update(Pedido p)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE pedido
                                SET fecha=@fecha, estado=@estado,
                                    id_cliente=@idCliente, id_vendedor=@idVendedor
                                WHERE id_pedido=@id";
            AddParam(cmd, "@fecha",      p.Fecha);
            AddParam(cmd, "@estado",     p.Estado);
            AddParam(cmd, "@idCliente",  p.IdCliente);
            AddParam(cmd, "@idVendedor", p.IdVendedor);
            AddParam(cmd, "@id",         p.IdPedido);
            cmd.ExecuteNonQuery();
        }

        public void UpdateEstado(int id, string estado)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE pedido SET estado = @estado WHERE id_pedido = @id";
            AddParam(cmd, "@estado", estado);
            AddParam(cmd, "@id",     id);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = _db.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM pedido WHERE id_pedido = @id";
            AddParam(cmd, "@id", id);
            cmd.ExecuteNonQuery();
        }

        private static Pedido MapPedido(IDataReader r) => new()
        {
            IdPedido   = r.GetInt32(0),
            Fecha      = r.GetDateTime(1),
            Estado     = r.GetString(2),
            IdCliente  = r.GetInt32(3),
            IdVendedor = r.GetInt32(4)
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