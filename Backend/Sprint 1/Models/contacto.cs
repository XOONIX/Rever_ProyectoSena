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
        public int id_contacto { get; set; }
        public int id_comprador { get; set; }
        public int id_vendedor { get; set; }
        public int id_inmueble { get; set; }
        public string mensaje { get; set; }
        public DateTime fecha { get; set; }
    }
}