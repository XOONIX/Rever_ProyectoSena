using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntidadesJson.clases
{
    public class inmueble
    {
        public int id_inmueble { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public int id_tipo { get; set; }
        public string direccion { get; set; }
        public int id_barrio { get; set; }
        public int habitaciones { get; set; }
        public int baños { get; set; }
        public int metros_cuadrados { get; set; }
        public int estrato { get; set; }
        public decimal latitud { get; set; }
        public decimal longitud { get; set; }
        public int id_usuario { get; set; }
        public int id_estado { get; set; }
        public DateTime fecha_publicacion { get; set; }
    }
}