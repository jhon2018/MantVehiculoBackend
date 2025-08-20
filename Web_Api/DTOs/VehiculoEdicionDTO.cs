using System.ComponentModel.DataAnnotations;

namespace Web_Api.DTOs
{
    public class VehiculoEdicionDTO
    {
        //[Required(ErrorMessage = "La placa es obligatoria.")]
        //public string placa { get; set; } = null!;

        [Required(ErrorMessage = "La marca es obligatoria.")]
        public string? marca { get; set; }

        [Required(ErrorMessage = "El modelo es obligatorio.")]
        public string? modelo { get; set; }

        [Required(ErrorMessage = "La fecha de compra es obligatoria.")]
        public DateTime? fecha_compra { get; set; }

        //public int id_vehiculo { get; set; }

    }

}
