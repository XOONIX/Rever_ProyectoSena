using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.Repositories;
using rever.Tests.Helpers;
using Xunit;

namespace rever.Tests.Repositories
{
    public class ContactoRepositoryTest
    {
        [Fact]
        public async Task GetContacto_ExistenRegistros_RetornaLista()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.Contacto.AddRange(
                new Contacto { IdContacto = 1, IdComprador = 1, IdVendedor = 2, IdInmueble = 10, Mensaje = "Hola, me interesa" },
                new Contacto { IdContacto = 2, IdComprador = 3, IdVendedor = 2, IdInmueble = 10, Mensaje = "¿Aún disponible?" }
            );
            await context.SaveChangesAsync();

            var repository = new ContactoRepository(context);

            // ACT
            var resultado = await repository.GetContacto();

            // ASSERT
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetContactoById_Existe_RetornaContacto()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.Contacto.Add(new Contacto { IdContacto = 5, IdComprador = 1, IdVendedor = 2, IdInmueble = 10, Mensaje = "Mensaje de prueba" });
            await context.SaveChangesAsync();

            var repository = new ContactoRepository(context);

            // ACT
            var resultado = await repository.GetContactoById(5);

            // ASSERT
            resultado.Should().NotBeNull();
            resultado!.Mensaje.Should().Be("Mensaje de prueba");
        }

        [Fact]
        public async Task GetContactoById_NoExiste_RetornaNull()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new ContactoRepository(context);

            // ACT
            var resultado = await repository.GetContactoById(999);

            // ASSERT
            resultado.Should().BeNull();
        }

        [Fact]
        public async Task PostContacto_GuardaExitosamente()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new ContactoRepository(context);
            var nuevo = new Contacto { IdContacto = 12, IdComprador = 4, IdVendedor = 5, IdInmueble = 20, Mensaje = "Quisiera agendar visita" };

            // ACT
            var resultado = await repository.PostContacto(nuevo);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.Contacto.FirstOrDefaultAsync(c => c.IdContacto == 12);
            enBd.Should().NotBeNull();
            enBd!.Mensaje.Should().Be("Quisiera agendar visita");
        }

        [Fact]
        public async Task PutContacto_Existe_ActualizaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var item = new Contacto { IdContacto = 1, IdComprador = 1, IdVendedor = 2, IdInmueble = 10, Mensaje = "Mensaje viejo" };
            context.Contacto.Add(item);
            await context.SaveChangesAsync();

            var repository = new ContactoRepository(context);
            item.Mensaje = "Mensaje editado";

            // ACT
            var resultado = await repository.PutContacto(item);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.Contacto.FirstOrDefaultAsync(c => c.IdContacto == 1);
            enBd!.Mensaje.Should().Be("Mensaje editado");
        }

        [Fact]
        public async Task PutContacto_NoExiste_RetornaFalse()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new ContactoRepository(context);
            var item = new Contacto { IdContacto = 999, IdComprador = 1, IdVendedor = 2, IdInmueble = 10, Mensaje = "No existe" };

            // ACT
            var resultado = await repository.PutContacto(item);

            // ASSERT
            resultado.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteContacto_Existe_EliminaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var item = new Contacto { IdContacto = 1, IdComprador = 1, IdVendedor = 2, IdInmueble = 10, Mensaje = "A borrar" };
            context.Contacto.Add(item);
            await context.SaveChangesAsync();

            var repository = new ContactoRepository(context);

            // ACT
            var resultado = await repository.DeleteContacto(item);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.Contacto.FirstOrDefaultAsync(c => c.IdContacto == 1);
            enBd.Should().BeNull();
        }
    }
}
