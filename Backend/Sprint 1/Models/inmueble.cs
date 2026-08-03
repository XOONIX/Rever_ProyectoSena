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
            [Key]
            [Required]
            public int id_inmueble { get; set; }
            [Required]
            [StringLength(150, ErrorMessage = "solo se permiten 150 caracteres")]
            public string Titulo { get; set; }
            [Required]
            [StringLength(250, ErrorMessage = "solo se permiten 250 caracteres")]
            public string descripcion { get; set; }
            [Required] 
            public decimal precio { get; set; }
            [Required]
            public int id_tipo { get; set; }
            [Required]
            [StringLength(200, ErrorMessage = "solo se permiten 200 caracteres")]
            public string direccion { get; set; }
            [Required] 
            [ForeignKey(id_barrio)]
            public int id_barrio { get; set; }
            [Required] 
            [Range(0,10, ErrorMessage = "solo se aceptan respuesta de 0-10")]
            public int habitaciones { get; set; }
            [Required] 
            [Range(0,10, ErrorMessage = "solo se aceptan respuesta de 0-10")]
            public int baños { get; set; }
            [Column(TypeName = "decimal(9,6)")]
            [Required]
            public int metros_cuadrados { get; set; }
            [Range(1,6, ErrorMessage = "solo se aceptan respuesta de 1-6")]
            [Required]
            public int estrato { get; set; }
            [Column(TypeName = "decimal(9,6)")]
            [Required]
            public decimal latitud { get; set; }
            [Column(TypeName = "decimal(9,6)")]
            [Required]
            public decimal longitud { get; set; }
            [Required] 
            [ForeignKey(id_usuario)]
            public int id_usuario { get; set; }
            [Required] 
            [ForeignKey(id_estado)]
            public int id_estado { get; set; } = 1;
             [Required] 
            public DateTime FechaPublicacion { get; set; } = DateTime.Now;
        }
}
