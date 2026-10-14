using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using rever.Controllers;
using rever.Models;
using rever.Repositories.Interfaces;
using Xunit;

namespace rever.Tests.Controllers
{
    public class CaracteristicaControllerTest
    {
        private readonly Mock<ICaracteristicaRepository> _repositoryMock;
        private readonly CaracteristicaController _sut; // System Under Test

        public CaracteristicaControllerTest()
        {
            // 1. Crear el Mock de la dependencia
            _repositoryMock = new Mock<ICaracteristicaRepository>();

            // 2. Instanciar el controlador con el repositorio simulado
            _sut = new CaracteristicaController(_repositoryMock.Object);

            // 3. Simular un usuario autenticado por defecto para las pruebas de POST, PUT y DELETE
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "AdminUser"),
                new Claim(ClaimTypes.Role, "1")
            }, "TestAuthentication"));

            _sut.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        #region Pruebas de ListarCaracteristica (GET)

        [Fact]
        public async Task ListarCaracteristica_ExistenRegistros_RetornaOkConLista()
        {
            // --- ARRANGE ---
            var listaEsperada = new List<Caracteristica>
            {
                new Caracteristica { IdCaracteristica = 1, Nombre = "Piscina" },
                new Caracteristica { IdCaracteristica = 2, Nombre = "Parqueadero" }
            };

            _repositoryMock
                .Setup(r => r.GetCaracteristica())
                .ReturnsAsync(listaEsperada);

            // --- ACT ---
            var result = await _sut.ListarCaracteristica();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(listaEsperada);
        }

        [Fact]
        public async Task ListarCaracteristica_ListaNula_RetornaNotFound()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetCaracteristica())
                .ReturnsAsync((List<Caracteristica>?)null);

            // --- ACT ---
            var result = await _sut.ListarCaracteristica();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se encontraron características registradas.");
        }

        #endregion

        #region Pruebas de ObtenerCaracteristica (GET por ID)

        [Fact]
        public async Task ObtenerCaracteristica_IdInvalido_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.ObtenerCaracteristica(0);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: El ID proporcionado no es válido.");
        }

        [Fact]
        public async Task ObtenerCaracteristica_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            int idBusqueda = 99;
            _repositoryMock
                .Setup(r => r.GetCaracteristicaById(idBusqueda))
                .ReturnsAsync((Caracteristica?)null);

            // --- ACT ---
            var result = await _sut.ObtenerCaracteristica(idBusqueda);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be($"404: No se encontró la característica con ID {idBusqueda}.");
        }

        [Fact]
        public async Task ObtenerCaracteristica_Existe_RetornaOkConObjeto()
        {
            // --- ARRANGE ---
            int idBusqueda = 1;
            var caracteristicaDb = new Caracteristica { IdCaracteristica = 1, Nombre = "Gimnasio" };

            _repositoryMock
                .Setup(r => r.GetCaracteristicaById(idBusqueda))
                .ReturnsAsync(caracteristicaDb);

            // --- ACT ---
            var result = await _sut.ObtenerCaracteristica(idBusqueda);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(caracteristicaDb);
        }

        #endregion

        #region Pruebas de CrearCaracteristica (POST)

        [Fact]
        public async Task CrearCaracteristica_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            // Simular contexto sin usuario autenticado
            _sut.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            var nuevaCaracteristica = new Caracteristica { Nombre = "Balcón" };

            // --- ACT ---
            var result = await _sut.CrearCaracteristica(nuevaCaracteristica);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task CrearCaracteristica_ObjetoNulo_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.CrearCaracteristica(null!);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos de la característica no pueden ser nulos.");
        }

        [Fact]
        public async Task CrearCaracteristica_DatosValidos_RetornaOk()
        {
            // --- ARRANGE ---
            var nuevaCaracteristica = new Caracteristica { Nombre = "Ascensor" };

            _repositoryMock
                .Setup(r => r.PostCaracteristica(nuevaCaracteristica))
                .ReturnsAsync(true); // El repositorio retorna bool (Task<bool>)

            // --- ACT ---
            var result = await _sut.CrearCaracteristica(nuevaCaracteristica);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            // Comprobamos que el valor devuelto sea System.Boolean y sea true
            var resultadoBooleano = objectResult.Value.Should().BeOfType<bool>().Subject;
            resultadoBooleano.Should().BeTrue();
        }

        #endregion

        #region Pruebas de ActualizarCaracteristica (PUT)

        [Fact]
        public async Task ActualizarCaracteristica_IdInvalido_RetornaBadRequest()
        {
            // --- ARRANGE ---
            var caracteristicaInvalida = new Caracteristica { IdCaracteristica = 0, Nombre = "Test" };

            // --- ACT ---
            var result = await _sut.ActualizarCaracteristica(caracteristicaInvalida);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para actualizar o el ID no son válidos.");
        }

        [Fact]
        public async Task ActualizarCaracteristica_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            var caracteristica = new Caracteristica { IdCaracteristica = 5, Nombre = "Jacuzzi" };

            _repositoryMock
                .Setup(r => r.GetCaracteristicaById(5))
                .ReturnsAsync((Caracteristica?)null);

            // --- ACT ---
            var result = await _sut.ActualizarCaracteristica(caracteristica);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se puede actualizar. La característica con ID 5 no existe.");
        }

        [Fact]
        public async Task ActualizarCaracteristica_Valido_RetornaOk()
        {
            // --- ARRANGE ---
            var exist = new Caracteristica { IdCaracteristica = 5, Nombre = "Nombre Antiguo" };
            var aActualizar = new Caracteristica { IdCaracteristica = 5, Nombre = "Nombre Nuevo" };

            _repositoryMock
                .Setup(r => r.GetCaracteristicaById(5))
                .ReturnsAsync(exist);

            _repositoryMock
                .Setup(r => r.PutCaracteristica(It.IsAny<Caracteristica>()))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.ActualizarCaracteristica(aActualizar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            _repositoryMock.Verify(r => r.PutCaracteristica(It.Is<Caracteristica>(c => c.Nombre == "Nombre Nuevo")), Times.Once);
        }

        #endregion

        #region Pruebas de EliminarCaracteristica (DELETE)

        [Fact]
        public async Task EliminarCaracteristica_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            int idBorrar = 88;

            _repositoryMock
                .Setup(r => r.GetCaracteristicaById(idBorrar))
                .ReturnsAsync((Caracteristica?)null);

            // --- ACT ---
            var result = await _sut.EliminarCaracteristica(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be($"404: No se puede eliminar. La característica con ID {idBorrar} no existe.");
        }

        [Fact]
        public async Task EliminarCaracteristica_Existe_EliminaYRetornaOk()
        {
            // --- ARRANGE ---
            int idBorrar = 2;
            var caracteristicaExistente = new Caracteristica { IdCaracteristica = 2, Nombre = "Zonas Verdes" };

            _repositoryMock
                .Setup(r => r.GetCaracteristicaById(idBorrar))
                .ReturnsAsync(caracteristicaExistente);

            _repositoryMock
                .Setup(r => r.DeleteCaracteristica(caracteristicaExistente))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.EliminarCaracteristica(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            _repositoryMock.Verify(r => r.DeleteCaracteristica(caracteristicaExistente), Times.Once);
        }

        #endregion
    }
}