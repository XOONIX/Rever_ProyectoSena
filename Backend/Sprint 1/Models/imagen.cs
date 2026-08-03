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
        public int id_imagen { get; set; }
        public string url { get; set; }
        public int id_inmueble { get; set; }
    }
}