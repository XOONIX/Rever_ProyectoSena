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
    public class LocalidadControllerTest
    {
        private readonly Mock<ILocalidadRepository> _repositoryMock;
        private readonly LocalidadController _sut; // System Under Test

        public LocalidadControllerTest()
        {
            _repositoryMock = new Mock<ILocalidadRepository>();
            _sut = new LocalidadController(_repositoryMock.Object);

            // Usuario administrador autenticado por defecto para POST, PUT y DELETE
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(1, ControllerTestHelper.RolAdmin));
        }

        #region Pruebas de ListarLocalidad (GET)

        [Fact]
        public async Task ListarLocalidad_ExistenRegistros_RetornaOkConLista()
        {
            // --- ARRANGE ---
            var listaEsperada = new List<Localidad>
            {
                new Localidad { IdLocalidad = 1, Nombre = "Chapinero" },
                new Localidad { IdLocalidad = 2, Nombre = "Usaquén" }
            };

            _repositoryMock
                .Setup(r => r.GetLocalidad())
                .ReturnsAsync(listaEsperada);

            // --- ACT ---
            var result = await _sut.ListarLocalidad();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(listaEsperada);
        }

        [Fact]
        public async Task ListarLocalidad_ListaNula_RetornaNotFound()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetLocalidad())
                .ReturnsAsync((List<Localidad>?)null);

            // --- ACT ---
            var result = await _sut.ListarLocalidad();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se encontraron localidades registradas.");
        }

        [Fact]
        public async Task ListarLocalidad_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetLocalidad())
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ListarLocalidad();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de ObtenerLocalidad (GET por ID)

        [Fact]
        public async Task ObtenerLocalidad_IdInvalido_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.ObtenerLocalidad(0);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: El ID proporcionado no es válido.");
        }

        [Fact]
        public async Task ObtenerLocalidad_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            int idBusqueda = 99;
            _repositoryMock
                .Setup(r => r.GetLocalidadById(idBusqueda))
                .ReturnsAsync((Localidad?)null);

            // --- ACT ---
            var result = await _sut.ObtenerLocalidad(idBusqueda);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be($"404: No se encontró la localidad con ID {idBusqueda}.");
        }

        [Fact]
        public async Task ObtenerLocalidad_Existe_RetornaOkConObjeto()
        {
            // --- ARRANGE ---
            int idBusqueda = 1;
            var localidadDb = new Localidad { IdLocalidad = 1, Nombre = "Chapinero" };

            _repositoryMock
                .Setup(r => r.GetLocalidadById(idBusqueda))
                .ReturnsAsync(localidadDb);

            // --- ACT ---
            var result = await _sut.ObtenerLocalidad(idBusqueda);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(localidadDb);
        }

        [Fact]
        public async Task ObtenerLocalidad_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetLocalidadById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ObtenerLocalidad(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de CrearLocalidad (POST)

        [Fact]
        public async Task CrearLocalidad_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);
            var nuevaLocalidad = new Localidad { Nombre = "Suba" };

            // --- ACT ---
            var result = await _sut.CrearLocalidad(nuevaLocalidad);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task CrearLocalidad_ObjetoNulo_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.CrearLocalidad(null!);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos de la localidad no pueden ser nulos.");
        }

        [Fact]
        public async Task CrearLocalidad_DatosValidos_RetornaOk()
        {
            // --- ARRANGE ---
            var nuevaLocalidad = new Localidad { Nombre = "Suba" };

            _repositoryMock
                .Setup(r => r.PostLocalidad(nuevaLocalidad))
                .ReturnsAsync(true); // El repositorio retorna bool (Task<bool>)

            // --- ACT ---
            var result = await _sut.CrearLocalidad(nuevaLocalidad);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var resultadoBooleano = objectResult.Value.Should().BeOfType<bool>().Subject;
            resultadoBooleano.Should().BeTrue();
        }

        [Fact]
        public async Task CrearLocalidad_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.PostLocalidad(It.IsAny<Localidad>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.CrearLocalidad(new Localidad { Nombre = "Suba" });

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de ActualizarLocalidad (PUT)

        [Fact]
        public async Task ActualizarLocalidad_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);

            // --- ACT ---
            var result = await _sut.ActualizarLocalidad(new Localidad { IdLocalidad = 1, Nombre = "Test" });

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task ActualizarLocalidad_ObjetoNulo_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.ActualizarLocalidad(null!);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para actualizar o el ID no son válidos.");
        }

        [Fact]
        public async Task ActualizarLocalidad_IdInvalido_RetornaBadRequest()
        {
            // --- ARRANGE ---
            var localidadInvalida = new Localidad { IdLocalidad = 0, Nombre = "Test" };

            // --- ACT ---
            var result = await _sut.ActualizarLocalidad(localidadInvalida);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para actualizar o el ID no son válidos.");
        }

        [Fact]
        public async Task ActualizarLocalidad_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            var localidad = new Localidad { IdLocalidad = 5, Nombre = "Kennedy" };

            _repositoryMock
                .Setup(r => r.GetLocalidadById(5))
                .ReturnsAsync((Localidad?)null);

            // --- ACT ---
            var result = await _sut.ActualizarLocalidad(localidad);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se puede actualizar. La localidad con ID 5 no existe.");
        }

        [Fact]
        public async Task ActualizarLocalidad_Valido_RetornaOk()
        {
            // --- ARRANGE ---
            var exist = new Localidad { IdLocalidad = 5, Nombre = "Nombre Antiguo" };
            var aActualizar = new Localidad { IdLocalidad = 5, Nombre = "Nombre Nuevo" };

            _repositoryMock
                .Setup(r => r.GetLocalidadById(5))
                .ReturnsAsync(exist);

            _repositoryMock
                .Setup(r => r.PutLocalidad(It.IsAny<Localidad>()))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.ActualizarLocalidad(aActualizar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            _repositoryMock.Verify(r => r.PutLocalidad(It.Is<Localidad>(l => l.Nombre == "Nombre Nuevo")), Times.Once);
        }

        [Fact]
        public async Task ActualizarLocalidad_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetLocalidadById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ActualizarLocalidad(new Localidad { IdLocalidad = 5, Nombre = "X" });

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de EliminarLocalidad (DELETE)

        [Fact]
        public async Task EliminarLocalidad_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);

            // --- ACT ---
            var result = await _sut.EliminarLocalidad(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task EliminarLocalidad_IdInvalido_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.EliminarLocalidad(0);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para eliminar o el ID no son válidos.");
        }

        [Fact]
        public async Task EliminarLocalidad_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            int idBorrar = 88;

            _repositoryMock
                .Setup(r => r.GetLocalidadById(idBorrar))
                .ReturnsAsync((Localidad?)null);

            // --- ACT ---
            var result = await _sut.EliminarLocalidad(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be($"404: No se puede eliminar. La localidad con ID {idBorrar} no existe.");
        }

        [Fact]
        public async Task EliminarLocalidad_Existe_EliminaYRetornaOk()
        {
            // --- ARRANGE ---
            int idBorrar = 2;
            var localidadExistente = new Localidad { IdLocalidad = 2, Nombre = "Usaquén" };

            _repositoryMock
                .Setup(r => r.GetLocalidadById(idBorrar))
                .ReturnsAsync(localidadExistente);

            _repositoryMock
                .Setup(r => r.DeleteLocalidad(localidadExistente))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.EliminarLocalidad(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            _repositoryMock.Verify(r => r.DeleteLocalidad(localidadExistente), Times.Once);
        }

        [Fact]
        public async Task EliminarLocalidad_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetLocalidadById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.EliminarLocalidad(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion
    }
}
