using System;
using System.Collections.Generic;
using DistribuidoraAseo.Models;
using DistribuidoraAseo.DAO.Interfaces;
using DistribuidoraAseo.Services.Interfaces;

namespace DistribuidoraAseo.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IProductoDAO _productoDAO;
        private readonly IPedidoDAO _pedidoDAO;
        private readonly IDetallePedidoDAO _detalleDAO;

        public PedidoService(IProductoDAO productoDAO, IPedidoDAO pedidoDAO, IDetallePedidoDAO detalleDAO)
        {
            _productoDAO = productoDAO;
            _pedidoDAO = pedidoDAO;
            _detalleDAO = detalleDAO;
        }

        // VALIDAR STOCK
        public void ValidarStock(int idProducto, int cantidad)
        {
            var producto = _productoDAO.GetById(idProducto);

            if (producto == null)
                throw new Exception("Producto no existe");

            if (producto.Stock < cantidad)
                throw new Exception("Stock insuficiente");
        }

        // CALCULAR TOTAL PEDIDO
        public decimal CalcularTotal(List<DetallePedido> detalles)
        {
            decimal total = 0;

            foreach (var item in detalles)
            {
                var producto = _productoDAO.GetById(item.IdProducto);

                if (producto == null)
                    throw new Exception("Producto no existe");

                total += producto.PrecioBase * item.Cantidad;
            }

            return total;
        }

        // CREAR PEDIDO COMPLETO
        public void CrearPedido(Pedido pedido, List<DetallePedido> detalles)
        {
            foreach (var item in detalles)
            {
                ValidarStock(item.IdProducto, item.Cantidad);
            }

            decimal total = CalcularTotal(detalles);

            _pedidoDAO.Insert(pedido);

            foreach (var item in detalles)
            {
                _detalleDAO.Insert(item);

                var producto = _productoDAO.GetById(item.IdProducto);

                if (producto == null)
                    throw new Exception("Producto no existe");

                producto.Stock -= item.Cantidad;
                _productoDAO.Update(producto);
            }
        }

        // ACTUALIZAR STOCK
        public void ActualizarStock(int idProducto, int cantidad)
        {
            var producto = _productoDAO.GetById(idProducto);

            if (producto == null)
                throw new Exception("Producto no existe");

            producto.Stock += cantidad;
            _productoDAO.Update(producto);
        }
    }
}