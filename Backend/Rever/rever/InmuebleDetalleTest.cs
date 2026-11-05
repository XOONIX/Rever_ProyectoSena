using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using rever.Dtos; // Namespace de tus DTOs

namespace ReverInmobiliaria.Tests
{
    public class InmuebleDetalleDtoTests
    {
        [Fact]
        public void InmuebleDetalleDto_DebeInicializarListasYAsignarPropiedadesCorrectamente()
        {
            // Arrange & Act (Instanciamos el DTO y asignamos valores a sus propiedades)
            var detalleDto = new InmuebleDetalleDto
            {
                IdInmueble = 1,
                Titulo = "Casa en venta en el norte",
                Descripcion = "Hermosa vista panorámica",
                Precio = 450000000m,
                Direccion = "Calle 127 # 7-15",
                Ubicacion = "Usaquén, Bogotá",
                Habitaciones = 3,
                Banos = 2,
                MetrosCuadrados = 120,
                Pisos = 2,
                Estrato = 5,
                Latitud = 4.7110m,
                Longitud = -74.0721m,
                Tipo = "Casa",
                Modo = "Venta",
                IdVendedor = 10,
                NombreVendedor = "Carlos Pérez",
                TelefonoVendedor = "3101234567",
                CorreoVendedor = "carlos@rever.com"
            };

            // Agregamos elementos a las listas para verificar su comportamiento
            detalleDto.Imagenes.Add("imagen1.jpg");
            detalleDto.Caracteristicas.Add("Piscina");

            // Assert (Verificamos mediante caja blanca que los datos internos coinciden)
            detalleDto.IdInmueble.Should().Be(1);
            detalleDto.Titulo.Should().Be("Casa en venta en el norte");
            detalleDto.Precio.Should().Be(450000000m);
            detalleDto.Ubicacion.Should().Be("Usaquén, Bogotá");

            // Verificamos las listas
            detalleDto.Imagenes.Should().ContainSingle().And.Contain("imagen1.jpg");
            detalleDto.Caracteristicas.Should().ContainSingle().And.Contain("Piscina");

            // Verificamos datos del vendedor
            detalleDto.NombreVendedor.Should().Be("Carlos Pérez");
            detalleDto.TelefonoVendedor.Should().Be("3101234567");
        }

        [Fact]
        public void InmuebleDetalleDto_ListasDebenIniciarVacias()
        {
            // Arrange & Act
            var detalleDto = new InmuebleDetalleDto();

            // Assert (Validamos que no inicien como nulas para evitar errores en el sistema)
            detalleDto.Imagenes.Should().NotBeNull();
            detalleDto.Imagenes.Should().BeEmpty();

            detalleDto.Caracteristicas.Should().NotBeNull();
            detalleDto.Caracteristicas.Should().BeEmpty();
        }
    }
}