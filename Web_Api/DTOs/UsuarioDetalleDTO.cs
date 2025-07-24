namespace Web_Api.DTOs
{
    public class UsuarioDetalleDTO
    {
        public int id_Usuario { get; set; }
        public string correo { get; set; }
        public string rol { get; set; }
        public bool activo { get; set; }
        public string fecha_creacion { get; set; } // como texto para formato personalizado
        public string nombre_completo { get; set; }
        public string dni { get; set; }
        public string telefono { get; set; }
        public string cargo { get; set; }
    }
}
