using System.ComponentModel.DataAnnotations;

namespace rever.Dtos
{
    public class ForgotPasswordDto
    {
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        public required string Correo { get; set; }
    }
}
