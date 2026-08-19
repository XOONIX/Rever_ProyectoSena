using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rever.Models
{
    [Table("caracteristicas")]
    public class Caracteristica
    {
        [Key]
        [Column("id_caracteristica")]
        public int IdCaracteristica { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; }
    }
}
