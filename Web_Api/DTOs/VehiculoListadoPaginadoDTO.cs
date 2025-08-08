namespace Web_Api.DTOs
{
    public class VehiculoListadoPaginadoDTO
    {
        public int totalRegistros { get; set; }
        public int paginaActual { get; set; }
        public int registrosPorPagina { get; set; }
        public List<VehiculoListadoDTO> vehiculos { get; set; }
    }
}
