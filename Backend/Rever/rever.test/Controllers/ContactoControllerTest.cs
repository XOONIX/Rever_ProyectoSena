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
    public class ContactoControllerTest
    {
        private readonly Mock<IContactoRepository> _repositoryMock;
        private readonly ContactoController _sut; // System Under Test

        public ContactoControllerTest()
        {
            _repositoryMock = new Mock<IContactoRepository>();
            _sut = new ContactoController(_repositoryMock.Object);

            // Usuario administrador (id 1) por defecto
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(1, ControllerTestHelper.RolAdmin));
        }

        #region Pruebas de ListarContacto (GET)

        [Fact]
        public async Task ListarContacto_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);

            // --- ACT ---
            var result = await _sut.ListarContacto();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task ListarContacto_ExistenRegistros_RetornaOkConLista()
        {
            // --- ARRANGE ---
            var listaEsperada = new List<Contacto>
            {
                new Contacto { IdContacto = 1, Mensaje = "Hola, me interesa el inmueble" },
                new Contacto { IdContacto = 2, Mensaje = "¿Sigue disponible?" }
            };

            _repositoryMock
                .Setup(r => r.GetContacto())
                .ReturnsAsync(listaEsperada);

            // --- ACT ---
            var result = await _sut.ListarContacto();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(listaEsperada);
        }

        [Fact]
        public async Task ListarContacto_ListaNula_RetornaNotFound()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetContacto())
                .ReturnsAsync((List<Contacto>?)null);

            // --- ACT ---
            var result = await _sut.ListarContacto();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se encontraron contactos registrados.");
        }

        [Fact]
        public async Task ListarContacto_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetContacto())
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ListarContacto();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de ObtenerContacto (GET por ID)

        [Fact]
        public async Task ObtenerContacto_IdInvalido_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.ObtenerContacto(0);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: El ID proporcionado no es válido.");
        }

        [Fact]
        public async Task ObtenerContacto_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            int idBusqueda = 99;
            _repositoryMock
                .Setup(r => r.GetContactoById(idBusqueda))
                .ReturnsAsync((Contacto?)null);

            // --- ACT ---
            var result = await _sut.ObtenerContacto(idBusqueda);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be($"404: No se encontró un contacto con ID {idBusqueda}.");
        }

        [Fact]
        public async Task ObtenerContacto_TokenSinId_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuarioSinId());

            _repositoryMock
                .Setup(r => r.GetContactoById(1))
                .ReturnsAsync(new Contacto { IdContacto = 1, IdComprador = 5, IdVendedor = 6 });

            // --- ACT ---
            var result = await _sut.ObtenerContacto(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: No se pudo verificar la identidad desde el token.");
        }

        [Fact]
        public async Task ObtenerContacto_UsuarioAjenoNoAdmin_RetornaForbidden()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(99, ControllerTestHelper.RolUsuario));

            _repositoryMock
                .Setup(r => r.GetContactoById(1))
                .ReturnsAsync(new Contacto { IdContacto = 1, IdComprador = 5, IdVendedor = 6 });

            // --- ACT ---
            var result = await _sut.ObtenerContacto(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(403);
            objectResult.Value.Should().Be("403: No puedes ver un mensaje que no te involucra.");
        }

        [Fact]
        public async Task ObtenerContacto_UsuarioEsComprador_RetornaOkConObjeto()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(5, ControllerTestHelper.RolUsuario));
            var contactoDb = new Contacto { IdContacto = 1, IdComprador = 5, IdVendedor = 6 };

            _repositoryMock
                .Setup(r => r.GetContactoById(1))
                .ReturnsAsync(contactoDb);

            // --- ACT ---
            var result = await _sut.ObtenerContacto(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(contactoDb);
        }

        [Fact]
        public async Task ObtenerContacto_UsuarioEsVendedor_RetornaOkConObjeto()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(6, ControllerTestHelper.RolUsuario));
            var contactoDb = new Contacto { IdContacto = 1, IdComprador = 5, IdVendedor = 6 };

            _repositoryMock
                .Setup(r => r.GetContactoById(1))
                .ReturnsAsync(contactoDb);

            // --- ACT ---
            var result = await _sut.ObtenerContacto(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(contactoDb);
        }

        [Fact]
        public async Task ObtenerContacto_UsuarioAdminAjeno_RetornaOkConObjeto()
        {
            // --- ARRANGE --- (el admin por defecto, id 1, no es parte del mensaje)
            var contactoDb = new Contacto { IdContacto = 1, IdComprador = 5, IdVendedor = 6 };

            _repositoryMock
                .Setup(r => r.GetContactoById(1))
                .ReturnsAsync(contactoDb);

            // --- ACT ---
            var result = await _sut.ObtenerContacto(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(contactoDb);
        }

        [Fact]
        public async Task ObtenerContacto_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetContactoById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ObtenerContacto(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de CrearContacto (POST)

        [Fact]
        public async Task CrearContacto_ObjetoNulo_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.CrearContacto(null!);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos del contacto no pueden ser nulos.");
        }

        [Fact]
        public async Task CrearContacto_TokenSinId_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuarioSinId());
            var nuevoContacto = new Contacto { IdVendedor = 6, IdInmueble = 3, Mensaje = "Hola" };

            // --- ACT ---
            var result = await _sut.CrearContacto(nuevoContacto);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: No se pudo verificar la identidad desde el token.");
        }

        [Fact]
        public async Task CrearContacto_DatosValidos_AsignaCompradorDelTokenYRetornaOk()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(7, ControllerTestHelper.RolUsuario));
            var nuevoContacto = new Contacto { IdComprador = 999, IdVendedor = 6, IdInmueble = 3, Mensaje = "Me interesa" };

            _repositoryMock
                .Setup(r => r.PostContacto(It.IsAny<Contacto>()))
                .ReturnsAsync(true); // El repositorio retorna bool (Task<bool>)

            // --- ACT ---
            var result = await _sut.CrearContacto(nuevoContacto);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var resultadoBooleano = objectResult.Value.Should().BeOfType<bool>().Subject;
            resultadoBooleano.Should().BeTrue();

            // El remitente siempre es el usuario autenticado (se ignora el IdComprador enviado)
            _repositoryMock.Verify(r => r.PostContacto(It.Is<Contacto>(c =>
                c.IdComprador == 7 &&
                c.Fecha > DateTime.UtcNow.AddMinutes(-1))), Times.Once);
        }

        [Fact]
        public async Task CrearContacto_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.PostContacto(It.IsAny<Contacto>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.CrearContacto(new Contacto { Mensaje = "Hola" });

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de ActualizarContacto (PUT)

        [Fact]
        public async Task ActualizarContacto_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);

            // --- ACT ---
            var result = await _sut.ActualizarContacto(new Contacto { IdContacto = 1 });

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task ActualizarContacto_ObjetoNulo_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.ActualizarContacto(null!);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para actualizar o el ID no son válidos.");
        }

        [Fact]
        public async Task ActualizarContacto_IdInvalido_RetornaBadRequest()
        {
            // --- ARRANGE ---
            var contactoInvalido = new Contacto { IdContacto = 0, Mensaje = "Test" };

            // --- ACT ---
            var result = await _sut.ActualizarContacto(contactoInvalido);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para actualizar o el ID no son válidos.");
        }

        [Fact]
        public async Task ActualizarContacto_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            var contacto = new Contacto { IdContacto = 5, Mensaje = "Hola" };

            _repositoryMock
                .Setup(r => r.GetContactoById(5))
                .ReturnsAsync((Contacto?)null);

            // --- ACT ---
            var result = await _sut.ActualizarContacto(contacto);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se puede actualizar. El contacto con ID 5 no existe.");
        }

        [Fact]
        public async Task ActualizarContacto_Valido_RetornaOk()
        {
            // --- ARRANGE ---
            var exist = new Contacto { IdContacto = 5, IdComprador = 1, IdVendedor = 2, IdInmueble = 3, Mensaje = "Mensaje antiguo" };
            var aActualizar = new Contacto { IdContacto = 5, IdComprador = 1, IdVendedor = 2, IdInmueble = 3, Mensaje = "Mensaje nuevo" };

            _repositoryMock
                .Setup(r => r.GetContactoById(5))
                .ReturnsAsync(exist);

            _repositoryMock
                .Setup(r => r.PutContacto(It.IsAny<Contacto>()))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.ActualizarContacto(aActualizar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            _repositoryMock.Verify(r => r.PutContacto(It.Is<Contacto>(c => c.Mensaje == "Mensaje nuevo")), Times.Once);
        }

        [Fact]
        public async Task ActualizarContacto_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetContactoById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ActualizarContacto(new Contacto { IdContacto = 5 });

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de DeleteContacto (DELETE)

        [Fact]
        public async Task DeleteContacto_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);

            // --- ACT ---
            var result = await _sut.DeleteContacto(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task DeleteContacto_IdInvalido_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.DeleteContacto(0);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: El ID del contacto no es válido.");
        }

        [Fact]
        public async Task DeleteContacto_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            int idBorrar = 88;

            _repositoryMock
                .Setup(r => r.GetContactoById(idBorrar))
                .ReturnsAsync((Contacto?)null);

            // --- ACT ---
            var result = await _sut.DeleteContacto(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be($"404: No se puede eliminar. El contacto con ID {idBorrar} no existe.");
        }

        [Fact]
        public async Task DeleteContacto_Existe_EliminaYRetornaOk()
        {
            // --- ARRANGE ---
            int idBorrar = 2;
            var contactoExistente = new Contacto { IdContacto = 2, Mensaje = "Mensaje a borrar" };

            _repositoryMock
                .Setup(r => r.GetContactoById(idBorrar))
                .ReturnsAsync(contactoExistente);

            _repositoryMock
                .Setup(r => r.DeleteContacto(contactoExistente))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.DeleteContacto(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            _repositoryMock.Verify(r => r.DeleteContacto(contactoExistente), Times.Once);
        }

        [Fact]
        public async Task DeleteContacto_RepositorioLanzaExcepcion_RetornaError500Generico()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetContactoById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.DeleteContacto(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500: Error interno del servidor.");
        }

        #endregion
    }
}
