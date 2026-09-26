using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rever.Models
{
    public class ModoTransaccion
    {
        [Key]
        [Column("id_modo")]
        public int IdModo { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        public required string Nombre { get; set; }
    }
}
