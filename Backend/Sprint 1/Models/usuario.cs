using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntidadesJson.clases
{
    public class usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID Usuario")]
        public int id_usuario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        [Display(Name = "Nombre Completo")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [StringLength(150, ErrorMessage = "El correo no puede superar los 150 caracteres.")]
        [Display(Name = "Correo Electrónico")]
        public string correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos {2} caracteres.")]
        [DataType(DataType.Password)] 
        [Display(Name = "Contraseña")]
        public string contraseña { get; set; }

        [Phone(ErrorMessage = "El formato del número de teléfono no es válido.")]
        [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres.")]
        [Display(Name = "Teléfono")]
        public string telefono { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        [ForeignKey("rol")] 
        [Display(Name = "Rol de Usuario")]
        public int id_rol { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de Registro")]
        public DateTime fecha_registro { get; set; } = DateTime.Now;
    }
}
