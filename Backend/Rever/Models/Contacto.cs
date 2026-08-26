using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rever.Models
{
    [Table("contactos")]
    public class Contacto
    {
        [Key]
        [Column("id_contacto")]
        public int IdContacto { get; set; }

        [Required(ErrorMessage = "El comprador es obligatorio.")]
        [Column("id_comprador")]
        public int IdComprador { get; set; }

        [Required(ErrorMessage = "El vendedor es obligatorio.")]
        [Column("id_vendedor")]
        public int IdVendedor { get; set; }

        [Required(ErrorMessage = "El inmueble es obligatorio.")]
        [Column("id_inmueble")]
        public int IdInmueble { get; set; }

        [Required(ErrorMessage = "El mensaje es obligatorio.")]
        [MinLength(5)]
        [Column("mensaje", TypeName = "TEXT")]
        public string Mensaje { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        // OJO: dos FKs distintas apuntan a la misma tabla Usuario,
        // por eso cada una necesita su propio nombre de navegación.
        [ForeignKey("IdComprador")]
        public virtual Usuario? Comprador { get; set; }

        [ForeignKey("IdVendedor")]
        public virtual Usuario? Vendedor { get; set; }

        [ForeignKey("IdInmueble")]
        public virtual Inmueble? Inmueble { get; set; }
    }
}
