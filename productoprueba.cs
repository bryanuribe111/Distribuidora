using Xunit;
using DistribuidoraAseo.DAO.Implementations;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.Tests
{
    public class Productoprueba
    {
        private readonly ProductoDAO _dao;

        public Productoprueba()
        {
            var db = new DatabaseConnection();
            _dao = new ProductoDAO(db);
        }

        [Fact]
        public void Insert_DeberiaInsertarProducto()
        {
            // Arrange
            var producto = new Producto
            {
                IdProducto = 999,
                Nombre = "Jabon azul",
                Tipo = "Jabon liquido",
                PrecioBase = 150000,
                Stock = 10,
                IdFabricador = 001
            };

            // Act
            _dao.Insert(producto);

            // Assert
            var resultado = _dao.GetById(999);

            Assert.NotNull(resultado);
            Assert.Equal("Jabon azul", resultado.Nombre);
        }

        [Fact]
        public void Update_DeberiaActualizarProducto()
        {
            // Arrange
            var producto = _dao.GetById(999);

            producto.Nombre = "Jabon Actualizado";

            // Act
            _dao.Update(producto);

            // Assert
            var actualizado = _dao.GetById(999);

            Assert.Equal("Jabon Actualizado", actualizado.Nombre);
        }

        [Fact]
        public void Delete_DeberiaEliminarProducto()
        {
            // Act
            _dao.Delete(999);

            // Assert
            var eliminado = _dao.GetById(999);

            Assert.Null(eliminado);
        }

        [Fact]
        public void GetAll_DeberiaRetornarProductos()
        {
            // Act
            var productos = _dao.GetAll();

            // Assert
            Assert.NotEmpty(productos);
        }
    }
}