using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntidadesJson.clases
{
        public class imagen
    {
            [Key]
            [Required]
            public int id_imagen {get;set;}
            [Required]
            [StringLength(225, ErrorMessage = "se aceptan hasta 225 caracteres")]
            public string url {get;set;}
            [Required]
            [ForeignKey(id_inmueble)]
            public int id_inmueble {get;set;}
    }
}
