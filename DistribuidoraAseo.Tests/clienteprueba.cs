using Xunit;
using DistribuidoraAseo.DAO.Implementations;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.Tests
{
    public class ClienteDAOTests : IDisposable
    {
        private readonly ClienteDAO _dao;

        // ID único para evitar conflictos
        private readonly int _testId = new Random().Next(10000, 99999);

        public ClienteDAOTests()
        {
            var db = new DatabaseConnection();
            _dao = new ClienteDAO(db);
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
                // Ignorar si el cliente no existe
            }
        }

        [Fact]
        public void Insert_DeberiaInsertarCliente()
        {
            // Arrange
            var cliente = CrearCliente();

            // Act
            _dao.Insert(cliente);

            // Assert
            var resultado = _dao.GetById(_testId);

            Assert.NotNull(resultado);
            Assert.Equal(cliente.Nombre, resultado.Nombre);
            Assert.Equal(cliente.Direccion, resultado.Direccion);
            Assert.Equal(cliente.TipoCliente, resultado.TipoCliente);
        }

        [Fact]
        public void GetById_DeberiaRetornarCliente()
        {
            // Arrange
            var cliente = CrearCliente();

            _dao.Insert(cliente);

            // Act
            var resultado = _dao.GetById(_testId);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(_testId, resultado.IdCliente);
        }

        [Fact]
        public void Update_DeberiaActualizarCliente()
        {
            // Arrange
            var cliente = CrearCliente();

            _dao.Insert(cliente);

            var clienteGuardado = _dao.GetById(_testId);

            Assert.NotNull(clienteGuardado);

            clienteGuardado.Nombre = "Cliente Actualizado";

            // Act
            _dao.Update(clienteGuardado);

            // Assert
            var actualizado = _dao.GetById(_testId);

            Assert.NotNull(actualizado);
            Assert.Equal("Cliente Actualizado", actualizado.Nombre);
        }

        [Fact]
        public void Delete_DeberiaEliminarCliente()
        {
            // Arrange
            var cliente = CrearCliente();

            _dao.Insert(cliente);

            // Act
            _dao.Delete(_testId);

            // Assert
            var eliminado = _dao.GetById(_testId);

            Assert.Null(eliminado);
        }

        [Fact]
        public void GetAll_DeberiaRetornarClientes()
        {
            // Act
            var clientes = _dao.GetAll();

            // Assert
            Assert.NotNull(clientes);
        }

        // Método auxiliar reutilizable
        private Cliente CrearCliente()
        {
            return new Cliente
            {
                IdCliente = _testId,
                Nombre = "Alejandro",
                Direccion = "Medellin",
                TipoCliente = "Frecuente"
            };
        }
    }
}