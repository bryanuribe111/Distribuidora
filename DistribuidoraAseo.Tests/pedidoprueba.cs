using Xunit;
using DistribuidoraAseo.DAO.Implementations;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.Tests
{
    public class PedidoDAOTests : IDisposable
    {
        private readonly PedidoDAO _dao;

        // ID único para pruebas
        private readonly int _testId = new Random().Next(10000, 99999);

        public PedidoDAOTests()
        {
            var db = new DatabaseConnection();
            _dao = new PedidoDAO(db);
        }

        // Limpieza automática
        public void Dispose()
        {
            try
            {
                _dao.Delete(_testId);
            }
            catch
            {
                // Ignorar si no existe
            }
        }

        [Fact]
        public void Insert_DeberiaInsertarPedido()
        {
            // Arrange
            var pedido = CrearPedido();

            // Act
            _dao.Insert(pedido);

            // Assert
            var resultado = _dao.GetById(_testId);

            Assert.NotNull(resultado);
            Assert.Equal("Pendiente", resultado.Estado);
            Assert.Equal(1, resultado.IdCliente);
            Assert.Equal(1, resultado.IdVendedor);
        }

        [Fact]
        public void GetById_DeberiaRetornarPedido()
        {
            // Arrange
            var pedido = CrearPedido();

            _dao.Insert(pedido);

            // Act
            var resultado = _dao.GetById(_testId);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(_testId, resultado.IdPedido);
        }

        [Fact]
        public void Update_DeberiaActualizarPedido()
        {
            // Arrange
            var pedido = CrearPedido();

            _dao.Insert(pedido);

            var pedidoGuardado = _dao.GetById(_testId);

            Assert.NotNull(pedidoGuardado);

            pedidoGuardado.Estado = "En proceso";

            // Act
            _dao.Update(pedidoGuardado);

            // Assert
            var actualizado = _dao.GetById(_testId);

            Assert.NotNull(actualizado);
            Assert.Equal("En proceso", actualizado.Estado);
        }

        [Fact]
        public void UpdateEstado_DeberiaCambiarEstado()
        {
            // Arrange
            var pedido = CrearPedido();

            _dao.Insert(pedido);

            // Act
            _dao.UpdateEstado(_testId, "Entregado");

            // Assert
            var actualizado = _dao.GetById(_testId);

            Assert.NotNull(actualizado);
            Assert.Equal("Entregado", actualizado.Estado);
        }

        [Fact]
        public void GetByClienteId_DeberiaRetornarLista()
        {
            // Arrange
            var pedido = CrearPedido();

            _dao.Insert(pedido);

            // Act
            var pedidos = _dao.GetByClienteId(1);

            // Assert
            Assert.NotNull(pedidos);
        }

        [Fact]
        public void GetByVendedorId_DeberiaRetornarLista()
        {
            // Arrange
            var pedido = CrearPedido();

            _dao.Insert(pedido);

            // Act
            var pedidos = _dao.GetByVendedorId(1);

            // Assert
            Assert.NotNull(pedidos);
        }

        [Fact]
        public void Delete_DeberiaEliminarPedido()
        {
            // Arrange
            var pedido = CrearPedido();

            _dao.Insert(pedido);

            // Act
            _dao.Delete(_testId);

            // Assert
            var eliminado = _dao.GetById(_testId);

            Assert.Null(eliminado);
        }

        [Fact]
        public void GetAll_DeberiaRetornarPedidos()
        {
            // Act
            var pedidos = _dao.GetAll();

            // Assert
            Assert.NotNull(pedidos);
        }

        // Método auxiliar reutilizable
        private Pedido CrearPedido()
        {
            return new Pedido
            {
                IdPedido = _testId,
                Fecha = DateTime.Now,
                Estado = "Pendiente",
                IdCliente = 1,
                IdVendedor = 1
            };
        }
    }
}