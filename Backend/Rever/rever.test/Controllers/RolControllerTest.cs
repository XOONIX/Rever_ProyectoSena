using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using rever.Controllers;
using rever.Models;
using rever.Repositories.Interfaces;
using rever.Tests.Helpers;
using Xunit;

namespace rever.Tests.Controllers
{
    public class RolControllerTest
    {
        private readonly Mock<IRolRepository> _repositoryMock;
        private readonly RolController _sut; // System Under Test

        public RolControllerTest()
        {
            _repositoryMock = new Mock<IRolRepository>();
            _sut = new RolController(_repositoryMock.Object);

            // Usuario administrador autenticado por defecto (todos los endpoints exigen autenticación)
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(1, ControllerTestHelper.RolAdmin));
        }

        #region Pruebas de ListarRol (GET)

        [Fact]
        public async Task ListarRol_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);

            // --- ACT ---
            var result = await _sut.ListarRol();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task ListarRol_ExistenRegistros_RetornaOkConLista()
        {
            // --- ARRANGE ---
            var listaEsperada = new List<Rol>
            {
                new Rol { IdRol = 1, Nombre = "Administrador" },
                new Rol { IdRol = 2, Nombre = "Vendedor" }
            };

            _repositoryMock
                .Setup(r => r.GetRol())
                .ReturnsAsync(listaEsperada);

            // --- ACT ---
            var result = await _sut.ListarRol();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(listaEsperada);
        }

        [Fact]
        public async Task ListarRol_ListaNula_RetornaNotFound()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetRol())
                .ReturnsAsync((List<Rol>?)null);

            // --- ACT ---
            var result = await _sut.ListarRol();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se encontraron roles registrados.");
        }

        [Fact]
        public async Task ListarRol_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetRol())
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ListarRol();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500: Error interno del servidor.");
        }

        #endregion

        #region Pruebas de ObtenerRol (GET por ID)

        [Fact]
        public async Task ObtenerRol_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);

            // --- ACT ---
            var result = await _sut.ObtenerRol(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task ObtenerRol_IdInvalido_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.ObtenerRol(0);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: El ID proporcionado no es válido.");
        }

        [Fact]
        public async Task ObtenerRol_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            int idBusqueda = 99;
            _repositoryMock
                .Setup(r => r.GetRolById(idBusqueda))
                .ReturnsAsync((Rol?)null);

            // --- ACT ---
            var result = await _sut.ObtenerRol(idBusqueda);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be($"404: No se encontró el rol con ID {idBusqueda}.");
        }

        [Fact]
        public async Task ObtenerRol_Existe_RetornaOkConObjeto()
        {
            // --- ARRANGE ---
            var rolDb = new Rol { IdRol = 1, Nombre = "Administrador" };

            _repositoryMock
                .Setup(r => r.GetRolById(1))
                .ReturnsAsync(rolDb);

            // --- ACT ---
            var result = await _sut.ObtenerRol(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(rolDb);
        }

        [Fact]
        public async Task ObtenerRol_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetRolById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ObtenerRol(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500: Error interno del servidor.");
        }

        #endregion

        #region Pruebas de CrearRol (POST)

        [Fact]
        public async Task CrearRol_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);

            // --- ACT ---
            var result = await _sut.CrearRol(new Rol { Nombre = "Comprador" });

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task CrearRol_ObjetoNulo_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.CrearRol(null!);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos del rol no pueden ser nulos.");
        }

        [Fact]
        public async Task CrearRol_DatosValidos_RetornaOk()
        {
            // --- ARRANGE ---
            var nuevoRol = new Rol { Nombre = "Comprador" };

            _repositoryMock
                .Setup(r => r.PostRol(nuevoRol))
                .ReturnsAsync(true); // El repositorio retorna bool (Task<bool>)

            // --- ACT ---
            var result = await _sut.CrearRol(nuevoRol);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var resultadoBooleano = objectResult.Value.Should().BeOfType<bool>().Subject;
            resultadoBooleano.Should().BeTrue();
        }

        [Fact]
        public async Task CrearRol_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.PostRol(It.IsAny<Rol>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.CrearRol(new Rol { Nombre = "Comprador" });

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500: Error interno del servidor.");
        }

        #endregion

        #region Pruebas de ActualizarRol (PUT)

        [Fact]
        public async Task ActualizarRol_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);

            // --- ACT ---
            var result = await _sut.ActualizarRol(new Rol { IdRol = 1, Nombre = "Test" });

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task ActualizarRol_ObjetoNulo_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.ActualizarRol(null!);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para actualizar o el ID no son válidos.");
        }

        [Fact]
        public async Task ActualizarRol_IdInvalido_RetornaBadRequest()
        {
            // --- ARRANGE ---
            var rolInvalido = new Rol { IdRol = 0, Nombre = "Test" };

            // --- ACT ---
            var result = await _sut.ActualizarRol(rolInvalido);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para actualizar o el ID no son válidos.");
        }

        [Fact]
        public async Task ActualizarRol_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            var rol = new Rol { IdRol = 5, Nombre = "Inexistente" };

            _repositoryMock
                .Setup(r => r.GetRolById(5))
                .ReturnsAsync((Rol?)null);

            // --- ACT ---
            var result = await _sut.ActualizarRol(rol);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se puede actualizar. El rol con ID 5 no existe.");
        }

        [Fact]
        public async Task ActualizarRol_Valido_RetornaOk()
        {
            // --- ARRANGE ---
            var exist = new Rol { IdRol = 5, Nombre = "Nombre Antiguo" };
            var aActualizar = new Rol { IdRol = 5, Nombre = "Nombre Nuevo" };

            _repositoryMock
                .Setup(r => r.GetRolById(5))
                .ReturnsAsync(exist);

            _repositoryMock
                .Setup(r => r.PutRol(It.IsAny<Rol>()))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.ActualizarRol(aActualizar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            _repositoryMock.Verify(r => r.PutRol(It.Is<Rol>(x => x.Nombre == "Nombre Nuevo")), Times.Once);
        }

        [Fact]
        public async Task ActualizarRol_RepositorioLanzaExcepcion_RetornaError500ConDetalle()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetRolById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ActualizarRol(new Rol { IdRol = 5, Nombre = "X" });

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500: Error interno del servidor. Detalle: Fallo de BD");
        }

        #endregion

        #region Pruebas de EliminarRol (DELETE)

        [Fact]
        public async Task EliminarRol_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);

            // --- ACT ---
            var result = await _sut.EliminarRol(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task EliminarRol_IdInvalido_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.EliminarRol(0);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para eliminar o el ID no son válidos.");
        }

        [Fact]
        public async Task EliminarRol_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            int idBorrar = 88;

            _repositoryMock
                .Setup(r => r.GetRolById(idBorrar))
                .ReturnsAsync((Rol?)null);

            // --- ACT ---
            var result = await _sut.EliminarRol(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be($"404: No se puede eliminar. El rol con ID {idBorrar} no existe.");
        }

        [Fact]
        public async Task EliminarRol_Existe_EliminaYRetornaOk()
        {
            // --- ARRANGE ---
            int idBorrar = 2;
            var rolExistente = new Rol { IdRol = 2, Nombre = "Vendedor" };

            _repositoryMock
                .Setup(r => r.GetRolById(idBorrar))
                .ReturnsAsync(rolExistente);

            _repositoryMock
                .Setup(r => r.DeleteRol(rolExistente))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.EliminarRol(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            _repositoryMock.Verify(r => r.DeleteRol(rolExistente), Times.Once);
        }

        [Fact]
        public async Task EliminarRol_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetRolById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.EliminarRol(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500: Error interno del servidor.");
        }

        #endregion
    }
}
