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
        public string Titulo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [Column("descripcion", TypeName = "TEXT")]
        public string Descripcion { get; set; }

        [Column("precio", TypeName = "decimal(12,2)")]
        public decimal Precio { get; set; }

        [Column("id_tipo")]
        public int IdTipo { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(200)]
        [Column("direccion")]
        public string Direccion { get; set; }

        [Column("id_barrio")]
        public int IdBarrio { get; set; }

        [Range(0, 10)]
        [Column("habitaciones")]
        public int Habitaciones { get; set; }

        [Range(0, 10)]
        [Column("baños")]
        public int Baños { get; set; }

        [Column("metros_cuadrados")]   
        public int MetrosCuadrados { get; set; }

        [Range(1, 6)]
        [Column("estrato")]
        public int Estrato { get; set; }

        [Column("latitud", TypeName = "decimal(10,8)")]
        public decimal Latitud { get; set; }

        [Column("longitud", TypeName = "decimal(11,8)")]
        public decimal Longitud { get; set; }

        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("id_estado")]
        public int IdEstado { get; set; } = 1;

        [Column("fecha_publicacion")]
        public DateTime FechaPublicacion { get; set; } = DateTime.Now;

        [ForeignKey("IdBarrio")]
        public virtual Barrio? Barrio { get; set; }

        [ForeignKey("IdTipo")]
        public virtual TipoInmueble? TipoInmueble { get; set; }

        [ForeignKey("IdEstado")]
        public virtual EstadoPublicacion? EstadoPublicacion { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario? Usuario { get; set; }
    }
}