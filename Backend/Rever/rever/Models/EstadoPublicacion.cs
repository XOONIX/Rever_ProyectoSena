using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rever.Models
{
    [Table("estados_publicacion")]
    public class EstadoPublicacion
    {
        [Key]
        [Column("id_estado")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50)]
        [Column("nombre")]
        public string Nombre { get; set; }
    }
}