using Xunit;
using DistribuidoraAseo.DAO.Implementations;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.Tests
{
    public class UsuarioDAOTests : IDisposable
    {
        private readonly UsuarioDAO _dao;

        // ID único para evitar conflictos
        private readonly int _testId = new Random().Next(10000, 99999);

        // Correo único para evitar duplicados
        private readonly string _testCorreo =
            $"usuario{Guid.NewGuid()}@test.com";

        public UsuarioDAOTests()
        {
            var db = new DatabaseConnection();
            _dao = new UsuarioDAO(db);
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
                // Ignorar si no existe
            }
        }

        [Fact]
        public void Insert_DeberiaInsertarUsuario()
        {
            // Arrange
            var usuario = CrearUsuario();

            // Act
            _dao.Insert(usuario);

            // Assert
            var resultado = _dao.GetById(_testId);

            Assert.NotNull(resultado);
            Assert.Equal(usuario.Nombre, resultado.Nombre);
            Assert.Equal(usuario.Correo, resultado.Correo);
            Assert.Equal(usuario.IdRol, resultado.IdRol);
        }

        [Fact]
        public void GetByCorreo_DeberiaRetornarUsuario()
        {
            // Arrange
            var usuario = CrearUsuario();

            _dao.Insert(usuario);

            // Act
            var resultado = _dao.GetByCorreo(_testCorreo);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(usuario.Nombre, resultado.Nombre);
            Assert.Equal(usuario.Correo, resultado.Correo);
        }

        [Fact]
        public void Update_DeberiaActualizarUsuario()
        {
            // Arrange
            var usuario = CrearUsuario();

            _dao.Insert(usuario);

            var usuarioGuardado = _dao.GetById(_testId);

            Assert.NotNull(usuarioGuardado);

            usuarioGuardado.Nombre = "Usuario Actualizado";

            // Act
            _dao.Update(usuarioGuardado);

            // Assert
            var actualizado = _dao.GetById(_testId);

            Assert.NotNull(actualizado);
            Assert.Equal("Usuario Actualizado", actualizado.Nombre);
        }

        [Fact]
        public void Delete_DeberiaEliminarUsuario()
        {
            // Arrange
            var usuario = CrearUsuario();

            _dao.Insert(usuario);

            // Act
            _dao.Delete(_testId);

            // Assert
            var eliminado = _dao.GetById(_testId);

            Assert.Null(eliminado);
        }

        [Fact]
        public void GetAll_DeberiaRetornarListaDeUsuarios()
        {
            // Act
            var usuarios = _dao.GetAll();

            // Assert
            Assert.NotNull(usuarios);
        }

        // Método auxiliar para reutilizar datos
        private Usuario CrearUsuario()
        {
            return new Usuario
            {
                IdUsuario = _testId,
                Nombre = "Usuario Prueba",
                Correo = _testCorreo,
                Contrasena = "123456",
                IdRol = 1
            };
        }
    }
}