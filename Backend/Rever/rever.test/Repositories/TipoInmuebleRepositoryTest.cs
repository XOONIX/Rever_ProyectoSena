using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.Repositories;
using rever.Tests.Helpers;
using Xunit;

namespace rever.Tests.Repositories
{
    public class TipoInmuebleRepositoryTest
    {
        [Fact]
        public async Task GetTipoInmueble_ExistenTipos_RetornaLista()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.TipoInmueble.AddRange(
                new TipoInmueble { IdTipo = 1, Nombre = "Apartamento" },
                new TipoInmueble { IdTipo = 2, Nombre = "Casa" }
            );
            await context.SaveChangesAsync();

            var repository = new TipoInmuebleRepository(context);

            // ACT
            var resultado = await repository.GetTipoInmueble();

            // ASSERT
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetTipoInmuebleById_Existe_RetornaTipo()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.TipoInmueble.Add(new TipoInmueble { IdTipo = 1, Nombre = "Oficina" });
            await context.SaveChangesAsync();

            var repository = new TipoInmuebleRepository(context);

            // ACT
            var resultado = await repository.GetTipoInmuebleById(1);

            // ASSERT
            resultado.Should().NotBeNull();
            resultado!.Nombre.Should().Be("Oficina");
        }

        [Fact]
        public async Task GetTipoInmuebleById_NoExiste_RetornaNull()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new TipoInmuebleRepository(context);

            // ACT
            var resultado = await repository.GetTipoInmuebleById(999);

            // ASSERT
            resultado.Should().BeNull();
        }

        [Fact]
        public async Task PostTipoInmueble_GuardaExitosamente()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new TipoInmuebleRepository(context);
            var nuevoTipo = new TipoInmueble { IdTipo = 3, Nombre = "Bodega" };

            // ACT
            var resultado = await repository.PostTipoInmueble(nuevoTipo);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.TipoInmueble.FirstOrDefaultAsync(t => t.IdTipo == 3);
            enBd.Should().NotBeNull();
            enBd!.Nombre.Should().Be("Bodega");
        }

        [Fact]
        public async Task PutTipoInmueble_Existe_ActualizaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var tipo = new TipoInmueble { IdTipo = 1, Nombre = "Local" };
            context.TipoInmueble.Add(tipo);
            await context.SaveChangesAsync();

            var repository = new TipoInmuebleRepository(context);
            tipo.Nombre = "Local Comercial";

            // ACT
            var resultado = await repository.PutTipoInmueble(tipo);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.TipoInmueble.FirstOrDefaultAsync(t => t.IdTipo == 1);
            enBd!.Nombre.Should().Be("Local Comercial");
        }

        [Fact]
        public async Task PutTipoInmueble_NoExiste_RetornaFalse()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new TipoInmuebleRepository(context);
            var tipo = new TipoInmueble { IdTipo = 999, Nombre = "Inexistente" };

            // ACT
            var resultado = await repository.PutTipoInmueble(tipo);

            // ASSERT
            resultado.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteTipoInmueble_Existe_EliminaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var tipo = new TipoInmueble { IdTipo = 1, Nombre = "Finca" };
            context.TipoInmueble.Add(tipo);
            await context.SaveChangesAsync();

            var repository = new TipoInmuebleRepository(context);

            // ACT
            var resultado = await repository.DeleteTipoInmueble(tipo);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.TipoInmueble.FirstOrDefaultAsync(t => t.IdTipo == 1);
            enBd.Should().BeNull();
        }
    }
}
