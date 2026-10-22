using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.Repositories;
using rever.Tests.Helpers;
using Xunit;

namespace rever.Tests.Repositories
{
    public class ModoTransaccionRepositoryTest
    {
        [Fact]
        public async Task GetAllAsync_ExistenModos_RetornaLista()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.ModoTransaccion.AddRange(
                new ModoTransaccion { IdModo = 1, Nombre = "Venta" },
                new ModoTransaccion { IdModo = 2, Nombre = "Arriendo" }
            );
            await context.SaveChangesAsync();

            var repository = new ModoTransaccionRepository(context);

            // ACT
            var resultado = await repository.GetAllAsync();

            // ASSERT
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetByIdAsync_Existe_RetornaModo()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.ModoTransaccion.Add(new ModoTransaccion { IdModo = 1, Nombre = "Venta" });
            await context.SaveChangesAsync();

            var repository = new ModoTransaccionRepository(context);

            // ACT
            var resultado = await repository.GetByIdAsync(1);

            // ASSERT
            resultado.Should().NotBeNull();
            resultado!.Nombre.Should().Be("Venta");
        }

        [Fact]
        public async Task GetByIdAsync_NoExiste_RetornaNull()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new ModoTransaccionRepository(context);

            // ACT
            var resultado = await repository.GetByIdAsync(999);

            // ASSERT
            resultado.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_GuardaExitosamente()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new ModoTransaccionRepository(context);
            var nuevo = new ModoTransaccion { IdModo = 3, Nombre = "Permuta" };

            // ACT
            var resultado = await repository.CreateAsync(nuevo);

            // ASSERT
            resultado.Should().NotBeNull();
            resultado.IdModo.Should().Be(3);
            var enBd = await context.ModoTransaccion.FirstOrDefaultAsync(m => m.IdModo == 3);
            enBd.Should().NotBeNull();
            enBd!.Nombre.Should().Be("Permuta");
        }

        [Fact]
        public async Task UpdateAsync_ActualizaYRetornaEntidad()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var modo = new ModoTransaccion { IdModo = 1, Nombre = "Venta" };
            context.ModoTransaccion.Add(modo);
            await context.SaveChangesAsync();

            var repository = new ModoTransaccionRepository(context);
            modo.Nombre = "Venta Directa";

            // ACT
            var resultado = await repository.UpdateAsync(modo);

            // ASSERT
            resultado.Nombre.Should().Be("Venta Directa");
            var enBd = await context.ModoTransaccion.FirstOrDefaultAsync(m => m.IdModo == 1);
            enBd!.Nombre.Should().Be("Venta Directa");
        }

        [Fact]
        public async Task DeleteAsync_Existe_EliminaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.ModoTransaccion.Add(new ModoTransaccion { IdModo = 1, Nombre = "Temporal" });
            await context.SaveChangesAsync();

            var repository = new ModoTransaccionRepository(context);

            // ACT
            var resultado = await repository.DeleteAsync(1);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.ModoTransaccion.FirstOrDefaultAsync(m => m.IdModo == 1);
            enBd.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_NoExiste_RetornaFalse()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new ModoTransaccionRepository(context);

            // ACT
            var resultado = await repository.DeleteAsync(999);

            // ASSERT
            resultado.Should().BeFalse();
        }
    }
}
