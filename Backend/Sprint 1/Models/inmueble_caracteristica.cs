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
    public class InmuebleCaracteristica
    {
        [Key]
        [Column("id_inmueble", Order = 0)]
        public int IdInmueble { get; set; }

        [Key]
        [Column("id_caracteristica", Order = 1)]
        public int IdCaracteristica { get; set; }

        // Propiedades de navegación para las Llaves Foráneas
        [ForeignKey("IdInmueble")]
        public virtual inmueble inmueble { get; set; } = null!;

        [ForeignKey("Idcaracteristica")]
        public virtual caracteristicas caracteristicas { get; set; } = null!;
    }
}