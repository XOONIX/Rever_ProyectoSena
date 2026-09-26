using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rever.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        [Column("nombre")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        [StringLength(100)]
        [Column("correo")]
        public required string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(255, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Column("contraseña")]
        public required string Contraseña { get; set; }

        [Required (ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El formato del teléfono no es válido.")]
        [StringLength(20)]
        [Column("telefono")]
        public required string Telefono { get; set; }

        [Required(ErrorMessage = "El id del rol es obligatorio.")]
        [Range(1, 3, ErrorMessage = "El id del rol debe estar entre 1 y 3.")]
        [Column("id_rol")]
        public required int IdRol { get; set; }

        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [ForeignKey("IdRol")]
        public Rol? Rol { get; set; }
    }
}
