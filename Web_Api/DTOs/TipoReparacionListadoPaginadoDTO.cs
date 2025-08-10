namespace Web_Api.DTOs
{
    public class TipoReparacionListadoPaginadoDTO
    {
        public int totalRegistros { get; set; }
        public int paginaActual { get; set; }
        public int registrosPorPagina { get; set; }
        public List<TipoReparacionListadoDTO> tipoReparacion { get; set; } = new();
    }

}
