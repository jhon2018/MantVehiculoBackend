//ARCHIVO Web_Api/DTO/ConductorListadoDTO.cs
namespace Web_Api.DTOs
{
    public class ConductorListadoDTO
    {
        public int id_Conductor { get; set; }
        public int id_Personal { get; set; }

        // Datos de conductor
        public string licencia { get; set; }

        // Datos de personal
        public string nombre_completo { get; set; }
        public string dni { get; set; }
        public string telefono { get; set; }
        public string cargo { get; set; }

        // Datos de usuario
        public string correo { get; set; }
        public bool activo { get; set; }
    }
}
