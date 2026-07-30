using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntidadesJson.clases
{
    public class barrio
    {
        public int id_barrio { get; set; }
        public string nombre { get; set; }
        public int id_ciudad { get; set; }
        [Required(ErrorMessage = "Hola")]
        public int id_localidad { get; set; }
    }
}