using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rever.Models
{
    [Table("inmuebles")]
    public class Inmueble
    {
        [Key]
        [Column("id_inmueble")]
        public int IdInmueble { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(150)]
        [Column("titulo")]
        public required string Titulo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [Column("descripcion", TypeName = "TEXT")]
        public required string Descripcion { get; set; }

        [Column("precio", TypeName = "decimal(12,2)")]
        public decimal Precio { get; set; }

        [Column("id_tipo")]
        public int IdTipo { get; set; }
        
        [Column("id_modo")]
        public int IdModo { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(200)]
        [Column("direccion")]
        public required string Direccion { get; set; }

        [Required(ErrorMessage = "El id del barrio es obligatorio.")]
        [Column("id_barrio")]
        public required int IdBarrio { get; set; }

        [Required(ErrorMessage = "El número de habitaciones es obligatorio.")]
        [Range(0, 10)]
        [Column("habitaciones")]
        public required int Habitaciones { get; set; }
        
        [Required(ErrorMessage = "El número de baños es obligatorio.")]
        [Range(0, 10)]
        [Column("baños")]
        public required int Baños { get; set; }

        [Required(ErrorMessage = "El número de metros cuadrados es obligatorio.")]
        [Column("metros_cuadrados")]   
        public required int MetrosCuadrados { get; set; }

        [Required(ErrorMessage = "El número de estrato es obligatorio.")]
        [Range(1, 6)]
        [Column("estrato")]
        public int Estrato { get; set; }

        [Required(ErrorMessage = "La latitud es obligatoria.")]
        [Column("latitud", TypeName = "decimal(10,8)")]
        public required decimal Latitud { get; set; }

        [Required(ErrorMessage = "La longitud es obligatoria.")]
        [Column("longitud", TypeName = "decimal(11,8)")]
        public required decimal Longitud { get; set; }

        [Required(ErrorMessage = "El id del usuario es obligatorio.")]
        [Column("id_usuario")]
        public required int IdUsuario { get; set; }

        [Column("id_estado")]
        public int IdEstado { get; set; } = 1;

        [Column("fecha_publicacion")]
        public DateTime FechaPublicacion { get; set; } = DateTime.Now;

        [ForeignKey("IdBarrio")]
        public Barrio? Barrio { get; set; }

        [ForeignKey("IdTipo")]
        public TipoInmueble? TipoInmueble { get; set; }

        [ForeignKey("IdModo")]
        public ModoTransaccion? ModoTransaccion { get; set; }

        [ForeignKey("IdEstado")]
        public EstadoPublicacion? EstadoPublicacion { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

        [InverseProperty("Inmueble")]
        public ICollection<Imagen> Imagenes { get; set; } = new List<Imagen>();

        [InverseProperty("Inmueble")]
        public ICollection<InmuebleCaracteristica> InmuebleCaracteristicas { get; set; } = new List<InmuebleCaracteristica>();
    }
}