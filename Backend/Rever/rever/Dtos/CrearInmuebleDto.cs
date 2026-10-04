using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TuProyecto.DTOs
{
    public class CrearInmuebleDto
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(150, ErrorMessage = "El título no puede superar los 150 caracteres.")]
        public required string Titulo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public required string Descripcion { get; set; }

        [Range(0, 9999999999.99, ErrorMessage = "El precio debe ser un valor positivo.")]
        public decimal Precio { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecciona un tipo de inmueble válido.")]
        public int IdTipo { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecciona un modo de transacción válido.")]
        public int IdModo { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(200, ErrorMessage = "La dirección no puede superar los 200 caracteres.")]
        public required string Direccion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selecciona un barrio válido.")]
        public int IdBarrio { get; set; }

        [Range(0, 10, ErrorMessage = "Las habitaciones deben estar entre 0 y 10.")]
        public int Habitaciones { get; set; }

        // Mapea la 'n' de JS ('banos') a la propiedad C#
        [JsonPropertyName("banos")]
        [Range(0, 10, ErrorMessage = "Los baños deben estar entre 0 y 10.")]
        public int Banos { get; set; }

        [Range(1, 200, ErrorMessage = "El piso debe ser un número válido.")]
        public int Pisos { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage = "Los metros cuadrados deben ser mayores a 0.")]
        public int MetrosCuadrados { get; set; }

        [Range(1, 6, ErrorMessage = "El estrato debe estar entre 1 y 6.")]
        public int Estrato { get; set; }

        public decimal Latitud { get; set; } = 0;
        public decimal Longitud { get; set; } = 0;

        // Listas dinámicas para la relación de tablas
        public List<int> CaracteristicasIds { get; set; } = new();

        [JsonPropertyName("imagenesUrls")]
        public List<string> ImagenesUrls { get; set; } = new();
    }
}
