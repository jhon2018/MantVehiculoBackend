using System.ComponentModel.DataAnnotations;

namespace Web_Api.DTOs
{
    public class ActualizarPasswordDTO
    {

        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string correo { get; set; }

        [Required(ErrorMessage = "La contraseña actual es obligatoria")]
        public string PasswordActual { get; set; }
        [Required(ErrorMessage = "Nuevo Password es Obligatorio")]
        public string PasswordNuevo { get; set; }

    }
}
