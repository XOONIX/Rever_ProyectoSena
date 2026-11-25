using System.ComponentModel.DataAnnotations;

namespace rever.Dtos
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        public required string Correo { get; set; }

        [Required(ErrorMessage = "El token de recuperación es obligatorio.")]
        public required string Token { get; set; }

        [Required(ErrorMessage = "La nueva contraseña es obligatoria.")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public required string NuevaContraseña { get; set; }
    }
}
