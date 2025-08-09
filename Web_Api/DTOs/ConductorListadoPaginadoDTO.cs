namespace Web_Api.DTOs
{
    public class ConductorListadoPaginadoDTO
    {
        public int totalRegistros { get; set; }
        public int paginaActual { get; set; }
        public int registrosPorPagina { get; set; }
        public List<ConductorListadoDTO> conductor { get; set; }

    }
}
