using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rever.Models
{
    [Table("barrios")]
    public class Barrio
    {
        [Key]
        [Column("id_barrio")]
        public int IdBarrio { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        [Column("nombre")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "la id de la ciudad es obligatoria.")]
        [Column("id_ciudad")]
        public required int IdCiudad { get; set; }

        [Required(ErrorMessage = "la id de la localidad es obligatoria.")]
        [Column("id_localidad")]
        public required int IdLocalidad { get; set; }

        [ForeignKey("IdCiudad")]
        public Ciudad? Ciudad { get; set; }

        [ForeignKey("IdLocalidad")]
        public Localidad? Localidad { get; set; }
    }
}
