using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rever.Models
{
    public class ModoTransaccion
    {
        public int IdModo { get; set; }
        public string Nombre { get; set; }
    }
}
