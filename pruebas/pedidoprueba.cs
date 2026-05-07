using Xunit;
using DistribuidoraAseo.DAO.Implementations;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Models;
using System;

namespace DistribuidoraAseo.Tests
{
    public class pedidoprueba
    {
        private readonly PedidoDAO _dao;

        public pedidoprueba()
        {
            var db = new DatabaseConnection();
            _dao = new PedidoDAO(db);
        }

        [Fact]
        public void Insert_DeberiaInsertarPedido()
        {
            var pedido = new Pedido
            {
                IdPedido = 999,
                Fecha = DateTime.Now,
                Estado = "Pendiente",
                IdCliente = 1,
                IdVendedor = 1
            };

            _dao.Insert(pedido);

            var resultado = _dao.GetById(999);

            Assert.NotNull(resultado);
            Assert.Equal("Pendiente", resultado.Estado);
        }

        [Fact]
        public void GetById_DeberiaRetornarPedido()
        {
            var pedido = _dao.GetById(999);

            Assert.NotNull(pedido);
            Assert.Equal(999, pedido.IdPedido);
        }

        [Fact]
        public void Update_DeberiaActualizarPedido()
        {
            var pedido = _dao.GetById(999);
            pedido.Estado = "En proceso";

            _dao.Update(pedido);

            var actualizado = _dao.GetById(999);

            Assert.Equal("En proceso", actualizado.Estado);
        }

        [Fact]
        public void UpdateEstado_DeberiaCambiarEstado()
        {
            _dao.UpdateEstado(999, "Entregado");

            var pedido = _dao.GetById(999);

            Assert.Equal("Entregado", pedido.Estado);
        }

        [Fact]
        public void GetByClienteId_DeberiaRetornarLista()
        {
            var pedidos = _dao.GetByClienteId(1);

            Assert.NotNull(pedidos);
        }

        [Fact]
        public void GetByVendedorId_DeberiaRetornarLista()
        {
            var pedidos = _dao.GetByVendedorId(1);

            Assert.NotNull(pedidos);
        }

        [Fact]
        public void Delete_DeberiaEliminarPedido()
        {
            _dao.Delete(999);

            var eliminado = _dao.GetById(999);

            Assert.Null(eliminado);
        }

        [Fact]
        public void GetAll_DeberiaRetornarPedidos()
        {
            var pedidos = _dao.GetAll();

            Assert.NotEmpty(pedidos);
        }
    }
}