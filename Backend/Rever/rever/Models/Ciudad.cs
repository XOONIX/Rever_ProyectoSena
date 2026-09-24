using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace rever.Models
{
    [Table("ciudades")]
    public class Ciudad
    {
        [Key]
        [Column("id_ciudad")]
        public int IdCiudad { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; }
    }
}
