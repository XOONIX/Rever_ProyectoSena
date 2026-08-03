using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntidadesJson.clases
{
    public class tipo_inmueble
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID Tipo")]
        public int id_tipo { get; set; }

        [Required(ErrorMessage = "El nombre del tipo de inmueble es obligatorio.")] 
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        [Display(Name = "Tipo de Inmueble")] 
        public string nombre { get; set; }
    }
}
