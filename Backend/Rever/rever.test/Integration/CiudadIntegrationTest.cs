using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using rever.Models;
using Xunit;

namespace rever.Tests.Integration
{
    public class CiudadIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public CiudadIntegrationTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetCiudades_SinAutenticacion_PermiteAcceso()
        {
            // ARRANGE: Cliente anónimo
            var client = _factory.CreateClient();

            // ACT
            var response = await client.GetAsync("/api/Ciudad");
            var errorBody = await response.Content.ReadAsStringAsync();

            // ASSERT: Debe responder 200 o 404 dependiendo si hay registros, pero nunca 401
            response.StatusCode.Should().BeOneOf(new[] { HttpStatusCode.OK, HttpStatusCode.NotFound }, errorBody);
        }

        [Fact]
        public async Task CrearCiudad_SinToken_Retorna401Unauthorized()
        {
            // ARRANGE: Cliente sin encabezado Authorization
            var client = _factory.CreateClient();
            var nuevaCiudad = new Ciudad { IdCiudad = 991, Nombre = "Ciudad No Autorizada" };

            // ACT
            var response = await client.PostAsJsonAsync("/api/Ciudad", nuevaCiudad);

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task FlujoCompleto_CrearObtenerActualizarYEliminarCiudad()
        {
            // ARRANGE: Cliente autenticado con token JWT válido
            var client = _factory.CrearClienteAutenticado(idUsuario: 1, rol: "1");
            var ciudad = new Ciudad { Nombre = "Pereira" };

            // 1. POST /api/Ciudad
            var postResponse = await client.PostAsJsonAsync("/api/Ciudad", ciudad);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            // 2. Obtener lista para conocer el ID generado por la BD
            var lista = await client.GetFromJsonAsync<List<Ciudad>>("/api/Ciudad");
            lista.Should().NotBeNull();
            var ciudadCreada = lista!.First(c => c.Nombre == "Pereira");
            int idGenerado = ciudadCreada.IdCiudad;

            // 3. GET /api/Ciudad/{id}
            var getResponse = await client.GetAsync($"/api/Ciudad/{idGenerado}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var ciudadObtenida = await getResponse.Content.ReadFromJsonAsync<Ciudad>();
            ciudadObtenida.Should().NotBeNull();
            ciudadObtenida!.Nombre.Should().Be("Pereira");

            // 4. PUT /api/Ciudad
            ciudadCreada.Nombre = "Pereira Capital del Eje";
            var putResponse = await client.PutAsJsonAsync("/api/Ciudad", ciudadCreada);
            putResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            // 5. DELETE /api/Ciudad/{id}
            var deleteResponse = await client.DeleteAsync($"/api/Ciudad/{idGenerado}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            // 6. Verificar que ya no existe (404)
            var getPostDelete = await client.GetAsync($"/api/Ciudad/{idGenerado}");
            getPostDelete.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
