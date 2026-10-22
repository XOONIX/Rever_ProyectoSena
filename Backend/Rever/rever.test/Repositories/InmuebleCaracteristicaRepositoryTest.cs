using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.Repositories;
using rever.Tests.Helpers;
using Xunit;

namespace rever.Tests.Repositories
{
    public class InmuebleCaracteristicaRepositoryTest
    {
        [Fact]
        public async Task GetInmuebleCaracteristica_ExistenRegistros_RetornaLista()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.InmuebleCaracteristica.AddRange(
                new InmuebleCaracteristica { IdInmueble = 1, IdCaracteristica = 10 },
                new InmuebleCaracteristica { IdInmueble = 1, IdCaracteristica = 20 }
            );
            await context.SaveChangesAsync();

            var repository = new InmuebleCaracteristicaRepository(context);

            // ACT
            var resultado = await repository.GetInmuebleCaracteristica();

            // ASSERT
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetByIds_Existe_RetornaRegistro()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.InmuebleCaracteristica.Add(new InmuebleCaracteristica { IdInmueble = 5, IdCaracteristica = 15 });
            await context.SaveChangesAsync();

            var repository = new InmuebleCaracteristicaRepository(context);

            // ACT
            var resultado = await repository.GetByIds(5, 15);

            // ASSERT
            resultado.Should().NotBeNull();
            resultado!.IdInmueble.Should().Be(5);
            resultado.IdCaracteristica.Should().Be(15);
        }

        [Fact]
        public async Task GetByIds_NoExiste_RetornaNull()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new InmuebleCaracteristicaRepository(context);

            // ACT
            var resultado = await repository.GetByIds(999, 999);

            // ASSERT
            resultado.Should().BeNull();
        }

        [Fact]
        public async Task PostInmuebleCaracteristica_NoExistePreviamente_GuardaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new InmuebleCaracteristicaRepository(context);
            var item = new InmuebleCaracteristica { IdInmueble = 2, IdCaracteristica = 7 };

            // ACT
            var resultado = await repository.PostInmuebleCaracteristica(item);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.InmuebleCaracteristica.FirstOrDefaultAsync(ic => ic.IdInmueble == 2 && ic.IdCaracteristica == 7);
            enBd.Should().NotBeNull();
        }

        [Fact]
        public async Task PostInmuebleCaracteristica_YaExiste_RetornaFalse()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.InmuebleCaracteristica.Add(new InmuebleCaracteristica { IdInmueble = 3, IdCaracteristica = 8 });
            await context.SaveChangesAsync();

            var repository = new InmuebleCaracteristicaRepository(context);
            var repetido = new InmuebleCaracteristica { IdInmueble = 3, IdCaracteristica = 8 };

            // ACT
            var resultado = await repository.PostInmuebleCaracteristica(repetido);

            // ASSERT
            resultado.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteInmuebleCaracteristica_Existe_EliminaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var item = new InmuebleCaracteristica { IdInmueble = 4, IdCaracteristica = 9 };
            context.InmuebleCaracteristica.Add(item);
            await context.SaveChangesAsync();

            var repository = new InmuebleCaracteristicaRepository(context);

            // ACT
            var resultado = await repository.DeleteInmuebleCaracteristica(item);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.InmuebleCaracteristica.FirstOrDefaultAsync(ic => ic.IdInmueble == 4 && ic.IdCaracteristica == 9);
            enBd.Should().BeNull();
        }
    }
}
