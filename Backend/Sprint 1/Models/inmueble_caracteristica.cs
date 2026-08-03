using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntidadesJson.clases
{
    [Table("inmueble_caracteristica")]
    public class inmueble_caracteristica
    {
        [Key]
        [Column("id_inmueble", Order = 0)]
        [Required(ErrorMessage = "El inmueble es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un inmueble válido.")]
        public int id_inmueble { get; set; }

        [Key]
        [Column("id_caracteristica", Order = 1)]
        [Required(ErrorMessage = "La característica es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una característica válida.")]
        public int id_caracteristica { get; set; }

        [ForeignKey(nameof(id_inmueble))]
        public virtual inmueble inmueble { get; set; } = null!;

        [ForeignKey(nameof(id_caracteristica))]
        public virtual caracteristicas caracteristicas { get; set; } = null!;
    }
}
