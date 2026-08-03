using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntidadesJson.clases
{
    public class localidad
    {
        [Key]
        public int id_localidad { get; set; }

        [Required(ErrorMessage = "El nombre de la localidad es obligatorio.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
        public string nombre { get; set; }
    }
}
