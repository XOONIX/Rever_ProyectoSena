using System.ComponentModel.DataAnnotations.Schema;

namespace rever.Models
{
    [Table("inmueble_caracteristica")]
    public class InmuebleCaracteristica
    {
        [Column("id_inmueble", Order = 0)]
        public int IdInmueble { get; set; }

        [Column("id_caracteristica", Order = 1)]
        public int IdCaracteristica { get; set; }

        [ForeignKey("IdInmueble")]
        public Inmueble? Inmueble { get; set; }

        [ForeignKey("IdCaracteristica")]
        public Caracteristica? Caracteristica { get; set; }
    }
}
