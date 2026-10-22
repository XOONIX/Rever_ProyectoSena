using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using Xunit;

namespace rever.Tests.Integration
{
    public class AuthIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public AuthIntegrationTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Login_DatosVacios_Retorna400BadRequest()
        {
            // ARRANGE
            var client = _factory.CreateClient();
            var loginInvalido = new Login { Correo = "", Contraseña = "" };

            // ACT
            var response = await client.PostAsJsonAsync("/api/Auth/Login", loginInvalido);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Login_UsuarioNoExiste_Retorna401Unauthorized()
        {
            // ARRANGE
            var client = _factory.CreateClient();
            var login = new Login { Correo = "noexiste@rever.com", Contraseña = "Password123*" };

            // ACT
            var response = await client.PostAsJsonAsync("/api/Auth/Login", login);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task FlujoCompleto_RegistroConfirmacionEInicioDeSesion()
        {
            // ARRANGE
            var client = _factory.CreateClient();
            var nuevoUsuario = new Usuario
            {
                Nombre = "Andrea Gómez",
                Correo = "andrea.integration@rever.com",
                Contraseña = "MiPasswordSeguro123!",
                Telefono = "3129876543",
                IdRol = 2
            };

            // 0. Asegurar que exista el rol correspondiente en BD
            await _factory.EjecutarEnContextoAsync(async context =>
            {
                if (!await context.Rol.AnyAsync(r => r.IdRol == 2))
                {
                    context.Rol.Add(new Rol { IdRol = 2, Nombre = "Comprador" });
                    await context.SaveChangesAsync();
                }
            });

            // 1. POST /api/Auth/Register
            var registerResponse = await client.PostAsJsonAsync("/api/Auth/Register", nuevoUsuario);
            registerResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            // 2. Obtener el token de confirmación generado en BD
            string? tokenConfirmacion = null;
            await _factory.EjecutarEnContextoAsync(async context =>
            {
                var usuarioEnBd = await context.Usuario.FirstOrDefaultAsync(u => u.Correo == nuevoUsuario.Correo);
                usuarioEnBd.Should().NotBeNull();
                tokenConfirmacion = usuarioEnBd!.TokenConfirmacion;
            });

            tokenConfirmacion.Should().NotBeNullOrWhiteSpace();

            // 3. GET /api/Auth/ConfirmEmail (desactivando auto-redirect para verificar la redirección al frontend)
            var clientSinRedirect = _factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            var emailEscaped = Uri.EscapeDataString(nuevoUsuario.Correo);
            var tokenEscaped = Uri.EscapeDataString(tokenConfirmacion!);
            var confirmResponse = await clientSinRedirect.GetAsync($"/api/Auth/ConfirmEmail?email={emailEscaped}&token={tokenEscaped}");
            confirmResponse.StatusCode.Should().Be(HttpStatusCode.Redirect);
            confirmResponse.Headers.Location!.ToString().Should().Contain("confirmed=true");

            // 4. POST /api/Auth/Login con credenciales válidas
            var loginRequest = new Login
            {
                Correo = nuevoUsuario.Correo,
                Contraseña = "MiPasswordSeguro123!"
            };

            var loginResponse = await client.PostAsJsonAsync("/api/Auth/Login", loginRequest);
            loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var jsonBody = await loginResponse.Content.ReadFromJsonAsync<JsonElement>();
            jsonBody.TryGetProperty("token", out var tokenProp).Should().BeTrue();
            tokenProp.GetString().Should().NotBeNullOrWhiteSpace();
        }
    }
}
