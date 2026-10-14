using System.Runtime.CompilerServices;
using System.Security.Claims;
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
    public class UsuarioControllerTest
    {
        // OJO: ObtenerUsuario reconoce como admin solo el rol "administrador" (IsInRole("administrador")),
        // mientras que CrearUsuario / ActualizarUsuario / DeleteUsuario usan las extensiones (rol "1").
        private const string RolAdminObtener = "administrador";

        private readonly Mock<IUsuarioRepository> _repositoryMock;
        private readonly UsuarioController _sut; // System Under Test

        public UsuarioControllerTest()
        {
            _repositoryMock = new Mock<IUsuarioRepository>();
            _sut = new UsuarioController(_repositoryMock.Object);

            // Usuario NO admin con id 7 por defecto
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(7, ControllerTestHelper.RolUsuario));
        }

        // Usuario puede tener miembros 'required'; GetUninitializedObject evita llenarlos todos
        // y luego se asignan solo las propiedades que importan en cada test.
        private static Usuario CrearUsuarioModelo(int id = 7, string correo = "test@correo.com", string? contraseña = "Clave123", int idRol = 2)
        {
            var usuario = (Usuario)RuntimeHelpers.GetUninitializedObject(typeof(Usuario));
            usuario.IdUsuario = id;
            usuario.Correo = correo;
            usuario.Contraseña = contraseña!;
            usuario.IdRol = idRol;
            return usuario;
        }

        #region Pruebas de ListarUsuario (GET)

        [Fact]
        public async Task ListarUsuario_UsuarioNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);

            // --- ACT ---
            var result = await _sut.ListarUsuario();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task ListarUsuario_ExistenRegistros_RetornaOkConLista()
        {
            // --- ARRANGE ---
            var listaEsperada = new List<Usuario>
            {
                CrearUsuarioModelo(1, "a@correo.com"),
                CrearUsuarioModelo(2, "b@correo.com")
            };

            _repositoryMock
                .Setup(r => r.GetUsuario())
                .ReturnsAsync(listaEsperada);

            // --- ACT ---
            var result = await _sut.ListarUsuario();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(listaEsperada);
        }

        [Fact]
        public async Task ListarUsuario_ListaNula_RetornaNotFound()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetUsuario())
                .ReturnsAsync((List<Usuario>?)null);

            // --- ACT ---
            var result = await _sut.ListarUsuario();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se encontraron usuarios registrados.");
        }

        [Fact]
        public async Task ListarUsuario_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetUsuario())
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ListarUsuario();

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500: Error interno del servidor.");
        }

        #endregion

        #region Pruebas de ObtenerUsuario (GET por ID)

        [Fact]
        public async Task ObtenerUsuario_IdInvalido_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.ObtenerUsuario(0);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: El ID proporcionado no es válido.");
        }

        [Fact]
        public async Task ObtenerUsuario_PerfilDeOtroUsuarioNoAdmin_RetornaForbidden()
        {
            // --- ACT --- (el usuario es el 7 y pide el perfil 8)
            var result = await _sut.ObtenerUsuario(8);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(403);
            objectResult.Value.Should().Be("403: No puedes ver el perfil de otro usuario.");
        }

        [Fact]
        public async Task ObtenerUsuario_UsuarioAnonimo_RetornaForbidden()
        {
            // --- ARRANGE ---
            // Sin token no hay id, así que "id != idUsuarioToken" se cumple y responde 403 antes que 401.
            ControllerTestHelper.AsignarAnonimo(_sut);

            // --- ACT ---
            var result = await _sut.ObtenerUsuario(7);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(403);
            objectResult.Value.Should().Be("403: No puedes ver el perfil de otro usuario.");
        }

        [Fact]
        public async Task ObtenerUsuario_AdminNoAutenticado_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            // El 401 solo se alcanza con una identidad sin autenticar que además tenga el rol "administrador"
            // (ClaimsIdentity sin authenticationType => IsAuthenticated = false).
            var principal = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, RolAdminObtener)
            }));
            ControllerTestHelper.AsignarUsuario(_sut, principal);

            // --- ACT ---
            var result = await _sut.ObtenerUsuario(7);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: Usuario no autenticado.");
        }

        [Fact]
        public async Task ObtenerUsuario_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetUsuarioById(7))
                .ReturnsAsync((Usuario?)null);

            // --- ACT ---
            var result = await _sut.ObtenerUsuario(7);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se encontró el usuario con ID 7.");
        }

        [Fact]
        public async Task ObtenerUsuario_PerfilPropio_RetornaOkConObjeto()
        {
            // --- ARRANGE ---
            var usuarioDb = CrearUsuarioModelo(7);

            _repositoryMock
                .Setup(r => r.GetUsuarioById(7))
                .ReturnsAsync(usuarioDb);

            // --- ACT ---
            var result = await _sut.ObtenerUsuario(7);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(usuarioDb);
        }

        [Fact]
        public async Task ObtenerUsuario_AdminPerfilAjeno_RetornaOkConObjeto()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(1, RolAdminObtener));
            var usuarioDb = CrearUsuarioModelo(8);

            _repositoryMock
                .Setup(r => r.GetUsuarioById(8))
                .ReturnsAsync(usuarioDb);

            // --- ACT ---
            var result = await _sut.ObtenerUsuario(8);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeEquivalentTo(usuarioDb);
        }

        [Fact]
        public async Task ObtenerUsuario_RepositorioLanzaExcepcion_RetornaError500ConDetalle()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetUsuarioById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ObtenerUsuario(7);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500: Error interno del servidor. Detalle: Fallo de BD | Inner: ");
        }

        #endregion

        #region Pruebas de CrearUsuario (POST)

        [Fact]
        public async Task CrearUsuario_ObjetoNulo_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.CrearUsuario(null!);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos del usuario o la contraseña no pueden estar vacíos.");
        }

        [Theory]
        [InlineData("", "Clave123")]
        [InlineData("   ", "Clave123")]
        [InlineData("test@correo.com", "")]
        [InlineData("test@correo.com", "   ")]
        public async Task CrearUsuario_CorreoOContrasenaVacios_RetornaBadRequest(string correo, string contraseña)
        {
            // --- ARRANGE ---
            var usuario = CrearUsuarioModelo(0, correo, contraseña);

            // --- ACT ---
            var result = await _sut.CrearUsuario(usuario);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos del usuario o la contraseña no pueden estar vacíos.");
        }

        [Fact]
        public async Task CrearUsuario_CorreoYaRegistrado_RetornaBadRequest()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);
            var usuario = CrearUsuarioModelo(0, "repetido@correo.com", "Clave123", 2);

            _repositoryMock
                .Setup(r => r.GetByEmailWithRolAsync("repetido@correo.com"))
                .ReturnsAsync(CrearUsuarioModelo(5, "repetido@correo.com"));

            // --- ACT ---
            var result = await _sut.CrearUsuario(usuario);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: El correo ya se encuentra registrado.");
            _repositoryMock.Verify(r => r.PostUsuario(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact]
        public async Task CrearUsuario_NoAdminIntentaCrearRol1_RetornaForbidden()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);
            var usuario = CrearUsuarioModelo(0, "nuevo@correo.com", "Clave123", 1);

            _repositoryMock
                .Setup(r => r.GetByEmailWithRolAsync(It.IsAny<string>()))
                .ReturnsAsync((Usuario?)null);

            // --- ACT ---
            var result = await _sut.CrearUsuario(usuario);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(403);
            objectResult.Value.Should().Be("403: No tienes permisos para crear usuarios con rol 1.");
        }

        [Fact]
        public async Task CrearUsuario_NoAdminConRolNoPermitido_RetornaBadRequest()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);
            var usuario = CrearUsuarioModelo(0, "nuevo@correo.com", "Clave123", 5);

            _repositoryMock
                .Setup(r => r.GetByEmailWithRolAsync(It.IsAny<string>()))
                .ReturnsAsync((Usuario?)null);

            // --- ACT ---
            var result = await _sut.CrearUsuario(usuario);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Solo se permite la creación de usuarios con rol 2 o 3.");
        }

        [Fact]
        public async Task CrearUsuario_DatosValidos_HasheaContrasenaYRetornaOkSinContrasena()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);
            var usuario = CrearUsuarioModelo(0, "nuevo@correo.com", "Clave123", 2);
            string? hashGuardado = null;

            _repositoryMock
                .Setup(r => r.GetByEmailWithRolAsync(It.IsAny<string>()))
                .ReturnsAsync((Usuario?)null);

            // Se captura la contraseña en el momento de guardar, porque el controlador la pone en null después
            _repositoryMock
                .Setup(r => r.PostUsuario(It.IsAny<Usuario>()))
                .Callback<Usuario>(u => hashGuardado = u.Contraseña)
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.CrearUsuario(usuario);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeSameAs(usuario);

            hashGuardado.Should().NotBeNullOrEmpty();
            hashGuardado.Should().NotBe("Clave123");
            hashGuardado.Should().StartWith("$2"); // prefijo de los hashes BCrypt

            usuario.Contraseña.Should().BeNull(); // no se devuelve la contraseña al cliente
        }

        [Fact]
        public async Task CrearUsuario_AdminCreaRol1_RetornaOk()
        {
            // --- ARRANGE --- (admin real: rol "1")
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(1, ControllerTestHelper.RolAdmin));
            var usuario = CrearUsuarioModelo(0, "admin2@correo.com", "Clave123", 1);

            _repositoryMock
                .Setup(r => r.GetByEmailWithRolAsync(It.IsAny<string>()))
                .ReturnsAsync((Usuario?)null);

            _repositoryMock
                .Setup(r => r.PostUsuario(It.IsAny<Usuario>()))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.CrearUsuario(usuario);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task CrearUsuario_RepositorioRetornaFalse_RetornaError500()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);
            var usuario = CrearUsuarioModelo(0, "nuevo@correo.com", "Clave123", 2);

            _repositoryMock
                .Setup(r => r.GetByEmailWithRolAsync(It.IsAny<string>()))
                .ReturnsAsync((Usuario?)null);

            _repositoryMock
                .Setup(r => r.PostUsuario(It.IsAny<Usuario>()))
                .ReturnsAsync(false);

            // --- ACT ---
            var result = await _sut.CrearUsuario(usuario);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500: Error interno al intentar crear el recurso.");
        }

        [Fact]
        public async Task CrearUsuario_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarAnonimo(_sut);
            var usuario = CrearUsuarioModelo(0, "nuevo@correo.com", "Clave123", 2);

            _repositoryMock
                .Setup(r => r.GetByEmailWithRolAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.CrearUsuario(usuario);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500: Error interno del servidor.");
        }

        #endregion

        #region Pruebas de ActualizarUsuario (PUT)

        [Fact]
        public async Task ActualizarUsuario_ObjetoNulo_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.ActualizarUsuario(null!);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para actualizar o el ID no son válidos.");
        }

        [Fact]
        public async Task ActualizarUsuario_IdInvalido_RetornaBadRequest()
        {
            // --- ARRANGE ---
            var usuarioInvalido = CrearUsuarioModelo(0);

            // --- ACT ---
            var result = await _sut.ActualizarUsuario(usuarioInvalido);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: Los datos para actualizar o el ID no son válidos.");
        }

        [Fact]
        public async Task ActualizarUsuario_CuentaDeOtroUsuario_RetornaForbidden()
        {
            // --- ARRANGE --- (el usuario es el 7 e intenta actualizar el 8)
            var usuario = CrearUsuarioModelo(8);

            // --- ACT ---
            var result = await _sut.ActualizarUsuario(usuario);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(403);
            objectResult.Value.Should().Be("403: No puedes actualizar la cuenta de otro usuario.");
            _repositoryMock.Verify(r => r.PutUsuario(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact]
        public async Task ActualizarUsuario_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            var usuario = CrearUsuarioModelo(7);

            _repositoryMock
                .Setup(r => r.GetUsuarioById(7))
                .ReturnsAsync((Usuario?)null);

            // --- ACT ---
            var result = await _sut.ActualizarUsuario(usuario);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se puede actualizar. El usuario con ID 7 no existe.");
        }

        [Fact]
        public async Task ActualizarUsuario_SinContrasenaNueva_ConservaContrasenaYRetornaOk()
        {
            // --- ARRANGE ---
            var exist = CrearUsuarioModelo(7, "viejo@correo.com", "hashAntiguo", 2);
            var aActualizar = CrearUsuarioModelo(7, "nuevo@correo.com", null, 2);
            string? correoGuardado = null;
            string? contraseñaGuardada = null;

            _repositoryMock
                .Setup(r => r.GetUsuarioById(7))
                .ReturnsAsync(exist);

            _repositoryMock
                .Setup(r => r.PutUsuario(It.IsAny<Usuario>()))
                .Callback<Usuario>(u =>
                {
                    correoGuardado = u.Correo;
                    contraseñaGuardada = u.Contraseña;
                })
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.ActualizarUsuario(aActualizar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().BeSameAs(exist);

            correoGuardado.Should().Be("nuevo@correo.com");
            contraseñaGuardada.Should().Be("hashAntiguo");
            exist.Contraseña.Should().BeNull(); // no se devuelve la contraseña al cliente
        }

        [Fact]
        public async Task ActualizarUsuario_ConContrasenaNueva_LaHasheaYRetornaOk()
        {
            // --- ARRANGE ---
            var exist = CrearUsuarioModelo(7, "viejo@correo.com", "hashAntiguo", 2);
            var aActualizar = CrearUsuarioModelo(7, "viejo@correo.com", "NuevaClave1", 2);
            string? contraseñaGuardada = null;

            _repositoryMock
                .Setup(r => r.GetUsuarioById(7))
                .ReturnsAsync(exist);

            _repositoryMock
                .Setup(r => r.PutUsuario(It.IsAny<Usuario>()))
                .Callback<Usuario>(u => contraseñaGuardada = u.Contraseña)
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.ActualizarUsuario(aActualizar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            contraseñaGuardada.Should().NotBe("NuevaClave1");
            contraseñaGuardada.Should().NotBe("hashAntiguo");
            contraseñaGuardada.Should().StartWith("$2");
        }

        [Fact]
        public async Task ActualizarUsuario_AdminCuentaAjena_RetornaOk()
        {
            // --- ARRANGE --- (admin real: rol "1")
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(1, ControllerTestHelper.RolAdmin));
            var exist = CrearUsuarioModelo(8, "viejo@correo.com", "hashAntiguo", 2);
            var aActualizar = CrearUsuarioModelo(8, "nuevo@correo.com", null, 2);

            _repositoryMock
                .Setup(r => r.GetUsuarioById(8))
                .ReturnsAsync(exist);

            _repositoryMock
                .Setup(r => r.PutUsuario(It.IsAny<Usuario>()))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.ActualizarUsuario(aActualizar);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task ActualizarUsuario_RepositorioLanzaExcepcion_RetornaError500ConDetalle()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetUsuarioById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.ActualizarUsuario(CrearUsuarioModelo(7));

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500: Error interno del servidor. Detalle: Fallo de BD");
        }

        #endregion

        #region Pruebas de DeleteUsuario (DELETE)

        [Fact]
        public async Task DeleteUsuario_IdInvalido_RetornaBadRequest()
        {
            // --- ACT ---
            var result = await _sut.DeleteUsuario(0);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
            objectResult.Value.Should().Be("400: El ID del usuario no es válido.");
        }

        [Fact]
        public async Task DeleteUsuario_TokenSinId_RetornaUnauthorized()
        {
            // --- ARRANGE ---
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuarioSinId());

            // --- ACT ---
            var result = await _sut.DeleteUsuario(7);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(401);
            objectResult.Value.Should().Be("401: No se pudo verificar la identidad desde el token.");
        }

        [Fact]
        public async Task DeleteUsuario_CuentaDeOtroUsuario_RetornaForbidden()
        {
            // --- ACT --- (el usuario es el 7 e intenta borrar el 8)
            var result = await _sut.DeleteUsuario(8);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(403);
            objectResult.Value.Should().Be("403: No puedes eliminar la cuenta de otro usuario.");
            _repositoryMock.Verify(r => r.DeleteUsuario(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact]
        public async Task DeleteUsuario_NoExiste_RetornaNotFound()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetUsuarioById(7))
                .ReturnsAsync((Usuario?)null);

            // --- ACT ---
            var result = await _sut.DeleteUsuario(7);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(404);
            objectResult.Value.Should().Be("404: No se puede eliminar. El usuario con ID 7 no existe.");
        }

        [Fact]
        public async Task DeleteUsuario_CuentaPropia_EliminaYRetornaOk()
        {
            // --- ARRANGE ---
            var usuarioExistente = CrearUsuarioModelo(7);

            _repositoryMock
                .Setup(r => r.GetUsuarioById(7))
                .ReturnsAsync(usuarioExistente);

            _repositoryMock
                .Setup(r => r.DeleteUsuario(usuarioExistente))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.DeleteUsuario(7);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
            _repositoryMock.Verify(r => r.DeleteUsuario(usuarioExistente), Times.Once);
        }

        [Fact]
        public async Task DeleteUsuario_AdminCuentaAjena_EliminaYRetornaOk()
        {
            // --- ARRANGE --- (admin real: rol "1")
            ControllerTestHelper.AsignarUsuario(_sut, ControllerTestHelper.CrearUsuario(1, ControllerTestHelper.RolAdmin));
            var usuarioExistente = CrearUsuarioModelo(8);

            _repositoryMock
                .Setup(r => r.GetUsuarioById(8))
                .ReturnsAsync(usuarioExistente);

            _repositoryMock
                .Setup(r => r.DeleteUsuario(usuarioExistente))
                .ReturnsAsync(true);

            // --- ACT ---
            var result = await _sut.DeleteUsuario(8);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task DeleteUsuario_RepositorioLanzaExcepcion_RetornaError500()
        {
            // --- ARRANGE ---
            _repositoryMock
                .Setup(r => r.GetUsuarioById(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Fallo de BD"));

            // --- ACT ---
            var result = await _sut.DeleteUsuario(7);

            // --- ASSERT ---
            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
            objectResult.Value.Should().Be("500: Error interno del servidor.");
        }

        #endregion
    }
}
