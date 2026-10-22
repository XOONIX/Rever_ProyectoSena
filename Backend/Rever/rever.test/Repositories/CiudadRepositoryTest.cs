using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.Repositories;
using rever.Tests.Helpers;
using Xunit;

namespace rever.Tests.Repositories
{
    public class CiudadRepositoryTest
    {
        [Fact]
        public async Task GetCiudad_ExistenCiudades_RetornaLista()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.Ciudad.AddRange(
                new Ciudad { IdCiudad = 1, Nombre = "Bogotá" },
                new Ciudad { IdCiudad = 2, Nombre = "Medellín" }
            );
            await context.SaveChangesAsync();

            var repository = new CiudadRepository(context);

            // ACT
            var resultado = await repository.GetCiudad();

            // ASSERT
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
            resultado.Should().Contain(c => c.Nombre == "Bogotá");
        }

        [Fact]
        public async Task GetCiudad_SinCiudades_RetornaListaVacia()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new CiudadRepository(context);

            // ACT
            var resultado = await repository.GetCiudad();

            // ASSERT
            resultado.Should().NotBeNull();
            resultado.Should().BeEmpty();
        }

        [Fact]
        public async Task GetCiudadById_Existe_RetornaCiudad()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.Ciudad.Add(new Ciudad { IdCiudad = 5, Nombre = "Cali" });
            await context.SaveChangesAsync();

            var repository = new CiudadRepository(context);

            // ACT
            var resultado = await repository.GetCiudadById(5);

            // ASSERT
            resultado.Should().NotBeNull();
            resultado!.Nombre.Should().Be("Cali");
        }

        [Fact]
        public async Task GetCiudadById_NoExiste_RetornaNull()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new CiudadRepository(context);

            // ACT
            var resultado = await repository.GetCiudadById(999);

            // ASSERT
            resultado.Should().BeNull();
        }

        [Fact]
        public async Task PostCiudad_DatosValidos_GuardaExitosamente()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new CiudadRepository(context);
            var nuevaCiudad = new Ciudad { IdCiudad = 10, Nombre = "Barranquilla" };

            // ACT
            var resultado = await repository.PostCiudad(nuevaCiudad);

            // ASSERT
            resultado.Should().BeTrue();
            var ciudadEnBd = await context.Ciudad.FirstOrDefaultAsync(c => c.IdCiudad == 10);
            ciudadEnBd.Should().NotBeNull();
            ciudadEnBd!.Nombre.Should().Be("Barranquilla");
        }

        [Fact]
        public async Task PutCiudad_Existe_ActualizaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var ciudadOriginal = new Ciudad { IdCiudad = 20, Nombre = "Cartagena Antigua" };
            context.Ciudad.Add(ciudadOriginal);
            await context.SaveChangesAsync();

            var repository = new CiudadRepository(context);
            ciudadOriginal.Nombre = "Cartagena Actualizada";

            // ACT
            var resultado = await repository.PutCiudad(ciudadOriginal);

            // ASSERT
            resultado.Should().BeTrue();
            var ciudadActualizada = await context.Ciudad.FirstOrDefaultAsync(c => c.IdCiudad == 20);
            ciudadActualizada.Should().NotBeNull();
            ciudadActualizada!.Nombre.Should().Be("Cartagena Actualizada");
        }

        [Fact]
        public async Task PutCiudad_NoExiste_RetornaFalse()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new CiudadRepository(context);
            var ciudadInexistente = new Ciudad { IdCiudad = 999, Nombre = "No Existe" };

            // ACT
            var resultado = await repository.PutCiudad(ciudadInexistente);

            // ASSERT
            resultado.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteCiudad_Existe_EliminaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var ciudad = new Ciudad { IdCiudad = 30, Nombre = "Bucaramanga" };
            context.Ciudad.Add(ciudad);
            await context.SaveChangesAsync();

            var repository = new CiudadRepository(context);

            // ACT
            var resultado = await repository.DeleteCiudad(ciudad);

            // ASSERT
            resultado.Should().BeTrue();
            var ciudadEnBd = await context.Ciudad.FirstOrDefaultAsync(c => c.IdCiudad == 30);
            ciudadEnBd.Should().BeNull();
        }
    }
}
