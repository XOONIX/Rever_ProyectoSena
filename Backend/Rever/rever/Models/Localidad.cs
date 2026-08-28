using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rever.Models
{
    [Table("localidad")]
    public class Localidad
    {
        [Key]
        [Column("id_localidad")]
        public int IdLocalidad { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, MinimumLength = 2)]
        [Column("nombre")]
        public string Nombre { get; set; }
    }
}
