using Xunit;
using DistribuidoraAseo.DAO.Implementations;
using DistribuidoraAseo.Data;
using DistribuidoraAseo.Models;

namespace DistribuidoraAseo.Tests
{
    public class Usuarioprueba
    {
        private readonly UsuarioDAO _dao;

        public Usuarioprueba()
        {
            var db = new DatabaseConnection();
            _dao = new UsuarioDAO(db);
        }

        [Fact]
        public void Insert_DeberiaInsertarUsuario()
        {
            // Arrange
            var usuario = new Usuario
            {
                IdUsuario = 999,
                Nombre = "Usuario Prueba",
                Correo = "prueba@test.com",
                Contrasena = "123456",
                IdRol = 1
            };

            // Act
            _dao.Insert(usuario);

            // Assert
            var resultado = _dao.GetById(999);

            Assert.NotNull(resultado);
            Assert.Equal("Usuario Prueba", resultado.Nombre);
        }

        [Fact]
        public void GetByCorreo_DeberiaRetornarUsuario()
        {
            // Act
            var usuario = _dao.GetByCorreo("prueba@test.com");

            // Assert
            Assert.NotNull(usuario);
            Assert.Equal("Usuario Prueba", usuario.Nombre);
        }

        [Fact]
        public void Update_DeberiaActualizarUsuario()
        {
            // Arrange
            var usuario = _dao.GetById(999);

            usuario.Nombre = "Usuario Actualizado";

            // Act
            _dao.Update(usuario);

            // Assert
            var actualizado = _dao.GetById(999);

            Assert.Equal("Usuario Actualizado", actualizado.Nombre);
        }

        [Fact]
        public void Delete_DeberiaEliminarUsuario()
        {
            // Act
            _dao.Delete(999);

            // Assert
            var eliminado = _dao.GetById(999);

            Assert.Null(eliminado);
        }

        [Fact]
        public void GetAll_DeberiaRetornarUsuarios()
        {
            // Act
            var usuarios = _dao.GetAll();

            // Assert
            Assert.NotEmpty(usuarios);
        }
    }
}