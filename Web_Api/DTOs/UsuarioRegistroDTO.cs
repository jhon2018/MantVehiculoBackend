//ARCHIVO Web_Api/DTOs/UsuarioRegistroDTO.cs
using System.ComponentModel.DataAnnotations;

public class UsuarioRegistroDTO
{
    [Required(ErrorMessage = "El correo es obligatorio")]
    public string correo { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    public string password { get; set; } // ✅ Mucho más explícito

    [Required(ErrorMessage = "El Rol es obligatorio")]
    public string rol { get; set; }

    [Required(ErrorMessage = "El nombre completo es obligatorio")]
    public string nombre_completo { get; set; }

    [Required(ErrorMessage = "El DNI es obligatorio")]
    public string dni { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    public string telefono { get; set; }

    [Required(ErrorMessage = "El cargo es obligatorio")]
    public string cargo { get; set; }
}
