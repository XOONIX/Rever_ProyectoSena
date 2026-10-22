using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.Repositories;
using rever.Tests.Helpers;
using Xunit;

namespace rever.Tests.Repositories
{
    public class CaracteristicaRepositoryTest
    {
        [Fact]
        public async Task GetCaracteristica_ExistenRegistros_RetornaLista()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.Caracteristica.AddRange(
                new Caracteristica { IdCaracteristica = 1, Nombre = "Piscina" },
                new Caracteristica { IdCaracteristica = 2, Nombre = "Ascensor" }
            );
            await context.SaveChangesAsync();

            var repository = new CaracteristicaRepository(context);

            // ACT
            var resultado = await repository.GetCaracteristica();

            // ASSERT
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetCaracteristicaById_Existe_RetornaEntidad()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.Caracteristica.Add(new Caracteristica { IdCaracteristica = 1, Nombre = "Gimnasio" });
            await context.SaveChangesAsync();

            var repository = new CaracteristicaRepository(context);

            // ACT
            var resultado = await repository.GetCaracteristicaById(1);

            // ASSERT
            resultado.Should().NotBeNull();
            resultado!.Nombre.Should().Be("Gimnasio");
        }

        [Fact]
        public async Task GetCaracteristicaById_NoExiste_RetornaNull()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new CaracteristicaRepository(context);

            // ACT
            var resultado = await repository.GetCaracteristicaById(999);

            // ASSERT
            resultado.Should().BeNull();
        }

        [Fact]
        public async Task PostCaracteristica_GuardaExitosamente()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new CaracteristicaRepository(context);
            var nueva = new Caracteristica { IdCaracteristica = 5, Nombre = "Parqueadero" };

            // ACT
            var resultado = await repository.PostCaracteristica(nueva);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.Caracteristica.FirstOrDefaultAsync(c => c.IdCaracteristica == 5);
            enBd.Should().NotBeNull();
            enBd!.Nombre.Should().Be("Parqueadero");
        }

        [Fact]
        public async Task PutCaracteristica_Existe_ActualizaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var item = new Caracteristica { IdCaracteristica = 1, Nombre = "Balcón" };
            context.Caracteristica.Add(item);
            await context.SaveChangesAsync();

            var repository = new CaracteristicaRepository(context);
            item.Nombre = "Balcón Grande";

            // ACT
            var resultado = await repository.PutCaracteristica(item);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.Caracteristica.FirstOrDefaultAsync(c => c.IdCaracteristica == 1);
            enBd!.Nombre.Should().Be("Balcón Grande");
        }

        [Fact]
        public async Task PutCaracteristica_NoExiste_RetornaFalse()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new CaracteristicaRepository(context);
            var item = new Caracteristica { IdCaracteristica = 999, Nombre = "Inexistente" };

            // ACT
            var resultado = await repository.PutCaracteristica(item);

            // ASSERT
            resultado.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteCaracteristica_Existe_EliminaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var item = new Caracteristica { IdCaracteristica = 1, Nombre = "Terraza" };
            context.Caracteristica.Add(item);
            await context.SaveChangesAsync();

            var repository = new CaracteristicaRepository(context);

            // ACT
            var resultado = await repository.DeleteCaracteristica(item);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.Caracteristica.FirstOrDefaultAsync(c => c.IdCaracteristica == 1);
            enBd.Should().BeNull();
        }
    }
}
