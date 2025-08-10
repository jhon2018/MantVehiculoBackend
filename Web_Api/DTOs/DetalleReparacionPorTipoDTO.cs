namespace Web_Api.DTOs
{
    public class DetalleReparacionPorTipoDTO
    {
        public int id_tipoReparacion { get; set; }
        public string? tipoDescripcion { get; set; }
        public List<DetalleReparacionListadoDTO> detalles { get; set; } = new();
    }


}
