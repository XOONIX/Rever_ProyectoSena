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
    public class ImagenControllerTest
    {
        private readonly Mock<IImagenRepository> _imagenRepositoryMock;
        private readonly Mock<IInmuebleRepository> _inmuebleRepositoryMock;
        private readonly ImagenController _sut; // System Under Test

        public ImagenControllerTest()
        {
            // 1. Mocks de las dos dependencias
            _imagenRepositoryMock = new Mock<IImagenRepository>();
            _inmuebleRepositoryMock = new Mock<IInmuebleRepository>();

            // 2. Controlador con los repositorios simulados
            _sut = new ImagenController(_imagenRepositoryMock.Object, _inmuebleRepositoryMock.Object);

            // 3. Usuario NO admin con id 7 por defecto (dueño de los inmuebles de prueba)
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(7, ControllerTestHelper.RolUsuario));
        }

        // Inmueble tiene miembros 'required'; este método los llena todos y solo
        // deja variable lo que importa en los tests: el dueño (IdUsuario).
        private static Inmueble CrearInmueble(int idUsuario)
        {
            return new Inmueble
            {
                IdUsuario = idUsuario,
                Titulo = "Inmueble de prueba",
                Descripcion = "Descripción de prueba",
                Direccion = "Calle 1 # 2-3",
                IdLocalidad = 1,
                Habitaciones = 1,
                Baños = 1,
                MetrosCuadrados = 50,
                Latitud = 0,
                Longitud = 0
            };
        }

        #region Pruebas de ListarImagen (GET)

        [Fact]
        public async Task ListarImagen_ExistenRegistros_RetornaOkConLista()
        {
            // --- ARRANGE ---
            var listaEsperada = new List<Imagen>
            {
                new Imagen { IdImagen = 1, Url = "https://img.test/1.jpg", IdInmueble = 1 },
                new Imagen { IdImagen = 2, Url = "https://img.test/2.jpg", IdInmueble = 1 }
            };

            _imagenRepositoryMock
                .Setup(r => r.GetImagen())
                .ReturnsAsync(listaEsperada);

            // --- ACT ---
            var result = await _sut.ListarImagen();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(listaEsperada);
        }

        [Fact]
        public async Task ListarImagen_ListaNula_RetornaNotFound()
        {
            // --- ARRANGE ---
            _imagenRepositoryMock
                .Setup(r => r.GetImagen())
                .ReturnsAsync((List<Imagen>?)null);

            // --- ACT ---
            var result = await _sut.ListarImagen();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se encontraron imágenes registradas.");
        }

        [Fact]
        public async Task ListarImagen_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _imagenRepositoryMock
                .Setup(r => r.GetImagen())
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ListarImagen();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de ObtenerImagen (GET por ID)

        [Fact]
        public async Task ObtenerImagen_IdInvalido_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.ObtenerImagen(0);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: El ID proporcionado no es válido.");
        }

        [Fact]
        public async Task ObtenerImagen_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            int idBusqueda = 99;
            _imagenRepositoryMock
                .Setup(r => r.GetImagenById(idBusqueda))
                .ReturnsAsync((Imagen?)null);

            // --- ACT ---
            var result = await _sut.ObtenerImagen(idBusqueda);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be($"404: No se encontró la imagen con ID {idBusqueda}.");
        }

        [Fact]
        public async Task ObtenerImagen_Existe_RetornaOkConObjeto()
        {
            // --- ARRANGE ---
            int idBusqueda = 1;
            var imagenDb = new Imagen { IdImagen = 1, Url = "https://img.test/1.jpg", IdInmueble = 1 };

            _imagenRepositoryMock
                .Setup(r => r.GetImagenById(idBusqueda))
                .ReturnsAsync(imagenDb);

            // --- ACT ---
            var result = await _sut.ObtenerImagen(idBusqueda);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(imagenDb);
        }

        #endregion

        #region Pruebas de CrearImagen (POST)

        [Fact]
        public async Task CrearImagen_ObjetoNulo_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.CrearImagen(null!);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos de la imagen no pueden ser nulos.");
        }

        [Fact]
        public async Task CrearImagen_IdInmuebleInvalido_RetornaBadRequest()
        {
            // --- ARRANGE ---
            var imagen = new Imagen { Url = "https://img.test/x.jpg", IdInmueble = 0 };

            // --- ACT ---
            var result = await _sut.CrearImagen(imagen);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos de la imagen no pueden ser nulos.");
        }

        [Fact]
        public async Task CrearImagen_InmuebleNoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            var imagen = new Imagen { Url = "https://img.test/x.jpg", IdInmueble = 5 };

            _inmuebleRepositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync((Inmueble?)null);

            // --- ACT ---
            var result = await _sut.CrearImagen(imagen);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: El inmueble con ID 5 no existe.");
        }

        [Fact]
        public async Task CrearImagen_InmuebleDeOtroUsuario_RetornaForbidden()
        {
            // --- ARRANGE --- (el usuario es el 7, el dueño es el 50)
            var imagen = new Imagen { Url = "https://img.test/x.jpg", IdInmueble = 5 };

            _inmuebleRepositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync(CrearInmueble(50));

            // --- ACT ---
            var result = await _sut.CrearImagen(imagen);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(403);
            objectResult.Value.Should().Be("403: No puedes agregar imágenes a un inmueble que no es tuyo.");
            _imagenRepositoryMock.Verify(r => r.PostImagen(It.IsAny<Imagen>()), Times.Never);
        }

        [Fact]
        public async Task CrearImagen_UsuarioEsDueno_RetornaOk()
        {
            // --- ARRANGE ---
            var imagen = new Imagen { Url = "https://img.test/x.jpg", IdInmueble = 5 };

            _inmuebleRepositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync(CrearInmueble(7));

            _imagenRepositoryMock
                .Setup(r => r.PostImagen(imagen))
                .ReturnsAsync(true); // El repositorio retorna bool (Task<bool>)

            // --- ACT ---
            var result = await _sut.CrearImagen(imagen);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var resultadoBooleano = objectResult.Value.Should().BeOfType<bool>().Subject;
            resultadoBooleano.Should().BeTrue();
        }

        [Fact]
        public async Task CrearImagen_UsuarioAdminInmuebleAjeno_RetornaOk()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(1, ControllerTestHelper.RolAdmin));
            var imagen = new Imagen { Url = "https://img.test/x.jpg", IdInmueble = 5 };

            _inmuebleRepositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync(CrearInmueble(50));

            _imagenRepositoryMock
                .Setup(r => r.PostImagen(imagen))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.CrearImagen(imagen);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task CrearImagen_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _inmuebleRepositoryMock
                .Setup(r => r.GetInmuebleById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.CrearImagen(new Imagen { Url = "x", IdInmueble = 5 });

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500 Error Interno: Fallo de BD");
        }

        #endregion

        #region Pruebas de ActualizarImagen (PUT)

        [Fact]
        public async Task ActualizarImagen_ObjetoNulo_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.ActualizarImagen(null!);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para actualizar o el ID no son válidos.");
        }

        [Fact]
        public async Task ActualizarImagen_IdInvalido_RetornaBadRequest()
        {
            // --- ARRANGE ---
            var imagenInvalida = new Imagen { IdImagen = 0, Url = "x", IdInmueble = 1 };

            // --- ACT ---
            var result = await _sut.ActualizarImagen(imagenInvalida);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para actualizar o el ID no son válidos.");
        }

        [Fact]
        public async Task ActualizarImagen_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            var imagen = new Imagen { IdImagen = 3, Url = "x", IdInmueble = 5 };

            _imagenRepositoryMock
                .Setup(r => r.GetImagenById(3))
                .ReturnsAsync((Imagen?)null);

            // --- ACT ---
            var result = await _sut.ActualizarImagen(imagen);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se puede actualizar. La imagen con ID 3 no existe.");
        }

        [Fact]
        public async Task ActualizarImagen_InmuebleNoExiste_RetornaForbidden()
        {
            // --- ARRANGE ---
            var exist = new Imagen { IdImagen = 3, Url = "a.jpg", IdInmueble = 5 };
            var imagen = new Imagen { IdImagen = 3, Url = "b.jpg", IdInmueble = 5 };

            _imagenRepositoryMock
                .Setup(r => r.GetImagenById(3))
                .ReturnsAsync(exist);

            _inmuebleRepositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync((Inmueble?)null);

            // --- ACT ---
            var result = await _sut.ActualizarImagen(imagen);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(403);
            objectResult.Value.Should().Be("403: No puedes modificar imágenes de un inmueble que no es tuyo.");
        }

        [Fact]
        public async Task ActualizarImagen_InmuebleDeOtroUsuario_RetornaForbidden()
        {
            // --- ARRANGE ---
            var exist = new Imagen { IdImagen = 3, Url = "a.jpg", IdInmueble = 5 };
            var imagen = new Imagen { IdImagen = 3, Url = "b.jpg", IdInmueble = 5 };

            _imagenRepositoryMock
                .Setup(r => r.GetImagenById(3))
                .ReturnsAsync(exist);

            _inmuebleRepositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync(CrearInmueble(50));

            // --- ACT ---
            var result = await _sut.ActualizarImagen(imagen);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(403);
            objectResult.Value.Should().Be("403: No puedes modificar imágenes de un inmueble que no es tuyo.");
            _imagenRepositoryMock.Verify(r => r.PutImagen(It.IsAny<Imagen>()), Times.Never);
        }

        [Fact]
        public async Task ActualizarImagen_Valido_RetornaOk()
        {
            // --- ARRANGE ---
            var exist = new Imagen { IdImagen = 3, Url = "a.jpg", IdInmueble = 5 };
            var aActualizar = new Imagen { IdImagen = 3, Url = "b.jpg", IdInmueble = 5 };

            _imagenRepositoryMock
                .Setup(r => r.GetImagenById(3))
                .ReturnsAsync(exist);

            _inmuebleRepositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync(CrearInmueble(7));

            _imagenRepositoryMock
                .Setup(r => r.PutImagen(It.IsAny<Imagen>()))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.ActualizarImagen(aActualizar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            _imagenRepositoryMock.Verify(r => r.PutImagen(It.Is<Imagen>(i => i.Url == "b.jpg")), Times.Once);
        }

        #endregion

        #region Pruebas de DeleteImagen (DELETE)

        [Fact]
        public async Task DeleteImagen_IdInvalido_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.DeleteImagen(0);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: El ID de la imagen no es válido.");
        }

        [Fact]
        public async Task DeleteImagen_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            int idBorrar = 88;

            _imagenRepositoryMock
                .Setup(r => r.GetImagenById(idBorrar))
                .ReturnsAsync((Imagen?)null);

            // --- ACT ---
            var result = await _sut.DeleteImagen(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be($"404: No se puede eliminar. La imagen con ID {idBorrar} no existe.");
        }

        [Fact]
        public async Task DeleteImagen_InmuebleDeOtroUsuario_RetornaForbidden()
        {
            // --- ARRANGE ---
            int idBorrar = 2;
            var imagenExistente = new Imagen { IdImagen = 2, Url = "a.jpg", IdInmueble = 5 };

            _imagenRepositoryMock
                .Setup(r => r.GetImagenById(idBorrar))
                .ReturnsAsync(imagenExistente);

            _inmuebleRepositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync(CrearInmueble(50));

            // --- ACT ---
            var result = await _sut.DeleteImagen(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(403);
            objectResult.Value.Should().Be("403: No puedes eliminar imágenes de un inmueble que no es tuyo.");
            _imagenRepositoryMock.Verify(r => r.DeleteImagen(It.IsAny<Imagen>()), Times.Never);
        }

        [Fact]
        public async Task DeleteImagen_Existe_EliminaYRetornaOk()
        {
            // --- ARRANGE ---
            int idBorrar = 2;
            var imagenExistente = new Imagen { IdImagen = 2, Url = "a.jpg", IdInmueble = 5 };

            _imagenRepositoryMock
                .Setup(r => r.GetImagenById(idBorrar))
                .ReturnsAsync(imagenExistente);

            _inmuebleRepositoryMock
                .Setup(r => r.GetInmuebleById(5))
                .ReturnsAsync(CrearInmueble(7));

            _imagenRepositoryMock
                .Setup(r => r.DeleteImagen(imagenExistente))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.DeleteImagen(idBorrar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            _imagenRepositoryMock.Verify(r => r.DeleteImagen(imagenExistente), Times.Once);
        }

        [Fact]
        public async Task DeleteImagen_RepositorioLanzaExcepcion_RetornaError500Generico()
        {
            // --- ARRANGE ---
            _imagenRepositoryMock
                .Setup(r => r.GetImagenById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.DeleteImagen(1);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500: Error interno del servidor.");
        }

        #endregion
    }
}