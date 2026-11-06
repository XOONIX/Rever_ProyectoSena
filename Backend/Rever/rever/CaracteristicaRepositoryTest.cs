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
    public class CaracteristicaRepositoryTests
    {
        private DatabaseService GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<DatabaseService>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new DatabaseService(options);
        }

        [Fact]
        public async Task GetCaracteristica_DebeRetornarListaDeCaracteristicas()
        {
            var dbName = Guid.NewGuid().ToString();
            using (var context = GetInMemoryDbContext(dbName))
            {
                context.Caracteristica.Add(new Caracteristica { IdCaracteristica = 1, Nombre = "Piscina" });
                context.Caracteristica.Add(new Caracteristica { IdCaracteristica = 2, Nombre = "Ascensor" });
                await context.SaveChangesAsync();
            }

            using var contextRepo = GetInMemoryDbContext(dbName);
            var repository = new CaracteristicaRepository(contextRepo);

            var resultado = await repository.GetCaracteristica();

            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetCaracteristicaById_DebeRetornarCaracteristica_CuandoExiste()
        {
            var dbName = Guid.NewGuid().ToString();
            using (var context = GetInMemoryDbContext(dbName))
            {
                context.Caracteristica.Add(new Caracteristica { IdCaracteristica = 1, Nombre = "Gimnasio" });
                await context.SaveChangesAsync();
            }

            using var contextRepo = GetInMemoryDbContext(dbName);
            var repository = new CaracteristicaRepository(contextRepo);

            var resultado = await repository.GetCaracteristicaById(1);

            resultado.Should().NotBeNull();
            resultado.Nombre.Should().Be("Gimnasio");
        }

        [Fact]
        public async Task PostCaracteristica_DebeAgregarYRetornarTrue()
        {
            var dbName = Guid.NewGuid().ToString();
            using var contextRepo = GetInMemoryDbContext(dbName);
            var repository = new CaracteristicaRepository(contextRepo);

            var nuevaCaracteristica = new Caracteristica { IdCaracteristica = 1, Nombre = "Seguridad 24h" };

            var resultado = await repository.PostCaracteristica(nuevaCaracteristica);

            resultado.Should().BeTrue();

            using var contextVerify = GetInMemoryDbContext(dbName);
            contextVerify.Caracteristica.Should().ContainSingle(c => c.Nombre == "Seguridad 24h");
        }

        [Fact]
        public async Task PutCaracteristica_DebeActualizarYRetornarTrue_CuandoExiste()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();

            // 1. Insertamos el dato inicial y cerramos este contexto para evitar conflictos de tracking
            using (var context = GetInMemoryDbContext(dbName))
            {
                context.Caracteristica.Add(new Caracteristica { IdCaracteristica = 1, Nombre = "Parqueadero" });
                await context.SaveChangesAsync();
            }

            // 2. Abrimos un nuevo contexto para la ejecución del repositorio
            using var contextRepo = GetInMemoryDbContext(dbName);
            var repository = new CaracteristicaRepository(contextRepo);

            var caracteristicaModificada = new Caracteristica { IdCaracteristica = 1, Nombre = "Parqueadero Cubierto" };

            // Act
            var resultado = await repository.PutCaracteristica(caracteristicaModificada);

            // Assert
            resultado.Should().BeTrue();

            using var contextVerify = GetInMemoryDbContext(dbName);
            var item = await contextVerify.Caracteristica.FindAsync(1);
            item.Nombre.Should().Be("Parqueadero Cubierto");
        }

        [Fact]
        public async Task PutCaracteristica_DebeRetornarFalse_CuandoNoExiste()
        {
            var dbName = Guid.NewGuid().ToString();
            using var contextRepo = GetInMemoryDbContext(dbName);
            var repository = new CaracteristicaRepository(contextRepo);

            var caracteristicaInexistente = new Caracteristica { IdCaracteristica = 99, Nombre = "Inexistente" };

            var resultado = await repository.PutCaracteristica(caracteristicaInexistente);

            resultado.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteCaracteristica_DebeEliminarYRetornarTrue()
        {
            var dbName = Guid.NewGuid().ToString();
            var item = new Caracteristica { IdCaracteristica = 1, Nombre = "Balcón" };

            using (var context = GetInMemoryDbContext(dbName))
            {
                context.Caracteristica.Add(item);
                await context.SaveChangesAsync();
            }

            using var contextRepo = GetInMemoryDbContext(dbName);
            var repository = new CaracteristicaRepository(contextRepo);

            var resultado = await repository.DeleteCaracteristica(item);

            resultado.Should().BeTrue();

            using var contextVerify = GetInMemoryDbContext(dbName);
            contextVerify.Caracteristica.Should().BeEmpty();
        }
    }
}