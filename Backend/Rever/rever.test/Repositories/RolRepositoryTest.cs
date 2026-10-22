using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.Repositories;
using rever.Tests.Helpers;
using Xunit;

namespace rever.Tests.Repositories
{
    public class RolRepositoryTest
    {
        [Fact]
        public async Task GetRol_ExistenRoles_RetornaLista()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.Rol.AddRange(
                new Rol { IdRol = 1, Nombre = "Administrador" },
                new Rol { IdRol = 2, Nombre = "Cliente" }
            );
            await context.SaveChangesAsync();

            var repository = new RolRepository(context);

            // ACT
            var resultado = await repository.GetRol();

            // ASSERT
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
            resultado.Should().Contain(r => r.Nombre == "Administrador");
        }

        [Fact]
        public async Task GetRol_SinRoles_RetornaListaVacia()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new RolRepository(context);

            // ACT
            var resultado = await repository.GetRol();

            // ASSERT
            resultado.Should().NotBeNull();
            resultado.Should().BeEmpty();
        }

        [Fact]
        public async Task GetRolById_Existe_RetornaRol()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.Rol.Add(new Rol { IdRol = 1, Nombre = "Administrador" });
            await context.SaveChangesAsync();

            var repository = new RolRepository(context);

            // ACT
            var resultado = await repository.GetRolById(1);

            // ASSERT
            resultado.Should().NotBeNull();
            resultado!.Nombre.Should().Be("Administrador");
        }

        [Fact]
        public async Task GetRolById_NoExiste_RetornaNull()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new RolRepository(context);

            // ACT
            var resultado = await repository.GetRolById(999);

            // ASSERT
            resultado.Should().BeNull();
        }

        [Fact]
        public async Task PostRol_DatosValidos_GuardaExitosamente()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new RolRepository(context);
            var nuevoRol = new Rol { IdRol = 3, Nombre = "Agente" };

            // ACT
            var resultado = await repository.PostRol(nuevoRol);

            // ASSERT
            resultado.Should().BeTrue();
            var rolEnBd = await context.Rol.FirstOrDefaultAsync(r => r.IdRol == 3);
            rolEnBd.Should().NotBeNull();
            rolEnBd!.Nombre.Should().Be("Agente");
        }

        [Fact]
        public async Task PutRol_Existe_ActualizaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var rol = new Rol { IdRol = 1, Nombre = "Admin" };
            context.Rol.Add(rol);
            await context.SaveChangesAsync();

            var repository = new RolRepository(context);
            rol.Nombre = "SuperAdmin";

            // ACT
            var resultado = await repository.PutRol(rol);

            // ASSERT
            resultado.Should().BeTrue();
            var rolActualizado = await context.Rol.FirstOrDefaultAsync(r => r.IdRol == 1);
            rolActualizado.Should().NotBeNull();
            rolActualizado!.Nombre.Should().Be("SuperAdmin");
        }

        [Fact]
        public async Task PutRol_NoExiste_RetornaFalse()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new RolRepository(context);
            var rol = new Rol { IdRol = 999, Nombre = "Inexistente" };

            // ACT
            var resultado = await repository.PutRol(rol);

            // ASSERT
            resultado.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteRol_Existe_EliminaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var rol = new Rol { IdRol = 2, Nombre = "Invitado" };
            context.Rol.Add(rol);
            await context.SaveChangesAsync();

            var repository = new RolRepository(context);

            // ACT
            var resultado = await repository.DeleteRol(rol);

            // ASSERT
            resultado.Should().BeTrue();
            var rolEnBd = await context.Rol.FirstOrDefaultAsync(r => r.IdRol == 2);
            rolEnBd.Should().BeNull();
        }
    }
}
