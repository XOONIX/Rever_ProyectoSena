using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntidadesJson.clases
{
        public class estado_publicacion
    {
            [Key]
            [Required]
            public int id_estado {get;set;}
            [Required]
            [StringLength(50, ErrorMessage = "solo se permiten 50 caracteres")]
            public string nombre {get;set;}
    
    }
}
