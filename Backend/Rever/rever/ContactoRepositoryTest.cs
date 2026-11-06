using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using rever.contexto;
using rever.Models;
using rever.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ReverInmobiliaria.Tests
{
    public class ContactoRepositoryTests
    {
        private DatabaseService GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<DatabaseService>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new DatabaseService(options);
        }

        [Fact]
        public async Task GetContacto_DebeRetornarListaDeContactos()
        {
            var dbName = Guid.NewGuid().ToString();
            using (var context = GetInMemoryDbContext(dbName))
            {
                context.Contacto.Add(new Contacto { IdContacto = 1, Mensaje = "Hola, me interesa la propiedad" });
                context.Contacto.Add(new Contacto { IdContacto = 2, Mensaje = "Quiero agendar una cita" });
                await context.SaveChangesAsync();
            }

            using var contextRepo = GetInMemoryDbContext(dbName);
            var repository = new ContactoRepository(contextRepo);

            var resultado = await repository.GetContacto();

            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetContactoById_DebeRetornarContacto_CuandoExiste()
        {
            var dbName = Guid.NewGuid().ToString();
            using (var context = GetInMemoryDbContext(dbName))
            {
                context.Contacto.Add(new Contacto { IdContacto = 1, Mensaje = "Información sobre el precio" });
                await context.SaveChangesAsync();
            }

            using var contextRepo = GetInMemoryDbContext(dbName);
            var repository = new ContactoRepository(contextRepo);

            var resultado = await repository.GetContactoById(1);

            resultado.Should().NotBeNull();
            resultado.Mensaje.Should().Be("Información sobre el precio");
        }

        [Fact]
        public async Task PostContacto_DebeAgregarYRetornarTrue()
        {
            var dbName = Guid.NewGuid().ToString();
            using var contextRepo = GetInMemoryDbContext(dbName);
            var repository = new ContactoRepository(contextRepo);

            var nuevoContacto = new Contacto { IdContacto = 1, Mensaje = "Nuevo mensaje de contacto" };

            var resultado = await repository.PostContacto(nuevoContacto);

            resultado.Should().BeTrue();

            using var contextVerify = GetInMemoryDbContext(dbName);
            contextVerify.Contacto.Should().ContainSingle(c => c.Mensaje == "Nuevo mensaje de contacto");
        }

        [Fact]
        public async Task PutContacto_DebeActualizarYRetornarTrue_CuandoExiste()
        {
            var dbName = Guid.NewGuid().ToString();

            using (var context = GetInMemoryDbContext(dbName))
            {
                context.Contacto.Add(new Contacto { IdContacto = 1, Mensaje = "Mensaje original" });
                await context.SaveChangesAsync();
            }

            using var contextRepo = GetInMemoryDbContext(dbName);
            var repository = new ContactoRepository(contextRepo);

            var contactoModificado = new Contacto { IdContacto = 1, Mensaje = "Mensaje actualizado" };

            var resultado = await repository.PutContacto(contactoModificado);

            resultado.Should().BeTrue();

            using var contextVerify = GetInMemoryDbContext(dbName);
            var item = await contextVerify.Contacto.FindAsync(1);
            item.Mensaje.Should().Be("Mensaje actualizado");
        }

        [Fact]
        public async Task PutContacto_DebeRetornarFalse_CuandoNoExiste()
        {
            var dbName = Guid.NewGuid().ToString();
            using var contextRepo = GetInMemoryDbContext(dbName);
            var repository = new ContactoRepository(contextRepo);

            var contactoInexistente = new Contacto { IdContacto = 99, Mensaje = "Inexistente" };

            var resultado = await repository.PutContacto(contactoInexistente);

            resultado.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteContacto_DebeEliminarYRetornarTrue()
        {
            var dbName = Guid.NewGuid().ToString();
            var item = new Contacto { IdContacto = 1, Mensaje = "Para eliminar" };

            using (var context = GetInMemoryDbContext(dbName))
            {
                context.Contacto.Add(item);
                await context.SaveChangesAsync();
            }

            using var contextRepo = GetInMemoryDbContext(dbName);
            var repository = new ContactoRepository(contextRepo);

            var resultado = await repository.DeleteContacto(item);

            resultado.Should().BeTrue();

            using var contextVerify = GetInMemoryDbContext(dbName);
            contextVerify.Contacto.Should().BeEmpty();
        }
    }
}