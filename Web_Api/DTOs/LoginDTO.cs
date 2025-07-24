using System.ComponentModel.DataAnnotations;

namespace Web_Api.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        public string correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string password { get; set; }
    }

}
