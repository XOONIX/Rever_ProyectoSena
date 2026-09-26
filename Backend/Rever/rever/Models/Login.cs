using System.ComponentModel.DataAnnotations;

namespace rever.Models
{
    public class Login
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        public required string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public required string Contraseña { get; set; }
    }
}