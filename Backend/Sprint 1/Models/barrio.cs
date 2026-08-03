using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Barrio
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdBarrio { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public string Nombre { get; set; }

    [Required]
    public int IdCiudad { get; set; }

    [Required]
    public int IdLocalidad { get; set; }

    [ForeignKey(nameof(IdCiudad))]
    public Ciudad Ciudad { get; set; }

    [ForeignKey(nameof(IdLocalidad))]
    public Localidad Localidad { get; set; }
}
