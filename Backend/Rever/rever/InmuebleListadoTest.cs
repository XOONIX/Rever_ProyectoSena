using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using rever.Dtos; // Namespace de tus DTOs

namespace ReverInmobiliaria.Tests
{
    public class InmuebleListadoDtoTests
    {
        [Fact]
        public void InmuebleListadoDto_DebeAsignarPropiedadesCorrectamente()
        {
            // Arrange & Act (Instanciamos el DTO de listado y asignamos valores resumidos)
            var listadoDto = new InmuebleListadoDto
            {
                IdInmueble = 5,
                Titulo = "Apartamento amoblado en el centro",
                Precio = 180000000m,
                Ubicacion = "Centro, Bogotá",
                Habitaciones = 2,
                Banos = 1,
                MetrosCuadrados = 55.5,
                Pisos = 1,
                Tipo = "Apartamento",
                Modo = "Venta",
                Ciudad = "Bogotá",
                Localidad = "Santa Fe",
                ImagenUrl = "https://rever.com/img/inmueble5.jpg",
                Caracteristicas = new List<string> { "Ascensor", "Seguridad 24h" }
            };

            // Assert (Verificamos mediante caja blanca que los datos se almacenan tal cual se espera)
            listadoDto.IdInmueble.Should().Be(5);
            listadoDto.Titulo.Should().Be("Apartamento amoblado en el centro");
            listadoDto.Precio.Should().Be(180000000m);
            listadoDto.MetrosCuadrados.Should().Be(55.5);
            listadoDto.Ciudad.Should().Be("Bogotá");
            listadoDto.ImagenUrl.Should().NotBeNullOrEmpty();

            // Verificamos la lista de características
            listadoDto.Caracteristicas.Should().HaveCount(2);
            listadoDto.Caracteristicas.Should().Contain("Ascensor");
        }

        [Fact]
        public void InmuebleListadoDto_CaracteristicasPuedeSerNuloSiNoSeInicializa()
        {
            // Arrange & Act
            var listadoDto = new InmuebleListadoDto
            {
                IdInmueble = 6,
                Titulo = "Lote campestre",
                Precio = 90000000m
            };

            // Assert (Como no se inicializó con "= new()", evaluamos su comportamiento predeterminado)
            listadoDto.IdInmueble.Should().Be(6);
            listadoDto.Caracteristicas.Should().BeNull();
        }
    }
}