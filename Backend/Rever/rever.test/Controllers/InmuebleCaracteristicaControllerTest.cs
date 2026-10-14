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
    public class InmuebleCaracteristicaControllerTest
    {
        private readonly Mock<IInmuebleCaracteristicaRepository> _repositoryMock;
        private readonly Mock<IInmuebleRepository> _inmuebleRepositoryMock;
        private readonly InmuebleCaracteristicaController _sut; // System Under Test

        public InmuebleCaracteristicaControllerTest()
        {
            _repositoryMock = new Mock<IInmuebleCaracteristicaRepository>();
            _inmuebleRepositoryMock = new Mock<IInmuebleRepository>();

            _sut = new InmuebleCaracteristicaController(_repositoryMock.Object, _inmuebleRepositoryMock.Object);

            // Usuario NO admin con id 7 por defecto (dueño de los inmuebles de prueba)
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(7, ControllerTestHelper.RolUsuario));
        }

        #region Pruebas de ListarInmuebleCaracteristica (GET)

        [Fact]
        public async Task ListarInmuebleCaracteristica_ExistenRegistros_RetornaOkConLista()
        {
            // --- ARRANGE ---
            var listaEsperada = new List<InmuebleCaracteristica>
            {
                new InmuebleCaracteristica { IdInmueble = 1 },
                new InmuebleCaracteristica { IdInmueble = 2 }
            };

            _repositoryMock
                .Setup(r => r.GetInmuebleCaracteristica())
                .ReturnsAsync(listaEsperada);

            // --- ACT ---
            var result = await _sut.ListarInmuebleCaracteristica();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(listaEsperada);
        }

        [Fact]
        public async Task ListarInmuebleCaracteristica_ListaNula_RetornaNotFound()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetInmuebleCaracteristica())
                .ReturnsAsync((List<InmuebleCaracteristica>?)null);

            // --- ACT ---
            var result = await _sut.ListarInmuebleCaracteristica();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se encontraron características de inmuebles registradas.");
        }

        [Fact]
        public async Task ListarInmuebleCaracteristica_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetInmuebleCaracteristica())
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ListarInmuebleCaracteristica();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de ObtenerInmuebleCaracteristica (GET por IDs)

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(-1, -1)]
        public async Task ObtenerInmuebleCaracteristica_IdsInvalidos_RetornaBadRequest(int id1, int id2)
        {
            // --- ACT ---
            var result = await _sut.ObtenerInmuebleCaracteristica(id1, id2);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los IDs proporcionados no son válidos.");
        }

        [Fact]
        public async Task ObtenerInmuebleCaracteristica_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetByIds(5, 9))
                .ReturnsAsync((InmuebleCaracteristica?)null);

            // --- ACT ---
            var result = await _sut.ObtenerInmuebleCaracteristica(5, 9);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se encontró la relación entre el Inmueble 5 y la Característica 9.");
        }

        [Fact]
        public async Task ObtenerInmuebleCaracteristica_Existe_RetornaOkConObjeto()
        {
            // --- ARRANGE ---
            var relacionDb = new InmuebleCaracteristica { IdInmueble = 5 };

            _repositoryMock
                .Setup(r => r.GetByIds(5, 9))
                .ReturnsAsync(relacionDb);

            // --- ACT ---
            var result = await _sut.ObtenerInmuebleCaracteristica(5, 9);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(relacionDb);
        }

        [Fact]
        public async Task ObtenerInmuebleCaracteristica_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetByIds(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ObtenerInmuebleCaracteristica(5, 9);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de CrearInmuebleCaracteristica (POST)

        [Fact]
        public async Task CrearInmuebleCaracteristica_ObjetoNulo_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.CrearInmuebleCaracteristica(null!);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos enviados no pueden ser nulos.");
        }

        [Fact]
        public async Task CrearInmuebleCaracteristica_IdInmuebleInvalido_RetornaBadRequest()
        {
            // --- ARRANGE ---
            var relacion = new InmuebleCaracteristica { IdInmueble = 0 };

            // --- ACT ---
            var result = await _sut.CrearInmuebleCaracteristica(relacion);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos enviados no pueden ser nulos.");
        }

        [Fact]
        public async Task CrearInmuebleCaracteristica_InmuebleNoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            var relacion = new InmuebleCaracteristica { IdInmueble = 5 };

            _inmuebleRepositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync((Inmueble?)null);

            // --- ACT ---
            var result = await _sut.CrearInmuebleCaracteristica(relacion);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: El inmueble con ID 5 no existe.");
        }

        [Fact]
        public async Task CrearInmuebleCaracteristica_InmuebleDeOtroUsuario_RetornaForbidden()
        {
            // --- ARRANGE --- (el usuario es el 7, el dueño es el 50)
            var relacion = new InmuebleCaracteristica { IdInmueble = 5 };

            _inmuebleRepositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync(ControllerTestHelper.CrearInmueble(50, 5));

            // --- ACT ---
            var result = await _sut.CrearInmuebleCaracteristica(relacion);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(403);
            objectResult.Value.Should().Be("403: No puedes agregar características a un inmueble que no es tuyo.");
            _repositoryMock.Verify(r => r.PostInmuebleCaracteristica(It.IsAny<InmuebleCaracteristica>()), Times.Never);
        }

        [Fact]
        public async Task CrearInmuebleCaracteristica_UsuarioEsDueno_RetornaOk()
        {
            // --- ARRANGE ---
            var relacion = new InmuebleCaracteristica { IdInmueble = 5 };

            _inmuebleRepositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync(ControllerTestHelper.CrearInmueble(7, 5));

            _repositoryMock
                .Setup(r => r.PostInmuebleCaracteristica(relacion))
                .ReturnsAsync(true); // El repositorio retorna bool (Task<bool>)

            // --- ACT ---
            var result = await _sut.CrearInmuebleCaracteristica(relacion);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var resultadoBooleano = objectResult.Value.Should().BeOfType<bool>().Subject;
            resultadoBooleano.Should().BeTrue();
        }

        [Fact]
        public async Task CrearInmuebleCaracteristica_UsuarioAdminInmuebleAjeno_RetornaOk()
        {
            // --- ARRANGE --- (EsDueñoOAdmin usa el rol "1" como admin)
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(1, ControllerTestHelper.RolAdmin));
            var relacion = new InmuebleCaracteristica { IdInmueble = 5 };

            _inmuebleRepositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync(ControllerTestHelper.CrearInmueble(50, 5));

            _repositoryMock
                .Setup(r => r.PostInmuebleCaracteristica(relacion))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.CrearInmuebleCaracteristica(relacion);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task CrearInmuebleCaracteristica_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _inmuebleRepositoryMock
                .Setup(r => r.GetInmuebleById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.CrearInmuebleCaracteristica(new InmuebleCaracteristica { IdInmueble = 5 });

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de EliminarInmuebleCaracteristica (DELETE)

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        public async Task EliminarInmuebleCaracteristica_IdsInvalidos_RetornaBadRequest(int id1, int id2)
        {
            // --- ACT ---
            var result = await _sut.EliminarInmuebleCaracteristica(id1, id2);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para eliminar o los IDs no son válidos.");
        }

        [Fact]
        public async Task EliminarInmuebleCaracteristica_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetByIds(5, 9))
                .ReturnsAsync((InmuebleCaracteristica?)null);

            // --- ACT ---
            var result = await _sut.EliminarInmuebleCaracteristica(5, 9);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se puede eliminar. La relación especificada no existe.");
        }

        [Fact]
        public async Task EliminarInmuebleCaracteristica_InmuebleNoExiste_RetornaForbidden()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetByIds(5, 9))
                .ReturnsAsync(new InmuebleCaracteristica { IdInmueble = 5 });

            _inmuebleRepositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync((Inmueble?)null);

            // --- ACT ---
            var result = await _sut.EliminarInmuebleCaracteristica(5, 9);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(403);
            objectResult.Value.Should().Be("403: No puedes eliminar características de un inmueble que no es tuyo.");
        }

        [Fact]
        public async Task EliminarInmuebleCaracteristica_InmuebleDeOtroUsuario_RetornaForbidden()
        {
            // --- ARRANGE ---
            var relacion = new InmuebleCaracteristica { IdInmueble = 5 };

            _repositoryMock
                .Setup(r => r.GetByIds(5, 9))
                .ReturnsAsync(relacion);

            _inmuebleRepositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync(ControllerTestHelper.CrearInmueble(50, 5));

            // --- ACT ---
            var result = await _sut.EliminarInmuebleCaracteristica(5, 9);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(403);
            objectResult.Value.Should().Be("403: No puedes eliminar características de un inmueble que no es tuyo.");
            _repositoryMock.Verify(r => r.DeleteInmuebleCaracteristica(It.IsAny<InmuebleCaracteristica>()), Times.Never);
        }

        [Fact]
        public async Task EliminarInmuebleCaracteristica_UsuarioEsDueno_EliminaYRetornaOk()
        {
            // --- ARRANGE ---
            var relacion = new InmuebleCaracteristica { IdInmueble = 5 };

            _repositoryMock
                .Setup(r => r.GetByIds(5, 9))
                .ReturnsAsync(relacion);

            _inmuebleRepositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync(ControllerTestHelper.CrearInmueble(7, 5));

            _repositoryMock
                .Setup(r => r.DeleteInmuebleCaracteristica(relacion))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.EliminarInmuebleCaracteristica(5, 9);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            _repositoryMock.Verify(r => r.DeleteInmuebleCaracteristica(relacion), Times.Once);
        }

        [Fact]
        public async Task EliminarInmuebleCaracteristica_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetByIds(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.EliminarInmuebleCaracteristica(5, 9);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion
    }
}
