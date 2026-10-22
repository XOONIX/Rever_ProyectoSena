using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.Repositories;
using rever.Tests.Helpers;
using Xunit;

namespace rever.Tests.Repositories
{
    public class EstadoPublicacionRepositoryTest
    {
        [Fact]
        public async Task GetEstadoPublicacion_ExistenEstados_RetornaLista()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.EstadoPublicacion.AddRange(
                new EstadoPublicacion { IdEstado = 1, Nombre = "Disponible" },
                new EstadoPublicacion { IdEstado = 2, Nombre = "Vendido" }
            );
            await context.SaveChangesAsync();

            var repository = new EstadoPublicacionRepository(context);

            // ACT
            var resultado = await repository.GetEstadoPublicacion();

            // ASSERT
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetEstadoPublicacionById_Existe_RetornaEstado()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.EstadoPublicacion.Add(new EstadoPublicacion { IdEstado = 1, Nombre = "Disponible" });
            await context.SaveChangesAsync();

            var repository = new EstadoPublicacionRepository(context);

            // ACT
            var resultado = await repository.GetEstadoPublicacionById(1);

            // ASSERT
            resultado.Should().NotBeNull();
            resultado!.Nombre.Should().Be("Disponible");
        }

        [Fact]
        public async Task GetEstadoPublicacionById_NoExiste_RetornaNull()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new EstadoPublicacionRepository(context);

            // ACT
            var resultado = await repository.GetEstadoPublicacionById(999);

            // ASSERT
            resultado.Should().BeNull();
        }

        [Fact]
        public async Task PostEstadoPublicacion_GuardaExitosamente()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new EstadoPublicacionRepository(context);
            var nuevo = new EstadoPublicacion { IdEstado = 3, Nombre = "Pausado" };

            // ACT
            var resultado = await repository.PostEstadoPublicacion(nuevo);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.EstadoPublicacion.FirstOrDefaultAsync(e => e.IdEstado == 3);
            enBd.Should().NotBeNull();
            enBd!.Nombre.Should().Be("Pausado");
        }

        [Fact]
        public async Task PutEstadoPublicacion_Existe_ActualizaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var estado = new EstadoPublicacion { IdEstado = 1, Nombre = "Activo" };
            context.EstadoPublicacion.Add(estado);
            await context.SaveChangesAsync();

            var repository = new EstadoPublicacionRepository(context);
            estado.Nombre = "Inactivo";

            // ACT
            var resultado = await repository.PutEstadoPublicacion(estado);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.EstadoPublicacion.FirstOrDefaultAsync(e => e.IdEstado == 1);
            enBd!.Nombre.Should().Be("Inactivo");
        }

        [Fact]
        public async Task PutEstadoPublicacion_NoExiste_RetornaFalse()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new EstadoPublicacionRepository(context);
            var estado = new EstadoPublicacion { IdEstado = 999, Nombre = "Inexistente" };

            // ACT
            var resultado = await repository.PutEstadoPublicacion(estado);

            // ASSERT
            resultado.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteEstadoPublicacion_Existe_EliminaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var estado = new EstadoPublicacion { IdEstado = 1, Nombre = "EliminarMe" };
            context.EstadoPublicacion.Add(estado);
            await context.SaveChangesAsync();

            var repository = new EstadoPublicacionRepository(context);

            // ACT
            var resultado = await repository.DeleteEstadoPublicacion(estado);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.EstadoPublicacion.FirstOrDefaultAsync(e => e.IdEstado == 1);
            enBd.Should().BeNull();
        }
    }
}
