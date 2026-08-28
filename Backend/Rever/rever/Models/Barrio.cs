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
        public string Nombre { get; set; }

        [Column("id_ciudad")]
        public int IdCiudad { get; set; }

        [Column("id_localidad")]
        public int IdLocalidad { get; set; }

        [ForeignKey("IdCiudad")]
        public Ciudad? Ciudad { get; set; }

        [ForeignKey("IdLocalidad")]
        public Localidad? Localidad { get; set; }
    }
}
