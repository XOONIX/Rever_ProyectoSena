using Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using rever.Controllers;
using rever.Models;
using rever.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Xunit;

namespace rever.Tests.Controllers
{
    public class AuthControllerTest
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly AuthController _sut; // System Under Test

        public AuthControllerTest()
        {
            _configurationMock = new Mock<IConfiguration>();
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _emailServiceMock = new Mock<IEmailService>();

            // Configurar claves JWT necesarias
            _configurationMock.Setup(c => c["Jwt:Key"]).Returns("SuperSecretKeyConMasDe32CaracteresLongitud123!");
            _configurationMock.Setup(c => c["Jwt:Issuer"]).Returns("ReverApi");
            _configurationMock.Setup(c => c["Jwt:Audience"]).Returns("ReverApp");
            _configurationMock.Setup(c => c["Jwt:DurationInMinutes"]).Returns("60");

            _sut = new AuthController(
                _configurationMock.Object,
                _usuarioRepositoryMock.Object,
                _emailServiceMock.Object
            );

            // Simular el contexto HTTP para Request.Scheme y Request.Host
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "http";
            httpContext.Request.Host = new HostString("localhost", 5000);
            _sut.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }

        #region Pruebas de Login

        [Fact]
        public async Task Login_LoginInvalidoONulo_RetornaBadRequest()
        {
            // Act
            var result = await _sut.Login(null!);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Login_UsuarioNoExiste_RetornaUnauthorized()
        {
            // Arrange
            var loginDto = new Login { Correo = "noexiste@test.com", Contraseña = "Password123" };
            _usuarioRepositoryMock
                .Setup(r => r.GetByEmailWithRolAsync(loginDto.Correo))
                .ReturnsAsync((Usuario?)null);

            // Act
            var result = await _sut.Login(loginDto);

            // Assert
            var unauthorized = result.Should().BeOfType<UnauthorizedObjectResult>().Subject;
            unauthorized.Value.Should().Be("Credenciales inválidas");
        }

        [Fact]
        public async Task Login_UsuarioNoConfirmado_RetornaUnauthorized()
        {
            // Arrange
            var passwordEncriptada = BCrypt.Net.BCrypt.HashPassword("Password123");
            var usuarioDb = new Usuario
            {
                IdUsuario = 1,
                Nombre = "Usuario Test",
                Telefono = "3001234567",
                Correo = "test@test.com",
                Contraseña = passwordEncriptada,
                Confirmado = false,
                IdRol = 2
            };

            var loginDto = new Login { Correo = "test@test.com", Contraseña = "Password123" };

            _usuarioRepositoryMock
                .Setup(r => r.GetByEmailWithRolAsync(loginDto.Correo))
                .ReturnsAsync(usuarioDb);

            // Act
            var result = await _sut.Login(loginDto);

            // Assert
            var unauthorized = result.Should().BeOfType<UnauthorizedObjectResult>().Subject;
            unauthorized.Value.Should().Be("Debes confirmar tu cuenta desde el correo que te enviamos antes de iniciar sesión.");
        }

        [Fact]
        public async Task Login_CredencialesValidasYConfirmado_RetornaOkConToken()
        {
            // Arrange
            var passwordEncriptada = BCrypt.Net.BCrypt.HashPassword("Password123");
            var usuarioDb = new Usuario
            {
                IdUsuario = 1,
                Nombre = "Usuario Test",
                Telefono = "3001234567",
                Correo = "test@test.com",
                Contraseña = passwordEncriptada,
                Confirmado = true,
                IdRol = 2,
                Rol = new Rol { IdRol = 2, Nombre = "Comprador" }
            };

            var loginDto = new Login { Correo = "test@test.com", Contraseña = "Password123" };

            _usuarioRepositoryMock
                .Setup(r => r.GetByEmailWithRolAsync(loginDto.Correo))
                .ReturnsAsync(usuarioDb);

            // Act
            var result = await _sut.Login(loginDto);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().NotBeNull();
        }

        #endregion

        #region Pruebas de Register

        [Fact]
        public async Task Register_CorreoYaExiste_RetornaBadRequest()
        {
            // Arrange
            var nuevoUsuario = new Usuario
            {
                Nombre = "Test",
                Telefono = "3001234567",
                Correo = "existente@test.com",
                Contraseña = "123",
                IdRol = 2
            };

            _usuarioRepositoryMock
                .Setup(r => r.GetByEmailWithRolAsync(nuevoUsuario.Correo))
                .ReturnsAsync(new Usuario
                {
                    IdUsuario = 1,
                    Nombre = "Existente",
                    Telefono = "3001234567",
                    Correo = "existente@test.com",
                    Contraseña = "123",
                    IdRol = 2
                });

            // Act
            var result = await _sut.Register(nuevoUsuario);

            // Assert
            var badRequest = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequest.Value.Should().Be("El correo ya se encuentra registrado.");
        }

        [Fact]
        public async Task Register_UsuarioValido_GuardaUsuarioYEnviaCorreo()
        {
            // Arrange
            var nuevoUsuario = new Usuario
            {
                Nombre = "Nuevo Usuario",
                Telefono = "3001234567",
                Correo = "nuevo@test.com",
                Contraseña = "Password123",
                IdRol = 2
            };

            _usuarioRepositoryMock
                .Setup(r => r.GetByEmailWithRolAsync(nuevoUsuario.Correo))
                .ReturnsAsync((Usuario?)null);

            _usuarioRepositoryMock
                .Setup(r => r.PostUsuario(It.IsAny<Usuario>()))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.Register(nuevoUsuario);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            // Verificar inserción y envío de email
            _usuarioRepositoryMock.Verify(r => r.PostUsuario(It.Is<Usuario>(u => u.Confirmado == false && u.TokenConfirmacion != null)), Times.Once);
            _emailServiceMock.Verify(e => e.SendEmailAsync(nuevoUsuario.Correo, "Confirma tu cuenta de Rever", It.IsAny<string>()), Times.Once);
        }

        #endregion

        #region Pruebas de ConfirmEmail

        [Fact]
        public async Task ConfirmEmail_TokenInvalido_RedirigeConError()
        {
            // Arrange
            _usuarioRepositoryMock
                .Setup(r => r.GetByEmailWithRolAsync("test@test.com"))
                .ReturnsAsync(new Usuario
                {
                    Nombre = "Test",
                    Telefono = "3001234567",
                    Correo = "test@test.com",
                    Contraseña = "123",
                    IdRol = 2,
                    TokenConfirmacion = "token_correcto"
                });

            // Act
            var result = await _sut.ConfirmEmail("test@test.com", "token_incorrecto");

            // Assert
            var redirect = result.Should().BeOfType<RedirectResult>().Subject;
            redirect.Url.Should().Contain("error=invalid_token");
        }

        [Fact]
        public async Task ConfirmEmail_TokenValido_ConfirmaUsuarioYRedirigeExitoso()
        {
            // Arrange
            var usuario = new Usuario
            {
                Nombre = "Test",
                Telefono = "3001234567",
                Correo = "test@test.com",
                Contraseña = "123",
                IdRol = 2,
                TokenConfirmacion = "valid_token",
                Confirmado = false
            };

            _usuarioRepositoryMock
                .Setup(r => r.GetByEmailWithRolAsync("test@test.com"))
                .ReturnsAsync(usuario);

            // Act
            var result = await _sut.ConfirmEmail("test@test.com", "valid_token");

            // Assert
            usuario.Confirmado.Should().BeTrue();
            usuario.TokenConfirmacion.Should().BeNull();

            _usuarioRepositoryMock.Verify(r => r.PutUsuario(usuario), Times.Once);

            var redirect = result.Should().BeOfType<RedirectResult>().Subject;
            redirect.Url.Should().Contain("confirmed=true");
        }

        #endregion
    }
}