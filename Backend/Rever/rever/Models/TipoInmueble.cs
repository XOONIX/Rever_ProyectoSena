using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rever.Models
{
    [Table("tipos_inmueble")]
    public class TipoInmueble
    {
        [Key]
        [Column("id_tipo")]
        public int IdTipo { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50)]
        [Column("nombre")]
        public string Nombre { get; set; }
    }
}
