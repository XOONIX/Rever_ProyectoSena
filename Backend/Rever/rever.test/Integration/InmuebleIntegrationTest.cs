using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using TuProyecto.DTOs;
using Xunit;

namespace rever.Tests.Integration
{
    public class InmuebleIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public InmuebleIntegrationTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ListarInmueble_SinAutenticacion_PermiteAcceso()
        {
            // ARRANGE
            var client = _factory.CreateClient();

            // ACT
            var response = await client.GetAsync("/api/Inmueble/listar");

            // ASSERT: La ruta permite acceso anónimo
            response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CrearInmuebleCompleto_SinToken_Retorna401Unauthorized()
        {
            // ARRANGE: Cliente sin encabezado Authorization
            var client = _factory.CreateClient();
            var dto = new CrearInmuebleDto
            {
                Titulo = "Casa en venta",
                Descripcion = "Hermosa casa",
                Precio = 200000000m,
                IdTipo = 1,
                IdModo = 1,
                Direccion = "Calle 100 # 20-30",
                IdLocalidad = 1
            };

            // ACT
            var response = await client.PostAsJsonAsync("/api/Inmueble/completo", dto);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task FlujoCompleto_CrearInmuebleCompletoYConsultarDetalle()
        {
            // 1. Preparar dependencias en BD (Localidad, Ciudad, Característica)
            await _factory.EjecutarEnContextoAsync(async context =>
            {
                if (!await context.Ciudad.AnyAsync(c => c.IdCiudad == 100))
                {
                    context.Ciudad.Add(new Ciudad { IdCiudad = 100, Nombre = "Armenia" });
                }

                if (!await context.Localidad.AnyAsync(l => l.IdLocalidad == 100))
                {
                    context.Localidad.Add(new Localidad { IdLocalidad = 100, Nombre = "Norte", IdCiudad = 100 });
                }

                if (!await context.Caracteristica.AnyAsync(c => c.IdCaracteristica == 100))
                {
                    context.Caracteristica.Add(new Caracteristica { IdCaracteristica = 100, Nombre = "Zona BBQ" });
                }

                await context.SaveChangesAsync();
            });

            // 2. Cliente autenticado como Admin (Rol 1)
            var client = _factory.CrearClienteAutenticado(idUsuario: 25, rol: "1");

            var dto = new CrearInmuebleDto
            {
                Titulo = "Penthouse Quindío",
                Descripcion = "Con vista a la cordillera",
                Precio = 450000000m,
                IdTipo = 1,
                IdModo = 1,
                Direccion = "Avenida Bolívar # 10",
                IdLocalidad = 100,
                Habitaciones = 3,
                Banos = 3,
                Pisos = 2,
                MetrosCuadrados = 120,
                Estrato = 5,
                CaracteristicasIds = new List<int> { 100 },
                ImagenesUrls = new List<string> { "http://rever.com/foto1.jpg" }
            };

            // 3. POST /api/Inmueble/completo
            var postResponse = await client.PostAsJsonAsync("/api/Inmueble/completo", dto);
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var postJson = await postResponse.Content.ReadFromJsonAsync<JsonElement>();
            postJson.TryGetProperty("idInmueble", out var idProp).Should().BeTrue();
            int idInmuebleGenerado = idProp.GetInt32();
            idInmuebleGenerado.Should().BeGreaterThan(0);

            // 4. GET /api/Inmueble/{id}
            var getResponse = await client.GetAsync($"/api/Inmueble/{idInmuebleGenerado}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var inmuebleConsultado = await getResponse.Content.ReadFromJsonAsync<Inmueble>();
            inmuebleConsultado.Should().NotBeNull();
            inmuebleConsultado!.Titulo.Should().Be("Penthouse Quindío");
        }
    }
}
