using Xunit;
using DistribuidoraAseo.DAO.Implementations;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.Tests
{
    public class ProductoDAOTests : IDisposable
    {
        private readonly ProductoDAO _dao;

        // ID único para evitar conflictos
        private readonly int _testId = new Random().Next(10000, 99999);

        public ProductoDAOTests()
        {
            var db = new DatabaseConnection();
            _dao = new ProductoDAO(db);
        }

        // Limpieza automática después de cada prueba
        public void Dispose()
        {
            try
            {
                _dao.Delete(_testId);
            }
            catch
            {
                // Ignorar errores si el producto no existe
            }
        }

        [Fact]
        public void Insert_DeberiaInsertarProducto()
        {
            // Arrange
            var producto = CrearProducto();

            // Act
            _dao.Insert(producto);

            // Assert
            var resultado = _dao.GetById(_testId);

            Assert.NotNull(resultado);
            Assert.Equal(producto.Nombre, resultado.Nombre);
            Assert.Equal(producto.Tipo, resultado.Tipo);
            Assert.Equal(producto.Stock, resultado.Stock);
            Assert.Equal(producto.PrecioBase, resultado.PrecioBase);
        }

        [Fact]
        public void Update_DeberiaActualizarProducto()
        {
            // Arrange
            var producto = CrearProducto();

            _dao.Insert(producto);

            var productoGuardado = _dao.GetById(_testId);

            Assert.NotNull(productoGuardado);

            productoGuardado.Nombre = "Jabon Actualizado";

            // Act
            _dao.Update(productoGuardado);

            // Assert
            var actualizado = _dao.GetById(_testId);

            Assert.NotNull(actualizado);
            Assert.Equal("Jabon Actualizado", actualizado.Nombre);
        }

        [Fact]
        public void Delete_DeberiaEliminarProducto()
        {
            // Arrange
            var producto = CrearProducto();

            _dao.Insert(producto);

            // Act
            _dao.Delete(_testId);

            // Assert
            var eliminado = _dao.GetById(_testId);

            Assert.Null(eliminado);
        }

        [Fact]
        public void GetAll_DeberiaRetornarListaDeProductos()
        {
            // Act
            var productos = _dao.GetAll();

            // Assert
            Assert.NotNull(productos);
        }

        // Método auxiliar para evitar repetir código
        private Producto CrearProducto()
        {
            return new Producto
            {
                IdProducto = _testId,
                Nombre = "Jabon Azul",
                Tipo = "Jabon Liquido",
                PrecioBase = 150000,
                Stock = 10,
                IdFabricador = 1
            };
        }
    }
}