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
    public class CiudadRepositoryTests
    {
        private DatabaseService GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<DatabaseService>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new DatabaseService(options);
        }

        [Fact]
        public async Task GetCiudad_DebeRetornarListaDeCiudades()
        {
            var dbName = Guid.NewGuid().ToString();
            using (var context = GetInMemoryDbContext(dbName))
            {
                context.Ciudad.Add(new Ciudad { IdCiudad = 1, Nombre = "Bogotá" });
                context.Ciudad.Add(new Ciudad { IdCiudad = 2, Nombre = "Medellín" });
                await context.SaveChangesAsync();
            }

            using var contextRepo = GetInMemoryDbContext(dbName);
            var repository = new CiudadRepository(contextRepo);

            var resultado = await repository.GetCiudad();

            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetCiudadById_DebeRetornarCiudad_CuandoExiste()
        {
            var dbName = Guid.NewGuid().ToString();
            using (var context = GetInMemoryDbContext(dbName))
            {
                context.Ciudad.Add(new Ciudad { IdCiudad = 1, Nombre = "Cali" });
                await context.SaveChangesAsync();
            }

            using var contextRepo = GetInMemoryDbContext(dbName);
            var repository = new CiudadRepository(contextRepo);

            var resultado = await repository.GetCiudadById(1);

            resultado.Should().NotBeNull();
            resultado.Nombre.Should().Be("Cali");
        }

        [Fact]
        public async Task PostCiudad_DebeAgregarYRetornarTrue()
        {
            var dbName = Guid.NewGuid().ToString();
            using var contextRepo = GetInMemoryDbContext(dbName);
            var repository = new CiudadRepository(contextRepo);

            var nuevaCiudad = new Ciudad { IdCiudad = 1, Nombre = "Barranquilla" };

            var resultado = await repository.PostCiudad(nuevaCiudad);

            resultado.Should().BeTrue();

            using var contextVerify = GetInMemoryDbContext(dbName);
            contextVerify.Ciudad.Should().ContainSingle(c => c.Nombre == "Barranquilla");
        }

        [Fact]
        public async Task PutCiudad_DebeActualizarYRetornarTrue_CuandoExiste()
        {
            var dbName = Guid.NewGuid().ToString();

            using (var context = GetInMemoryDbContext(dbName))
            {
                context.Ciudad.Add(new Ciudad { IdCiudad = 1, Nombre = "Cartagena" });
                await context.SaveChangesAsync();
            }

            using var contextRepo = GetInMemoryDbContext(dbName);
            var repository = new CiudadRepository(contextRepo);

            var ciudadModificada = new Ciudad { IdCiudad = 1, Nombre = "Cartagena de Indias" };

            var resultado = await repository.PutCiudad(ciudadModificada);

            resultado.Should().BeTrue();

            using var contextVerify = GetInMemoryDbContext(dbName);
            var item = await contextVerify.Ciudad.FindAsync(1);
            item.Nombre.Should().Be("Cartagena de Indias");
        }

        [Fact]
        public async Task PutCiudad_DebeRetornarFalse_CuandoNoExiste()
        {
            var dbName = Guid.NewGuid().ToString();
            using var contextRepo = GetInMemoryDbContext(dbName);
            var repository = new CiudadRepository(contextRepo);

            var ciudadInexistente = new Ciudad { IdCiudad = 99, Nombre = "Inexistente" };

            var resultado = await repository.PutCiudad(ciudadInexistente);

            resultado.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteCiudad_DebeEliminarYRetornarTrue()
        {
            var dbName = Guid.NewGuid().ToString();
            var item = new Ciudad { IdCiudad = 1, Nombre = "Pereira" };

            using (var context = GetInMemoryDbContext(dbName))
            {
                context.Ciudad.Add(item);
                await context.SaveChangesAsync();
            }

            using var contextRepo = GetInMemoryDbContext(dbName);
            var repository = new CiudadRepository(contextRepo);

            var resultado = await repository.DeleteCiudad(item);

            resultado.Should().BeTrue();

            using var contextVerify = GetInMemoryDbContext(dbName);
            contextVerify.Ciudad.Should().BeEmpty();
        }
    }
}