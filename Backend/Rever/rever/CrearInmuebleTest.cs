using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Xunit;
using TuProyecto.DTOs; 

namespace ReverInmobiliaria.Tests
{
    public class CrearInmuebleDtoTests
    {
        [Fact]
        public void Dto_DebeSerInvalido_CuandoElPrecioEsNegativo()
        {
            
            var dto = new CrearInmuebleDto
            {
                Titulo = "Casa campestre en el norte",
                Descripcion = "Amplia y fresca",
                Precio = -50000m, 
                IdTipo = 1,
                IdModo = 1,
                Direccion = "Calle 100 # 15-20",
                IdLocalidad = 1
            };

            var resultadosValidacion = new List<ValidationResult>();
            var contextoValidacion = new ValidationContext(dto, null, null);

             
            bool esValido = Validator.TryValidateObject(dto, contextoValidacion, resultadosValidacion, true);

             
            esValido.Should().BeFalse();
            resultadosValidacion.Should().Contain(v => v.MemberNames.Contains("Precio"));
        }

        [Fact]
        public void Dto_DebeSerValido_CuandoTodosLosCamposCumplenLasReglas()
        {
             
            var dto = new CrearInmuebleDto
            {
                Titulo = "Apartamento moderno",
                Descripcion = "Zona exclusiva",
                Precio = 350000000m,
                IdTipo = 1,
                IdModo = 1,
                Direccion = "Carrera 30",
                IdLocalidad = 2,
                Habitaciones = 3,
                Banos = 2,
                Pisos = 1,
                MetrosCuadrados = 85,
                Estrato = 4
            };

            var resultadosValidacion = new List<ValidationResult>();
            var contextoValidacion = new ValidationContext(dto, null, null);

           
            bool esValido = Validator.TryValidateObject(dto, contextoValidacion, resultadosValidacion, true);

            
            esValido.Should().BeTrue();
            resultadosValidacion.Should().BeEmpty();
        }
    }
}