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
    public class TipoInmuebleControllerTest
    {
        private readonly Mock<ITipoInmuebleRepository> _repositoryMock;
        private readonly TipoInmuebleController _sut; // System Under Test

        public TipoInmuebleControllerTest()
        {
            _repositoryMock = new Mock<ITipoInmuebleRepository>();
            _sut = new TipoInmuebleController(_repositoryMock.Object);

            // Usuario administrador autenticado por defecto para POST, PUT y DELETE
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(1, ControllerTestHelper.RolAdmin));
        }

        #region Pruebas de ListarTipoInmueble (GET)

        [Fact]
        public async Task ListarTipoInmueble_ExistenRegistros_RetornaOkConLista()
        {
            // --- ARRANGE ---
            var listaEsperada = new List<TipoInmueble>
            {
                new TipoInmueble { IdTipo = 1, Nombre = "Casa" },
                new TipoInmueble { IdTipo = 2, Nombre = "Apartamento" }
            };

            _repositoryMock
                .Setup(r => r.GetTipoInmueble())
                .ReturnsAsync(listaEsperada);

            // --- ACT ---
            var result = await _sut.ListarTipoInmueble();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(listaEsperada);
        }

        [Fact]
        public async Task ListarTipoInmueble_ListaNula_RetornaNotFound()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetTipoInmueble())
                .ReturnsAsync((List<TipoInmueble>?)null);

            // --- ACT ---
            var result = await _sut.ListarTipoInmueble();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se encontraron tipos de inmueble registrados.");
        }

        [Fact]
        public async Task ListarTipoInmueble_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetTipoInmueble())
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ListarTipoInmueble();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500: Error interno del servidor.");
        }

        #endregion

        #region Pruebas de ObtenerTipoInmueble (GET por ID)

        [Fact]
        public async Task ObtenerTipoInmueble_IdInvalido_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.ObtenerTipoInmueble(0);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: El ID proporcionado no es válido.");
        }

        [Fact]
        public async Task ObtenerTipoInmueble_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            int idBusqueda = 99;
            _repositoryMock
                .Setup(r => r.GetTipoInmuebleById(idBusqueda))
                .ReturnsAsync((TipoInmueble?)null);

            // --- ACT ---
            var result = await _sut.ObtenerTipoInmueble(idBusqueda);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be($"404: No se encontró el tipo de inmueble con ID {idBusqueda}.");
        }

        [Fact]
        public async Task ObtenerTipoInmueble_Existe_RetornaOkConObjeto()
        {
            // --- ARRANGE ---
            var tipoDb = new TipoInmueble { IdTipo = 1, Nombre = "Casa" };

            _repositoryMock
                .Setup(r => r.GetTipoInmuebleById(1))
                .ReturnsAsync(tipoDb);

            // --- ACT ---
            var result = await _sut.ObtenerTipoInmueble(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(tipoDb);
        }

        [Fact]
        public async Task ObtenerTipoInmueble_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetTipoInmuebleById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ObtenerTipoInmueble(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500: Error interno del servidor.");
        }

        #endregion

        #region Pruebas de CrearTipoInmueble (POST)

        [Fact]
        public async Task CrearTipoInmueble_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);

            // --- ACT ---
            var result = await _sut.CrearTipoInmueble(new TipoInmueble { Nombre = "Local" });

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task CrearTipoInmueble_ObjetoNulo_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.CrearTipoInmueble(null!);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos del tipo de inmueble no pueden ser nulos.");
        }

        [Fact]
        public async Task CrearTipoInmueble_DatosValidos_RetornaOk()
        {
            // --- ARRANGE ---
            var nuevoTipo = new TipoInmueble { Nombre = "Local" };

            _repositoryMock
                .Setup(r => r.PostTipoInmueble(nuevoTipo))
                .ReturnsAsync(true); // El repositorio retorna bool (Task<bool>)

            // --- ACT ---
            var result = await _sut.CrearTipoInmueble(nuevoTipo);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var resultadoBooleano = objectResult.Value.Should().BeOfType<bool>().Subject;
            resultadoBooleano.Should().BeTrue();
        }

        [Fact]
        public async Task CrearTipoInmueble_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.PostTipoInmueble(It.IsAny<TipoInmueble>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.CrearTipoInmueble(new TipoInmueble { Nombre = "Local" });

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500: Error interno del servidor.");
        }

        #endregion

        #region Pruebas de ActualizarTipoInmueble (PUT)

        [Fact]
        public async Task ActualizarTipoInmueble_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);

            // --- ACT ---
            var result = await _sut.ActualizarTipoInmueble(new TipoInmueble { IdTipo = 1, Nombre = "Test" });

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task ActualizarTipoInmueble_ObjetoNulo_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.ActualizarTipoInmueble(null!);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para actualizar o el ID no son válidos.");
        }

        [Fact]
        public async Task ActualizarTipoInmueble_IdInvalido_RetornaBadRequest()
        {
            // --- ARRANGE ---
            var tipoInvalido = new TipoInmueble { IdTipo = 0, Nombre = "Test" };

            // --- ACT ---
            var result = await _sut.ActualizarTipoInmueble(tipoInvalido);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para actualizar o el ID no son válidos.");
        }

        [Fact]
        public async Task ActualizarTipoInmueble_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            var tipo = new TipoInmueble { IdTipo = 5, Nombre = "Bodega" };

            _repositoryMock
                .Setup(r => r.GetTipoInmuebleById(5))
                .ReturnsAsync((TipoInmueble?)null);

            // --- ACT ---
            var result = await _sut.ActualizarTipoInmueble(tipo);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se puede actualizar. El tipo de inmueble con ID 5 no existe.");
        }

        [Fact]
        public async Task ActualizarTipoInmueble_Valido_RetornaOk()
        {
            // --- ARRANGE ---
            var exist = new TipoInmueble { IdTipo = 5, Nombre = "Nombre Antiguo" };
            var aActualizar = new TipoInmueble { IdTipo = 5, Nombre = "Nombre Nuevo" };

            _repositoryMock
                .Setup(r => r.GetTipoInmuebleById(5))
                .ReturnsAsync(exist);

            _repositoryMock
                .Setup(r => r.PutTipoInmueble(It.IsAny<TipoInmueble>()))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.ActualizarTipoInmueble(aActualizar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            _repositoryMock.Verify(r => r.PutTipoInmueble(It.Is<TipoInmueble>(t => t.Nombre == "Nombre Nuevo")), Times.Once);
        }

        [Fact]
        public async Task ActualizarTipoInmueble_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetTipoInmuebleById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ActualizarTipoInmueble(new TipoInmueble { IdTipo = 5, Nombre = "X" });

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500: Error interno del servidor.");
        }

        #endregion

        #region Pruebas de EliminarTipoInmueble (DELETE)

        [Fact]
        public async Task EliminarTipoInmueble_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);

            // --- ACT ---
            var result = await _sut.EliminarTipoInmueble(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task EliminarTipoInmueble_IdInvalido_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.EliminarTipoInmueble(0);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para eliminar o el ID no son válidos.");
        }

        [Fact]
        public async Task EliminarTipoInmueble_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            int idBorrar = 88;

            _repositoryMock
                .Setup(r => r.GetTipoInmuebleById(idBorrar))
                .ReturnsAsync((TipoInmueble?)null);

            // --- ACT ---
            var result = await _sut.EliminarTipoInmueble(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be($"404: No se puede eliminar. El tipo de inmueble con ID {idBorrar} no existe.");
        }

        [Fact]
        public async Task EliminarTipoInmueble_Existe_EliminaYRetornaOk()
        {
            // --- ARRANGE ---
            int idBorrar = 2;
            var tipoExistente = new TipoInmueble { IdTipo = 2, Nombre = "Apartamento" };

            _repositoryMock
                .Setup(r => r.GetTipoInmuebleById(idBorrar))
                .ReturnsAsync(tipoExistente);

            _repositoryMock
                .Setup(r => r.DeleteTipoInmueble(tipoExistente))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.EliminarTipoInmueble(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            _repositoryMock.Verify(r => r.DeleteTipoInmueble(tipoExistente), Times.Once);
        }

        [Fact]
        public async Task EliminarTipoInmueble_RepositorioLanzaExcepcion_RetornaError500ConMensaje()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetTipoInmuebleById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.EliminarTipoInmueble(1);

            // --- ASSERT ---
            // Este es el único endpoint de este controlador que devuelve ex.Message en el 500
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion
    }
}
