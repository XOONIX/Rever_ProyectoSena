using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rever.Models
{
    [Table("imagenes")]
    public class Imagen
    {
        [Key]
        [Column("id_imagen")]
        public int IdImagen { get; set; }

        [Required(ErrorMessage = "La URL es obligatoria.")]
        [StringLength(255)]
        [Column("url")]
        public string Url { get; set; }

        [Column("id_inmueble")]
        public int IdInmueble { get; set; }

        [ForeignKey("IdInmueble")]
        public virtual Inmueble? Inmueble { get; set; }
    }
}