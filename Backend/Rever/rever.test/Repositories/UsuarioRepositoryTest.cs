using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.Repositories;
using rever.Tests.Helpers;
using Xunit;

namespace rever.Tests.Repositories
{
    public class UsuarioRepositoryTest
    {
        private static Usuario CrearUsuarioPrueba(int id, string correo = "test@rever.com")
        {
            return new Usuario
            {
                IdUsuario = id,
                Nombre = $"Usuario {id}",
                Correo = correo,
                Contraseña = "Password123!",
                Telefono = "3001234567",
                IdRol = 1
            };
        }

        [Fact]
        public async Task GetUsuario_ExistenUsuarios_RetornaLista()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.Usuario.AddRange(
                CrearUsuarioPrueba(1, "user1@rever.com"),
                CrearUsuarioPrueba(2, "user2@rever.com")
            );
            await context.SaveChangesAsync();

            var repository = new UsuarioRepository(context);

            // ACT
            var resultado = await repository.GetUsuario();

            // ASSERT
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetUsuarioById_Existe_RetornaUsuario()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.Usuario.Add(CrearUsuarioPrueba(10, "juan@rever.com"));
            await context.SaveChangesAsync();

            var repository = new UsuarioRepository(context);

            // ACT
            var resultado = await repository.GetUsuarioById(10);

            // ASSERT
            resultado.Should().NotBeNull();
            resultado!.Correo.Should().Be("juan@rever.com");
        }

        [Fact]
        public async Task GetUsuarioById_NoExiste_RetornaNull()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new UsuarioRepository(context);

            // ACT
            var resultado = await repository.GetUsuarioById(999);

            // ASSERT
            resultado.Should().BeNull();
        }

        [Fact]
        public async Task GetByEmailWithRolAsync_Existe_RetornaUsuarioConRol()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var rol = new Rol { IdRol = 1, Nombre = "Administrador" };
            context.Rol.Add(rol);

            var usuario = CrearUsuarioPrueba(1, "admin@rever.com");
            usuario.IdRol = 1;
            context.Usuario.Add(usuario);
            await context.SaveChangesAsync();

            var repository = new UsuarioRepository(context);

            // ACT
            var resultado = await repository.GetByEmailWithRolAsync("admin@rever.com");

            // ASSERT
            resultado.Should().NotBeNull();
            resultado!.Correo.Should().Be("admin@rever.com");
            resultado.Rol.Should().NotBeNull();
            resultado.Rol!.Nombre.Should().Be("Administrador");
        }

        [Fact]
        public async Task GetByEmailWithRolAsync_NoExiste_RetornaNull()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new UsuarioRepository(context);

            // ACT
            var resultado = await repository.GetByEmailWithRolAsync("noexiste@rever.com");

            // ASSERT
            resultado.Should().BeNull();
        }

        [Fact]
        public async Task PostUsuario_GuardaExitosamente()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new UsuarioRepository(context);
            var nuevo = CrearUsuarioPrueba(5, "nuevo@rever.com");

            // ACT
            var resultado = await repository.PostUsuario(nuevo);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.Usuario.FirstOrDefaultAsync(u => u.IdUsuario == 5);
            enBd.Should().NotBeNull();
            enBd!.Correo.Should().Be("nuevo@rever.com");
        }

        [Fact]
        public async Task PutUsuario_Existe_ActualizaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var usuario = CrearUsuarioPrueba(1, "antes@rever.com");
            context.Usuario.Add(usuario);
            await context.SaveChangesAsync();

            var repository = new UsuarioRepository(context);
            var modificado = CrearUsuarioPrueba(1, "despues@rever.com");
            modificado.Nombre = "Nombre Actualizado";

            // ACT
            var resultado = await repository.PutUsuario(modificado);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.Usuario.FirstOrDefaultAsync(u => u.IdUsuario == 1);
            enBd!.Correo.Should().Be("despues@rever.com");
            enBd.Nombre.Should().Be("Nombre Actualizado");
        }

        [Fact]
        public async Task PutUsuario_NoExiste_RetornaFalse()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new UsuarioRepository(context);
            var inexistente = CrearUsuarioPrueba(999, "no@rever.com");

            // ACT
            var resultado = await repository.PutUsuario(inexistente);

            // ASSERT
            resultado.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteUsuario_Existe_EliminaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var usuario = CrearUsuarioPrueba(1, "borrar@rever.com");
            context.Usuario.Add(usuario);
            await context.SaveChangesAsync();

            var repository = new UsuarioRepository(context);

            // ACT
            var resultado = await repository.DeleteUsuario(usuario);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.Usuario.FirstOrDefaultAsync(u => u.IdUsuario == 1);
            enBd.Should().BeNull();
        }
    }
}
