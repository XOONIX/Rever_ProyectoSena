using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntidadesJson.clases
{
    public class rol
    {
        [Key]
        public int id_rol { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre del rol debe tener entre 3 y 50 caracteres.")]
        public string nombre { get; set; }
    }
}
