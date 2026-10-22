using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using rever.Models;
using rever.Repositories;
using rever.Tests.Helpers;
using TuProyecto.DTOs;
using Xunit;

namespace rever.Tests.Repositories
{
    public class InmuebleRepositoryTest
    {
        private static Inmueble CrearInmueble(int idInmueble, int idUsuario = 1, int idLocalidad = 1)
        {
            return new Inmueble
            {
                IdInmueble = idInmueble,
                Titulo = $"Inmueble {idInmueble}",
                Descripcion = "Hermosa propiedad",
                Precio = 250000000m,
                IdTipo = 1,
                IdModo = 1,
                Direccion = "Carrera 15 # 100-20",
                IdLocalidad = idLocalidad,
                Habitaciones = 3,
                Baños = 2,
                MetrosCuadrados = 85,
                Estrato = 4,
                Pisos = 1,
                Latitud = 4.678m,
                Longitud = -74.055m,
                IdUsuario = idUsuario,
                IdEstado = 1
            };
        }

        [Fact]
        public async Task GetInmueble_ExistenInmuebles_RetornaLista()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.Inmueble.AddRange(
                CrearInmueble(1),
                CrearInmueble(2)
            );
            await context.SaveChangesAsync();

            var repository = new InmuebleRepository(context);

            // ACT
            var resultado = await repository.GetInmueble();

            // ASSERT
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetInmuebleById_Existe_RetornaInmueble()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.Inmueble.Add(CrearInmueble(10));
            await context.SaveChangesAsync();

            var repository = new InmuebleRepository(context);

            // ACT
            var resultado = await repository.GetInmuebleById(10);

            // ASSERT
            resultado.Should().NotBeNull();
            resultado!.Titulo.Should().Be("Inmueble 10");
        }

        [Fact]
        public async Task GetInmuebleById_NoExiste_RetornaNull()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new InmuebleRepository(context);

            // ACT
            var resultado = await repository.GetInmuebleById(999);

            // ASSERT
            resultado.Should().BeNull();
        }

        [Fact]
        public async Task PostInmueble_GuardaExitosamente()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new InmuebleRepository(context);
            var nuevo = CrearInmueble(5);

            // ACT
            var resultado = await repository.PostInmueble(nuevo);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.Inmueble.FirstOrDefaultAsync(i => i.IdInmueble == 5);
            enBd.Should().NotBeNull();
            enBd!.Titulo.Should().Be("Inmueble 5");
        }

        [Fact]
        public async Task PutInmueble_Existe_ActualizaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var item = CrearInmueble(1);
            context.Inmueble.Add(item);
            await context.SaveChangesAsync();

            var repository = new InmuebleRepository(context);
            item.Titulo = "Título Editado";
            item.Precio = 300000000m;

            // ACT
            var resultado = await repository.PutInmueble(item);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.Inmueble.FirstOrDefaultAsync(i => i.IdInmueble == 1);
            enBd!.Titulo.Should().Be("Título Editado");
            enBd.Precio.Should().Be(300000000m);
        }

        [Fact]
        public async Task PutInmueble_NoExiste_RetornaFalse()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new InmuebleRepository(context);
            var inexistente = CrearInmueble(999);

            // ACT
            var resultado = await repository.PutInmueble(inexistente);

            // ASSERT
            resultado.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteInmueble_Existe_EliminaYRetornaTrue()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var item = CrearInmueble(1);
            context.Inmueble.Add(item);
            await context.SaveChangesAsync();

            var repository = new InmuebleRepository(context);

            // ACT
            var resultado = await repository.DeleteInmueble(item);

            // ASSERT
            resultado.Should().BeTrue();
            var enBd = await context.Inmueble.FirstOrDefaultAsync(i => i.IdInmueble == 1);
            enBd.Should().BeNull();
        }

        [Fact]
        public async Task GetListadoAsync_RetornaListadoProyectadoConRelaciones()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var ciudad = new Ciudad { IdCiudad = 1, Nombre = "Medellín" };
            var localidad = new Localidad { IdLocalidad = 1, Nombre = "Poblado", IdCiudad = 1, Ciudad = ciudad };
            var tipo = new TipoInmueble { IdTipo = 1, Nombre = "Apartamento" };
            var modo = new ModoTransaccion { IdModo = 1, Nombre = "Venta" };
            var caracteristica = new Caracteristica { IdCaracteristica = 1, Nombre = "Piscina" };

            context.Ciudad.Add(ciudad);
            context.Localidad.Add(localidad);
            context.TipoInmueble.Add(tipo);
            context.ModoTransaccion.Add(modo);
            context.Caracteristica.Add(caracteristica);

            var inmueble = CrearInmueble(1, idLocalidad: 1);
            inmueble.Localidad = localidad;
            inmueble.TipoInmueble = tipo;
            inmueble.ModoTransaccion = modo;
            inmueble.Imagenes.Add(new Imagen { IdImagen = 1, Url = "http://img.jpg", Portada = true, IdInmueble = 1 });
            inmueble.InmuebleCaracteristicas.Add(new InmuebleCaracteristica { IdInmueble = 1, IdCaracteristica = 1, Caracteristica = caracteristica });

            context.Inmueble.Add(inmueble);
            await context.SaveChangesAsync();

            var repository = new InmuebleRepository(context);

            // ACT
            var listado = (await repository.GetListadoAsync()).ToList();

            // ASSERT
            listado.Should().NotBeEmpty();
            var primero = listado.First();
            primero.IdInmueble.Should().Be(1);
            primero.Ciudad.Should().Be("Medellín");
            primero.Localidad.Should().Be("Poblado");
            primero.Tipo.Should().Be("Apartamento");
            primero.Modo.Should().Be("venta");
            primero.ImagenUrl.Should().Be("http://img.jpg");
            primero.Caracteristicas.Should().Contain("Piscina");
        }

        [Fact]
        public async Task GetDetalleAsync_Existe_RetornaDetalleCompleto()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var ciudad = new Ciudad { IdCiudad = 1, Nombre = "Bogotá" };
            var localidad = new Localidad { IdLocalidad = 1, Nombre = "Chapinero", IdCiudad = 1, Ciudad = ciudad };
            var tipo = new TipoInmueble { IdTipo = 1, Nombre = "Casa" };
            var modo = new ModoTransaccion { IdModo = 1, Nombre = "Arriendo" };
            var vendedor = new Usuario
            {
                IdUsuario = 5,
                Nombre = "Carlos Propietario",
                Correo = "carlos@rever.com",
                Contraseña = "pass",
                Telefono = "3101234567",
                IdRol = 1
            };

            context.Ciudad.Add(ciudad);
            context.Localidad.Add(localidad);
            context.TipoInmueble.Add(tipo);
            context.ModoTransaccion.Add(modo);
            context.Usuario.Add(vendedor);

            var inmueble = CrearInmueble(10, idUsuario: 5, idLocalidad: 1);
            inmueble.Localidad = localidad;
            inmueble.TipoInmueble = tipo;
            inmueble.ModoTransaccion = modo;
            inmueble.Usuario = vendedor;

            context.Inmueble.Add(inmueble);
            await context.SaveChangesAsync();

            var repository = new InmuebleRepository(context);

            // ACT
            var detalle = await repository.GetDetalleAsync(10);

            // ASSERT
            detalle.Should().NotBeNull();
            detalle!.IdInmueble.Should().Be(10);
            detalle.NombreVendedor.Should().Be("Carlos Propietario");
            detalle.CorreoVendedor.Should().Be("carlos@rever.com");
            detalle.Modo.Should().Be("arriendo");
        }

        [Fact]
        public async Task PostInmuebleCompleto_LocalidadNoExiste_LanzaArgumentException()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            var repository = new InmuebleRepository(context);
            var dto = new CrearInmuebleDto
            {
                Titulo = "Casa campestre",
                Descripcion = "Amplia",
                Direccion = "Km 5",
                IdLocalidad = 999 // Inexistente
            };

            // ACT
            var accion = async () => await repository.PostInmuebleCompleto(dto, idUsuario: 1);

            // ASSERT
            await accion.Should().ThrowAsync<ArgumentException>()
                .WithMessage("La localidad seleccionada no existe.");
        }

        [Fact]
        public async Task PostInmuebleCompleto_CaracteristicaNoExiste_LanzaArgumentException()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.Localidad.Add(new Localidad { IdLocalidad = 1, Nombre = "Centro", IdCiudad = 1 });
            await context.SaveChangesAsync();

            var repository = new InmuebleRepository(context);
            var dto = new CrearInmuebleDto
            {
                Titulo = "Apartamento",
                Descripcion = "Moderno",
                Direccion = "Calle 10",
                IdLocalidad = 1,
                CaracteristicasIds = new List<int> { 999 } // Inexistente
            };

            // ACT
            var accion = async () => await repository.PostInmuebleCompleto(dto, idUsuario: 1);

            // ASSERT
            await accion.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Una o más características no existen.");
        }

        [Fact]
        public async Task PostInmuebleCompleto_DatosValidos_GuardaInmuebleConImagenesYCaracteristicas()
        {
            // ARRANGE
            using var context = RepositoryTestHelper.CreateInMemoryContext();
            context.Localidad.Add(new Localidad { IdLocalidad = 1, Nombre = "Centro", IdCiudad = 1 });
            context.Caracteristica.Add(new Caracteristica { IdCaracteristica = 1, Nombre = "Garaje" });
            context.Caracteristica.Add(new Caracteristica { IdCaracteristica = 2, Nombre = "Vigilancia" });
            await context.SaveChangesAsync();

            var repository = new InmuebleRepository(context);
            var dto = new CrearInmuebleDto
            {
                Titulo = "Penthouse de Lujo",
                Descripcion = "Vista panorámica",
                Precio = 800000000m,
                IdTipo = 1,
                IdModo = 1,
                Direccion = "Avenida Principal # 1",
                IdLocalidad = 1,
                Habitaciones = 4,
                Banos = 3,
                Pisos = 2,
                MetrosCuadrados = 180,
                Estrato = 6,
                CaracteristicasIds = new List<int> { 1, 2 },
                ImagenesUrls = new List<string> { "http://foto1.jpg", "http://foto2.jpg" }
            };

            // ACT
            var inmuebleCreado = await repository.PostInmuebleCompleto(dto, idUsuario: 10);

            // ASSERT
            inmuebleCreado.Should().NotBeNull();
            inmuebleCreado!.Titulo.Should().Be("Penthouse de Lujo");

            var inmuebleEnBd = await context.Inmueble
                .Include(i => i.Imagenes)
                .Include(i => i.InmuebleCaracteristicas)
                .FirstOrDefaultAsync(i => i.IdInmueble == inmuebleCreado.IdInmueble);

            inmuebleEnBd.Should().NotBeNull();
            inmuebleEnBd!.Imagenes.Should().HaveCount(2);
            inmuebleEnBd.Imagenes.First(img => img.Url == "http://foto1.jpg").Portada.Should().BeTrue();
            inmuebleEnBd.InmuebleCaracteristicas.Should().HaveCount(2);
        }
    }
}
