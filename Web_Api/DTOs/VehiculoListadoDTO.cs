
//ARCHIVO web_Api/DTOs/VehiculoListadoDTO.cs
namespace Web_Api.DTOs
{
    public class VehiculoListadoDTO
    {
        public int id_Vehiculo { get; set; } // Agregado para identificar el vehículo
        public string placa { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string fecha_compra { get; set; } // formato amigable
    }

}
