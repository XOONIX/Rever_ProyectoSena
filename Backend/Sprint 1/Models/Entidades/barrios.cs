using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rever.Models.Entidades
{
    public class barrios
    {
        public int id_barrios {  get; set; }

        public string nombre { get; set; }

        public int id_ciudad {  get; set; }

        public int id_localidad { get; set; }
    }
}
