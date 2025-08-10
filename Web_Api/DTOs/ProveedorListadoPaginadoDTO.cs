namespace Web_Api.DTOs
{
    public class ProveedorListadoPaginadoDTO
    {
        public int totalRegistros { get; set; }
        public int paginaActual { get; set; }
        public int registrosPorPagina { get; set; }
        public List<ProveedorListadoDTO> proveedor { get; set; } = new();
    }

}
