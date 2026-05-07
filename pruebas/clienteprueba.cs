using DistribuidoraAseo.DAO.Implementations;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Models;
using Xunit;

namespace DistribuidoraAseo.Tests
{
    public class Clienteprueba
    {
        [Fact]
        public void Insert_DeberiaInsertarCliente()
        {
            // Arrange
            var db = new DatabaseConnection();
            var dao = new ClienteDAO(db);

            var cliente = new Cliente
            {
                IdCliente = 100,
                Nombre = "Alejandro",
                Direccion = "Medellin",
                TipoCliente = "Frecuente"
            };

            // Act
            dao.Insert(cliente);

            // Assert
            var resultado = dao.GetById(100);

            Assert.NotNull(resultado);
            Assert.Equal("Alejandro", resultado.Nombre);
        }
    }
}