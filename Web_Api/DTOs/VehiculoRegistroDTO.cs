using System.ComponentModel.DataAnnotations;

namespace Web_Api.DTOs
{
    public class VehiculoRegistroDTO
    {
        public string placa { get; set; } = null!;
        public string marca { get; set; } = null!;
        public string modelo { get; set; } = null!;

        //[Required]
        public DateTime? fecha_compra { get; set; }//Esto obliga al cliente a enviar el campo fecha_compra, pero permite que esté vacío (null), lo cual puedes manejar en el servicio.
    }


}
