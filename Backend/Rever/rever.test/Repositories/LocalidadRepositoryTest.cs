using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.Repositories;
using rever.Tests.Helpers;
using Xunit;

namespace rever.Tests.Repositories
{
    public class LocalidadRepositoryTest
    {
        [Fact]
        public async Task GetLocalidad_ExistenLocalidades_RetornaLista()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.Localidad.AddRange(
                new Localidad { IdLocalidad = 1, Nombre = "Chapinero", IdCiudad = 1 },
                new Localidad { IdLocalidad = 2, Nombre = "Usaquén", IdCiudad = 1 }
            );
            await context.SaveChangesAsync();

            var repository = new LocalidadRepository(context);

            // ACT
            var resultado = await repository.GetLocalidad();

            // ASSERT
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetLocalidadById_Existe_RetornaLocalidad()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.Localidad.Add(new Localidad { IdLocalidad = 1, Nombre = "Suba", IdCiudad = 1 });
            await context.SaveChangesAsync();

            var repository = new LocalidadRepository(context);

            // ACT
            var resultado = await repository.GetLocalidadById(1);

            // ASSERT
            resultado.Should().NotBeNull();
            resultado!.Nombre.Should().Be("Suba");
        }

        [Fact]
        public async Task GetLocalidadById_NoExiste_RetornaNull()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new LocalidadRepository(context);

            // ACT
            var resultado = await repository.GetLocalidadById(999);

            // ASSERT
            resultado.Should().BeNull();
        }

        [Fact]
        public async Task PostLocalidad_GuardaExitosamente()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new LocalidadRepository(context);
            var nueva = new Localidad { IdLocalidad = 3, Nombre = "Engativá", IdCiudad = 1 };

            // ACT
            var resultado = await repository.PostLocalidad(nueva);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.Localidad.FirstOrDefaultAsync(l => l.IdLocalidad == 3);
            enBd.Should().NotBeNull();
            enBd!.Nombre.Should().Be("Engativá");
        }

        [Fact]
        public async Task PutLocalidad_Existe_ActualizaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var loc = new Localidad { IdLocalidad = 1, Nombre = "Fontibón", IdCiudad = 1 };
            context.Localidad.Add(loc);
            await context.SaveChangesAsync();

            var repository = new LocalidadRepository(context);
            loc.Nombre = "Fontibón Centro";

            // ACT
            var resultado = await repository.PutLocalidad(loc);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.Localidad.FirstOrDefaultAsync(l => l.IdLocalidad == 1);
            enBd!.Nombre.Should().Be("Fontibón Centro");
        }

        [Fact]
        public async Task PutLocalidad_NoExiste_RetornaFalse()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new LocalidadRepository(context);
            var loc = new Localidad { IdLocalidad = 999, Nombre = "Inexistente", IdCiudad = 1 };

            // ACT
            var resultado = await repository.PutLocalidad(loc);

            // ASSERT
            resultado.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteLocalidad_Existe_EliminaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var loc = new Localidad { IdLocalidad = 1, Nombre = "Teusaquillo", IdCiudad = 1 };
            context.Localidad.Add(loc);
            await context.SaveChangesAsync();

            var repository = new LocalidadRepository(context);

            // ACT
            var resultado = await repository.DeleteLocalidad(loc);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.Localidad.FirstOrDefaultAsync(l => l.IdLocalidad == 1);
            enBd.Should().BeNull();
        }
    }
}
