using AccesoDatos.Models;

namespace Web_Api.DTOs
{
    public class UsuarioLoginRespuestaDTO
    {
        public string correo { get; set; }
        public string nombre_completo { get; set; }
        public string rol { get; set; }
        public string Token { get; set; }
    }
}
