using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntidadesJson.clases
{
    public class contacto
    {
        [Key]
        public int id_contacto { get; set; }

        [Required(ErrorMessage = "El comprador es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un comprador válido.")]
        public int id_comprador { get; set; }

        [Required(ErrorMessage = "El vendedor es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un vendedor válido.")]
        public int id_vendedor { get; set; }

        [Required(ErrorMessage = "El inmueble es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un inmueble válido.")]
        public int id_inmueble { get; set; }

        [Required(ErrorMessage = "El mensaje es obligatorio.")]
        [MinLength(5, ErrorMessage = "El mensaje debe tener al menos 5 caracteres.")]
        public string mensaje { get; set; }

        public DateTime fecha { get; set; }
    }
}
