using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using rever.Controllers;
using rever.Dtos;
using rever.Models;
using rever.Repositories.Interfaces;
using rever.Tests.Helpers;
using System.Runtime.CompilerServices;
using TuProyecto.DTOs;
using Xunit;

namespace rever.Tests.Controllers
{
    public class InmuebleControllerTest
    {
        // OJO: el método privado EsDuenoOAdmin del controlador reconoce como admin los roles
        // "administrador" o "Admin" (NO el rol "1"). Por eso estos tests usan este rol para el admin.
        private const string RolAdminInmueble = "administrador";

        private readonly Mock<IInmuebleRepository> _repositoryMock;
        private readonly InmuebleController _sut; // System Under Test

        public InmuebleControllerTest()
        {
            _repositoryMock = new Mock<IInmuebleRepository>();
            _sut = new InmuebleController(_repositoryMock.Object);

            // Usuario NO admin con id 7 por defecto (dueño de los inmuebles de prueba)
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(7, ControllerTestHelper.RolUsuario));
        }

        #region Pruebas de ListarInmueble (GET listar)

        [Fact]
        public async Task ListarInmueble_ExistenRegistros_RetornaOkConLista()
        {
            // --- ARRANGE ---
            var listaEsperada = new List<Inmueble>
            {
                ControllerTestHelper.CrearInmueble(7, 1),
                ControllerTestHelper.CrearInmueble(8, 2)
            };

            _repositoryMock
                .Setup(r => r.GetInmueble())
                .ReturnsAsync(listaEsperada);

            // --- ACT ---
            var result = await _sut.ListarInmueble();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(listaEsperada);
        }

        [Fact]
        public async Task ListarInmueble_ListaNula_RetornaNotFound()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetInmueble())
                .ReturnsAsync((List<Inmueble>?)null);

            // --- ACT ---
            var result = await _sut.ListarInmueble();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se encontraron inmuebles registrados.");
        }

        [Fact]
        public async Task ListarInmueble_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetInmueble())
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ListarInmueble();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de ObtenerInmueble (GET por ID)

        [Fact]
        public async Task ObtenerInmueble_IdInvalido_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.ObtenerInmueble(0);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: El ID proporcionado no es válido.");
        }

        [Fact]
        public async Task ObtenerInmueble_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            int idBusqueda = 99;
            _repositoryMock
                .Setup(r => r.GetInmuebleById(idBusqueda))
                .ReturnsAsync((Inmueble?)null);

            // --- ACT ---
            var result = await _sut.ObtenerInmueble(idBusqueda);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be($"404: No se encontró el inmueble con ID {idBusqueda}.");
        }

        [Fact]
        public async Task ObtenerInmueble_Existe_RetornaOkConObjeto()
        {
            // --- ARRANGE ---
            int idBusqueda = 1;
            var inmuebleDb = ControllerTestHelper.CrearInmueble(7, idBusqueda);

            _repositoryMock
                .Setup(r => r.GetInmuebleById(idBusqueda))
                .ReturnsAsync(inmuebleDb);

            // --- ACT ---
            var result = await _sut.ObtenerInmueble(idBusqueda);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(inmuebleDb);
        }

        [Fact]
        public async Task ObtenerInmueble_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetInmuebleById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ObtenerInmueble(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de ObtenerDetalleInmueble (GET {id}/Detalle)

        [Fact]
        public async Task ObtenerDetalleInmueble_IdInvalido_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.ObtenerDetalleInmueble(0);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: El ID proporcionado no es válido.");
        }

        [Fact]
        public async Task ObtenerDetalleInmueble_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            // Sin Setup, Moq devuelve null para GetDetalleAsync (el DTO es una clase).
            // --- ACT ---
            var result = await _sut.ObtenerDetalleInmueble(99);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se encontró el inmueble con ID 99.");
        }

        #endregion

        #region Pruebas de CrearInmueble (POST)

        [Fact]
        public async Task CrearInmueble_ObjetoNulo_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.CrearInmueble(null!);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos del inmueble no pueden ser nulos.");
        }

        [Fact]
        public async Task CrearInmueble_TokenSinId_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuarioSinId());
            var nuevoInmueble = ControllerTestHelper.CrearInmueble(0, 0);

            // --- ACT ---
            var result = await _sut.CrearInmueble(nuevoInmueble);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: No se pudo verificar la identidad desde el token.");
        }

        [Fact]
        public async Task CrearInmueble_DatosValidos_AsignaDuenoDelTokenYRetornaOk()
        {
            // --- ARRANGE --- (el IdUsuario enviado en el body se ignora)
            var nuevoInmueble = ControllerTestHelper.CrearInmueble(999, 0);

            _repositoryMock
                .Setup(r => r.PostInmueble(It.IsAny<Inmueble>()))
                .ReturnsAsync(true); // El repositorio retorna bool (Task<bool>)

            // --- ACT ---
            var result = await _sut.CrearInmueble(nuevoInmueble);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var resultadoBooleano = objectResult.Value.Should().BeOfType<bool>().Subject;
            resultadoBooleano.Should().BeTrue();

            _repositoryMock.Verify(r => r.PostInmueble(It.Is<Inmueble>(i => i.IdUsuario == 7)), Times.Once);
        }

        [Fact]
        public async Task CrearInmueble_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.PostInmueble(It.IsAny<Inmueble>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.CrearInmueble(ControllerTestHelper.CrearInmueble(7, 0));

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de ActualizarInmueble (PUT)

        [Fact]
        public async Task ActualizarInmueble_ObjetoNulo_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.ActualizarInmueble(null!);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para actualizar o el ID no son válidos.");
        }

        [Fact]
        public async Task ActualizarInmueble_IdInvalido_RetornaBadRequest()
        {
            // --- ARRANGE ---
            var inmuebleInvalido = ControllerTestHelper.CrearInmueble(7, 0);

            // --- ACT ---
            var result = await _sut.ActualizarInmueble(inmuebleInvalido);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para actualizar o el ID no son válidos.");
        }

        [Fact]
        public async Task ActualizarInmueble_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            var inmueble = ControllerTestHelper.CrearInmueble(7, 5);

            _repositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync((Inmueble?)null);

            // --- ACT ---
            var result = await _sut.ActualizarInmueble(inmueble);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se puede actualizar. El inmueble con ID 5 no existe.");
        }

        [Fact]
        public async Task ActualizarInmueble_InmuebleDeOtroUsuario_RetornaForbidden()
        {
            // --- ARRANGE --- (el usuario es el 7, el dueño es el 50)
            var inmueble = ControllerTestHelper.CrearInmueble(50, 5);

            _repositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync(ControllerTestHelper.CrearInmueble(50, 5));

            // --- ACT ---
            var result = await _sut.ActualizarInmueble(inmueble);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(403);
            objectResult.Value.Should().Be("403: No puedes modificar un inmueble que no te pertenece.");
            _repositoryMock.Verify(r => r.PutInmueble(It.IsAny<Inmueble>()), Times.Never);
        }

        [Fact]
        public async Task ActualizarInmueble_UsuarioEsDueno_ActualizaYRetornaOk()
        {
            // --- ARRANGE ---
            var exist = ControllerTestHelper.CrearInmueble(7, 5);
            var aActualizar = ControllerTestHelper.CrearInmueble(99, 5); // intenta "regalar" el inmueble al usuario 99
            aActualizar.Titulo = "Título nuevo";

            _repositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync(exist);

            _repositoryMock
                .Setup(r => r.PutInmueble(It.IsAny<Inmueble>()))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.ActualizarInmueble(aActualizar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            // Se actualiza el título pero el dueño NO cambia
            _repositoryMock.Verify(r => r.PutInmueble(It.Is<Inmueble>(i =>
                i.Titulo == "Título nuevo" &&
                i.IdUsuario == 7)), Times.Once);
        }

        [Fact]
        public async Task ActualizarInmueble_AdminInmuebleAjeno_RetornaOk()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(1, RolAdminInmueble));
            var exist = ControllerTestHelper.CrearInmueble(50, 5);

            _repositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync(exist);

            _repositoryMock
                .Setup(r => r.PutInmueble(It.IsAny<Inmueble>()))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.ActualizarInmueble(ControllerTestHelper.CrearInmueble(50, 5));

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task ActualizarInmueble_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetInmuebleById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ActualizarInmueble(ControllerTestHelper.CrearInmueble(7, 5));

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de EliminarInmueble (DELETE)

        [Fact]
        public async Task EliminarInmueble_IdInvalido_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.EliminarInmueble(0);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para eliminar o el ID no son válidos.");
        }

        [Fact]
        public async Task EliminarInmueble_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            int idBorrar = 88;

            _repositoryMock
                .Setup(r => r.GetInmuebleById(idBorrar))
                .ReturnsAsync((Inmueble?)null);

            // --- ACT ---
            var result = await _sut.EliminarInmueble(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be($"404: No se puede eliminar. El inmueble con ID {idBorrar} no existe.");
        }

        [Fact]
        public async Task EliminarInmueble_InmuebleDeOtroUsuario_RetornaForbidden()
        {
            // --- ARRANGE ---
            int idBorrar = 2;

            _repositoryMock
                .Setup(r => r.GetInmuebleById(idBorrar))
                .ReturnsAsync(ControllerTestHelper.CrearInmueble(50, idBorrar));

            // --- ACT ---
            var result = await _sut.EliminarInmueble(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(403);
            objectResult.Value.Should().Be("403: No puedes eliminar un inmueble que no te pertenece.");
            _repositoryMock.Verify(r => r.DeleteInmueble(It.IsAny<Inmueble>()), Times.Never);
        }

        [Fact]
        public async Task EliminarInmueble_UsuarioEsDueno_EliminaYRetornaOk()
        {
            // --- ARRANGE ---
            int idBorrar = 2;
            var inmuebleExistente = ControllerTestHelper.CrearInmueble(7, idBorrar);

            _repositoryMock
                .Setup(r => r.GetInmuebleById(idBorrar))
                .ReturnsAsync(inmuebleExistente);

            _repositoryMock
                .Setup(r => r.DeleteInmueble(inmuebleExistente))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.EliminarInmueble(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            _repositoryMock.Verify(r => r.DeleteInmueble(inmuebleExistente), Times.Once);
        }

        [Fact]
        public async Task EliminarInmueble_AdminInmuebleAjeno_EliminaYRetornaOk()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(1, RolAdminInmueble));
            int idBorrar = 2;
            var inmuebleExistente = ControllerTestHelper.CrearInmueble(50, idBorrar);

            _repositoryMock
                .Setup(r => r.GetInmuebleById(idBorrar))
                .ReturnsAsync(inmuebleExistente);

            _repositoryMock
                .Setup(r => r.DeleteInmueble(inmuebleExistente))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.EliminarInmueble(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task EliminarInmueble_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetInmuebleById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.EliminarInmueble(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de GetAll (GET)

        [Fact]
        public async Task GetAll_RetornaOk()
        {
            // --- ACT ---
            // Sin Setup, Moq devuelve un valor por defecto para GetListadoAsync; solo se comprueba el 200.
            var result = await _sut.GetAll();

            // --- ASSERT ---
            // Ok(...) devuelve OkObjectResult (no ObjectResult "puro")
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
        }

        #endregion

        #region Pruebas de CrearInmuebleCompleto (POST completo)

        // CrearInmuebleDto puede tener miembros 'required'; GetUninitializedObject evita llenarlos todos.
        private static CrearInmuebleDto CrearDto()
        {
            return (CrearInmuebleDto)RuntimeHelpers.GetUninitializedObject(typeof(CrearInmuebleDto));
        }

        [Fact]
        public async Task CrearInmuebleCompleto_TokenSinId_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuarioSinId());

            // --- ACT ---
            var result = await _sut.CrearInmuebleCompleto(CrearDto());

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: No se pudo verificar la identidad desde el token.");
        }

        [Fact]
        public async Task CrearInmuebleCompleto_DatosValidos_RetornaCreated()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.PostInmuebleCompleto(It.IsAny<CrearInmuebleDto>(), 7))
                .ReturnsAsync(ControllerTestHelper.CrearInmueble(7, 15));

            // --- ACT ---
            var result = await _sut.CrearInmuebleCompleto(CrearDto());

            // --- ASSERT ---
            var created = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            created.StatusCode.Should().Be(201);
            created.ActionName.Should().Be(nameof(InmuebleController.ObtenerInmueble));
            created.RouteValues!["id"].Should().Be(15);
        }

        [Fact]
        public async Task CrearInmuebleCompleto_ArgumentException_RetornaBadRequest()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.PostInmuebleCompleto(It.IsAny<CrearInmuebleDto>(), It.IsAny<int>()))
                .ThrowsAsync(new ArgumentException("Dato inválido"));

            // --- ACT ---
            var result = await _sut.CrearInmuebleCompleto(CrearDto());

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Dato inválido");
        }

        [Fact]
        public async Task CrearInmuebleCompleto_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.PostInmuebleCompleto(It.IsAny<CrearInmuebleDto>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.CrearInmuebleCompleto(CrearDto());

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion
    }
}
