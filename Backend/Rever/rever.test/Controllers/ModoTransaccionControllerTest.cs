using System.Runtime.CompilerServices;
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
    // Este controlador no usa StatusCode(...) ni try/catch: devuelve Ok(), NotFound(), BadRequest(),
    // CreatedAtAction() y NoContent(), así que aquí se comprueba cada tipo de resultado concreto.
    public class ModoTransaccionControllerTest
    {
        private readonly Mock<IModoTransaccionRepository> _repositoryMock;
        private readonly ModoTransaccionController _sut; // System Under Test

        public ModoTransaccionControllerTest()
        {
            _repositoryMock = new Mock<IModoTransaccionRepository>();
            _sut = new ModoTransaccionController(_repositoryMock.Object);

            // Usuario administrador autenticado por defecto
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(1, ControllerTestHelper.RolAdmin));
        }

        // GetUninitializedObject evita problemas si el modelo tiene miembros 'required'
        private static ModoTransaccion CrearModo(int idModo)
        {
            var modo = (ModoTransaccion)RuntimeHelpers.GetUninitializedObject(typeof(ModoTransaccion));
            modo.IdModo = idModo;
            return modo;
        }

        #region Pruebas de GetAll (GET)

        [Fact]
        public async Task GetAll_ExistenRegistros_RetornaOkConLista()
        {
            // --- ARRANGE ---
            var listaEsperada = new List<ModoTransaccion> { CrearModo(1), CrearModo(2) };

            _repositoryMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(listaEsperada);

            // --- ACT ---
            var result = await _sut.GetAll();

            // --- ASSERT ---
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(listaEsperada);
        }

        #endregion

        #region Pruebas de GetById (GET por ID)

        [Fact]
        public async Task GetById_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetByIdAsync(99))
                .ReturnsAsync((ModoTransaccion?)null);

            // --- ACT ---
            var result = await _sut.GetById(99);

            // --- ASSERT ---
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetById_Existe_RetornaOkConObjeto()
        {
            // --- ARRANGE ---
            var modoDb = CrearModo(1);

            _repositoryMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(modoDb);

            // --- ACT ---
            var result = await _sut.GetById(1);

            // --- ASSERT ---
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(modoDb);
        }

        #endregion

        #region Pruebas de Create (POST)

        [Fact]
        public async Task Create_DatosValidos_RetornaCreatedAtAction()
        {
            // --- ARRANGE ---
            var nuevoModo = CrearModo(0);
            var modoCreado = CrearModo(3);

            _repositoryMock
                .Setup(r => r.CreateAsync(nuevoModo))
                .ReturnsAsync(modoCreado);

            // --- ACT ---
            var result = await _sut.Create(nuevoModo);

            // --- ASSERT ---
            var created = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            created.StatusCode.Should().Be(201);
            created.ActionName.Should().Be(nameof(ModoTransaccionController.GetById));
            created.RouteValues!["id"].Should().Be(3);
            created.Value.Should().BeSameAs(modoCreado);
        }

        #endregion

        #region Pruebas de Update (PUT)

        [Fact]
        public async Task Update_IdNoCoincideConElDelObjeto_RetornaBadRequest()
        {
            // --- ARRANGE ---
            var modo = CrearModo(2);

            // --- ACT ---
            var result = await _sut.Update(1, modo);

            // --- ASSERT ---
            result.Should().BeOfType<BadRequestResult>();
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<ModoTransaccion>()), Times.Never);
        }

        [Fact]
        public async Task Update_IdCoincide_RetornaOkConObjetoActualizado()
        {
            // --- ARRANGE ---
            var modo = CrearModo(2);
            var modoActualizado = CrearModo(2);

            _repositoryMock
                .Setup(r => r.UpdateAsync(modo))
                .ReturnsAsync(modoActualizado);

            // --- ACT ---
            var result = await _sut.Update(2, modo);

            // --- ASSERT ---
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeSameAs(modoActualizado);
            _repositoryMock.Verify(r => r.UpdateAsync(modo), Times.Once);
        }

        #endregion

        #region Pruebas de Delete (DELETE)

        [Fact]
        public async Task Delete_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.DeleteAsync(88))
                .ReturnsAsync(false);

            // --- ACT ---
            var result = await _sut.Delete(88);

            // --- ASSERT ---
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Delete_Existe_RetornaNoContent()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.DeleteAsync(2))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.Delete(2);

            // --- ASSERT ---
            result.Should().BeOfType<NoContentResult>();
            _repositoryMock.Verify(r => r.DeleteAsync(2), Times.Once);
        }

        #endregion
    }
}
