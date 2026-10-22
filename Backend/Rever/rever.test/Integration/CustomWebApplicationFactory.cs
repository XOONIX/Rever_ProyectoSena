using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Moq;
using rever.contexto;

namespace rever.Tests.Integration
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly string _databaseName = "ReverIntegrationTestsDb_" + Guid.NewGuid().ToString();

        public const string TestJwtKey = "ThisIsA256BitKeyThatIsExactly32BytesLongAndEncodedInBase64";
        public const string TestIssuer = "https://localhost:7015/";
        public const string TestAudience = "https://localhost:7015/";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                // Remover descriptores de EF Core y SqlServer para evitar conflicto de múltiples proveedores
                var efDescriptors = services
                    .Where(d => (d.ServiceType.Namespace != null && d.ServiceType.Namespace.StartsWith("Microsoft.EntityFrameworkCore"))
                                || d.ServiceType == typeof(DatabaseService)
                                || d.ImplementationType == typeof(DatabaseService))
                    .ToList();

                foreach (var d in efDescriptors)
                {
                    services.Remove(d);
                }

                services.AddDbContext<DatabaseService>(options =>
                {
                    options.UseInMemoryDatabase(_databaseName)
                           .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                });

                // Mockear IEmailService para evitar llamadas SMTP reales en tests
                var emailServiceDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(IEmailService));
                if (emailServiceDescriptor != null)
                {
                    services.Remove(emailServiceDescriptor);
                }

                var mockEmail = new Mock<IEmailService>();
                mockEmail.Setup(e => e.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                         .Returns(Task.CompletedTask);

                services.AddSingleton(mockEmail.Object);
            });
        }

        public string GenerarTokenJwt(int idUsuario = 1, string rol = "1", string correo = "test@rever.com")
        {
            var keyBytes = Encoding.UTF8.GetBytes(TestJwtKey);
            var secretKey = new SymmetricSecurityKey(keyBytes);
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString()),
                new Claim(ClaimTypes.Name, correo),
                new Claim(ClaimTypes.Role, rol),
                new Claim("nombre", "Usuario Test")
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: TestIssuer,
                audience: TestAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public HttpClient CrearClienteAutenticado(int idUsuario = 1, string rol = "1", string correo = "test@rever.com")
        {
            var client = CreateClient();
            var token = GenerarTokenJwt(idUsuario, rol, correo);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        public async Task EjecutarEnContextoAsync(Func<DatabaseService, Task> accion)
        {
            using var scope = Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DatabaseService>();
            await accion(context);
        }
    }
}
