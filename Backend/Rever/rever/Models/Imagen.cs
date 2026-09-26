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
        public required string Url { get; set; }

        [Column("portada")]
        public bool Portada { get; set; }

        [Required(ErrorMessage = "El id del inmueble es obligatorio.")]
        [Column("id_inmueble")]
        public required int IdInmueble { get; set; }

        [ForeignKey("IdInmueble")]
        public Inmueble? Inmueble { get; set; }
    }
}