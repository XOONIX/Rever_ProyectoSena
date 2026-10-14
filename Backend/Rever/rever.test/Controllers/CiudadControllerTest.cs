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
    public class CiudadControllerTest
    {
        private readonly Mock<ICiudadRepository> _repositoryMock;
        private readonly CiudadController _sut; // System Under Test

        public CiudadControllerTest()
        {
            // 1. Instanciar el Mock de la interfaz del repositorio
            _repositoryMock = new Mock<ICiudadRepository>();

            // 2. Crear la instancia del controlador pasando el mock
            _sut = new CiudadController(_repositoryMock.Object);

            // 3. Simular un usuario autenticado para los métodos POST, PUT y DELETE
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

        #region Pruebas de ListarCiudad (GET)

        [Fact]
        public async Task ListarCiudad_ExistenRegistros_RetornaOkConLista()
        {
            // --- ARRANGE ---
            var listaEsperada = new List<Ciudad>
            {
                new Ciudad { IdCiudad = 1, Nombre = "Bogotá" },
                new Ciudad { IdCiudad = 2, Nombre = "Medellín" }
            };

            _repositoryMock
                .Setup(r => r.GetCiudad())
                .ReturnsAsync(listaEsperada);

            // --- ACT ---
            var result = await _sut.ListarCiudad();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(listaEsperada);
        }

        [Fact]
        public async Task ListarCiudad_ListaNula_RetornaNotFound()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetCiudad())
                .ReturnsAsync((List<Ciudad>?)null);

            // --- ACT ---
            var result = await _sut.ListarCiudad();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se encontraron ciudades registradas.");
        }

        #endregion

        #region Pruebas de ObtenerCiudad (GET por ID)

        [Fact]
        public async Task ObtenerCiudad_IdInvalido_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.ObtenerCiudad(0);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: El ID proporcionado no es válido.");
        }

        [Fact]
        public async Task ObtenerCiudad_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            int idBusqueda = 99;
            _repositoryMock
                .Setup(r => r.GetCiudadById(idBusqueda))
                .ReturnsAsync((Ciudad?)null);

            // --- ACT ---
            var result = await _sut.ObtenerCiudad(idBusqueda);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be($"404: No se encontró la ciudad con ID {idBusqueda}.");
        }

        [Fact]
        public async Task ObtenerCiudad_Existe_RetornaOkConObjeto()
        {
            // --- ARRANGE ---
            int idBusqueda = 1;
            var ciudadDb = new Ciudad { IdCiudad = 1, Nombre = "Cali" };

            _repositoryMock
                .Setup(r => r.GetCiudadById(idBusqueda))
                .ReturnsAsync(ciudadDb);

            // --- ACT ---
            var result = await _sut.ObtenerCiudad(idBusqueda);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(ciudadDb);
        }

        #endregion

        #region Pruebas de CrearCiudad (POST)

        [Fact]
        public async Task CrearCiudad_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            _sut.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext() // Sin usuario autenticado
            };

            var nuevaCiudad = new Ciudad { Nombre = "Bucaramanga" };

            // --- ACT ---
            var result = await _sut.CrearCiudad(nuevaCiudad);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task CrearCiudad_ObjetoNulo_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.CrearCiudad(null!);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos de la ciudad no pueden ser nulos.");
        }

        [Fact]
        public async Task CrearCiudad_DatosValidos_RetornaOk()
        {
            // --- ARRANGE ---
            var nuevaCiudad = new Ciudad { Nombre = "Pereira" };

            // Caso 1: Si tu PostCiudad retorna bool (Task<bool>)
            _repositoryMock
                .Setup(r => r.PostCiudad(nuevaCiudad))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.CrearCiudad(nuevaCiudad);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var esExitoso = objectResult.Value.Should().BeOfType<bool>().Subject;
            esExitoso.Should().BeTrue();
        }

        #endregion

        #region Pruebas de ActualizarCiudad (PUT)

        [Fact]
        public async Task ActualizarCiudad_IdInvalido_RetornaBadRequest()
        {
            // --- ARRANGE ---
            var ciudadInvalida = new Ciudad { IdCiudad = 0, Nombre = "Test" };

            // --- ACT ---
            var result = await _sut.ActualizarCiudad(ciudadInvalida);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para actualizar o el ID no son válidos.");
        }

        [Fact]
        public async Task ActualizarCiudad_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            var ciudad = new Ciudad { IdCiudad = 5, Nombre = "Manizales" };

            _repositoryMock
                .Setup(r => r.GetCiudadById(5))
                .ReturnsAsync((Ciudad?)null);

            // --- ACT ---
            var result = await _sut.ActualizarCiudad(ciudad);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se puede actualizar. La ciudad con ID 5 no existe.");
        }

        [Fact]
        public async Task ActualizarCiudad_Valido_RetornaOk()
        {
            // --- ARRANGE ---
            var exist = new Ciudad { IdCiudad = 5, Nombre = "Nombre Antiguo" };
            var aActualizar = new Ciudad { IdCiudad = 5, Nombre = "Nombre Nuevo" };

            _repositoryMock
                .Setup(r => r.GetCiudadById(5))
                .ReturnsAsync(exist);

            _repositoryMock
                .Setup(r => r.PutCiudad(It.IsAny<Ciudad>()))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.ActualizarCiudad(aActualizar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            _repositoryMock.Verify(r => r.PutCiudad(It.Is<Ciudad>(c => c.Nombre == "Nombre Nuevo")), Times.Once);
        }

        #endregion

        #region Pruebas de EliminarCiudad (DELETE)

        [Fact]
        public async Task EliminarCiudad_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            int idBorrar = 88;

            _repositoryMock
                .Setup(r => r.GetCiudadById(idBorrar))
                .ReturnsAsync((Ciudad?)null);

            // --- ACT ---
            var result = await _sut.EliminarCiudad(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
        }
        
        #endregion
    }
}