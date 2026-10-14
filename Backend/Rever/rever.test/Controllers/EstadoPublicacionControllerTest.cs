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
    public class EstadoPublicacionControllerTest
    {
        private readonly Mock<IEstadoPublicacionRepository> _repositoryMock;
        private readonly EstadoPublicacionController _sut; // System Under Test

        public EstadoPublicacionControllerTest()
        {
            _repositoryMock = new Mock<IEstadoPublicacionRepository>();
            _sut = new EstadoPublicacionController(_repositoryMock.Object);

            // Usuario administrador autenticado por defecto para POST, PUT y DELETE
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(1, ControllerTestHelper.RolAdmin));
        }

        #region Pruebas de ListarEstadoPublicacion (GET)

        [Fact]
        public async Task ListarEstadoPublicacion_ExistenRegistros_RetornaOkConLista()
        {
            // --- ARRANGE ---
            var listaEsperada = new List<EstadoPublicacion>
            {
                new EstadoPublicacion { IdEstado = 1, Nombre = "Activa" },
                new EstadoPublicacion { IdEstado = 2, Nombre = "Vendida" }
            };

            _repositoryMock
                .Setup(r => r.GetEstadoPublicacion())
                .ReturnsAsync(listaEsperada);

            // --- ACT ---
            var result = await _sut.ListarEstadoPublicacion();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(listaEsperada);
        }

        [Fact]
        public async Task ListarEstadoPublicacion_ListaNula_RetornaNotFound()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetEstadoPublicacion())
                .ReturnsAsync((List<EstadoPublicacion>?)null);

            // --- ACT ---
            var result = await _sut.ListarEstadoPublicacion();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se encontraron estados de publicación registrados.");
        }

        [Fact]
        public async Task ListarEstadoPublicacion_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetEstadoPublicacion())
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ListarEstadoPublicacion();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de ObtenerEstadoPublicacion (GET por ID)

        [Fact]
        public async Task ObtenerEstadoPublicacion_IdInvalido_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.ObtenerEstadoPublicacion(0);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: El ID proporcionado no es válido.");
        }

        [Fact]
        public async Task ObtenerEstadoPublicacion_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            int idBusqueda = 99;
            _repositoryMock
                .Setup(r => r.GetEstadoPublicacionById(idBusqueda))
                .ReturnsAsync((EstadoPublicacion?)null);

            // --- ACT ---
            var result = await _sut.ObtenerEstadoPublicacion(idBusqueda);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be($"404: No se encontró el estado de publicación con ID {idBusqueda}.");
        }

        [Fact]
        public async Task ObtenerEstadoPublicacion_Existe_RetornaOkConObjeto()
        {
            // --- ARRANGE ---
            int idBusqueda = 1;
            var estadoDb = new EstadoPublicacion { IdEstado = 1, Nombre = "Activa" };

            _repositoryMock
                .Setup(r => r.GetEstadoPublicacionById(idBusqueda))
                .ReturnsAsync(estadoDb);

            // --- ACT ---
            var result = await _sut.ObtenerEstadoPublicacion(idBusqueda);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(estadoDb);
        }

        [Fact]
        public async Task ObtenerEstadoPublicacion_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetEstadoPublicacionById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ObtenerEstadoPublicacion(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de CrearEstadoPublicacion (POST)

        [Fact]
        public async Task CrearEstadoPublicacion_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);
            var nuevoEstado = new EstadoPublicacion { Nombre = "Pausada" };

            // --- ACT ---
            var result = await _sut.CrearEstadoPublicacion(nuevoEstado);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task CrearEstadoPublicacion_ObjetoNulo_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.CrearEstadoPublicacion(null!);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos del estado de publicación no pueden ser nulos.");
        }

        [Fact]
        public async Task CrearEstadoPublicacion_DatosValidos_RetornaOk()
        {
            // --- ARRANGE ---
            var nuevoEstado = new EstadoPublicacion { Nombre = "Pausada" };

            _repositoryMock
                .Setup(r => r.PostEstadoPublicacion(nuevoEstado))
                .ReturnsAsync(true); // El repositorio retorna bool (Task<bool>)

            // --- ACT ---
            var result = await _sut.CrearEstadoPublicacion(nuevoEstado);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var resultadoBooleano = objectResult.Value.Should().BeOfType<bool>().Subject;
            resultadoBooleano.Should().BeTrue();
        }

        [Fact]
        public async Task CrearEstadoPublicacion_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.PostEstadoPublicacion(It.IsAny<EstadoPublicacion>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.CrearEstadoPublicacion(new EstadoPublicacion { Nombre = "Pausada" });

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de ActualizarEstadoPublicacion (PUT)

        [Fact]
        public async Task ActualizarEstadoPublicacion_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);

            // --- ACT ---
            var result = await _sut.ActualizarEstadoPublicacion(new EstadoPublicacion { IdEstado = 1, Nombre = "Test" });

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task ActualizarEstadoPublicacion_IdInvalido_RetornaBadRequest()
        {
            // --- ARRANGE ---
            var estadoInvalido = new EstadoPublicacion { IdEstado = 0, Nombre = "Test" };

            // --- ACT ---
            var result = await _sut.ActualizarEstadoPublicacion(estadoInvalido);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para actualizar o el ID no son válidos.");
        }

        [Fact]
        public async Task ActualizarEstadoPublicacion_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            var estado = new EstadoPublicacion { IdEstado = 5, Nombre = "Archivada" };

            _repositoryMock
                .Setup(r => r.GetEstadoPublicacionById(5))
                .ReturnsAsync((EstadoPublicacion?)null);

            // --- ACT ---
            var result = await _sut.ActualizarEstadoPublicacion(estado);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se puede actualizar. El estado de publicación con ID 5 no existe.");
        }

        [Fact]
        public async Task ActualizarEstadoPublicacion_Valido_RetornaOk()
        {
            // --- ARRANGE ---
            var exist = new EstadoPublicacion { IdEstado = 5, Nombre = "Nombre Antiguo" };
            var aActualizar = new EstadoPublicacion { IdEstado = 5, Nombre = "Nombre Nuevo" };

            _repositoryMock
                .Setup(r => r.GetEstadoPublicacionById(5))
                .ReturnsAsync(exist);

            _repositoryMock
                .Setup(r => r.PutEstadoPublicacion(It.IsAny<EstadoPublicacion>()))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.ActualizarEstadoPublicacion(aActualizar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            _repositoryMock.Verify(r => r.PutEstadoPublicacion(It.Is<EstadoPublicacion>(e => e.Nombre == "Nombre Nuevo")), Times.Once);
        }

        #endregion

        #region Pruebas de DeleteEstadoPublicacion (DELETE)

        [Fact]
        public async Task DeleteEstadoPublicacion_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);

            // --- ACT ---
            var result = await _sut.DeleteEstadoPublicacion(1);

            // --- ASSERT ---
            // OJO: el controlador dice "Estado de publicación no autenticado." (parece un error de copiar/pegar;
            // debería decir "Usuario no autenticado."). Si lo corriges en el controlador, cambia este mensaje.
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Estado de publicación no autenticado.");
        }

        [Fact]
        public async Task DeleteEstadoPublicacion_IdInvalido_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.DeleteEstadoPublicacion(0);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: El ID del estado de publicación no es válido.");
        }

        [Fact]
        public async Task DeleteEstadoPublicacion_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            int idBorrar = 88;

            _repositoryMock
                .Setup(r => r.GetEstadoPublicacionById(idBorrar))
                .ReturnsAsync((EstadoPublicacion?)null);

            // --- ACT ---
            var result = await _sut.DeleteEstadoPublicacion(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be($"404: No se puede eliminar. El estado de publicación con ID {idBorrar} no existe.");
        }

        [Fact]
        public async Task DeleteEstadoPublicacion_Existe_EliminaYRetornaOk()
        {
            // --- ARRANGE ---
            int idBorrar = 2;
            var estadoExistente = new EstadoPublicacion { IdEstado = 2, Nombre = "Vendida" };

            _repositoryMock
                .Setup(r => r.GetEstadoPublicacionById(idBorrar))
                .ReturnsAsync(estadoExistente);

            _repositoryMock
                .Setup(r => r.DeleteEstadoPublicacion(estadoExistente))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.DeleteEstadoPublicacion(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            _repositoryMock.Verify(r => r.DeleteEstadoPublicacion(estadoExistente), Times.Once);
        }

        [Fact]
        public async Task DeleteEstadoPublicacion_RepositorioLanzaExcepcion_RetornaError500Generico()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetEstadoPublicacionById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.DeleteEstadoPublicacion(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500: Error interno del servidor.");
        }

        #endregion
    }
}
