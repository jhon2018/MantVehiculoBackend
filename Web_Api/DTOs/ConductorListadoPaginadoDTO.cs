//Web_Api/ConductorListadoPaginadoDTO.cs
namespace Web_Api.DTOs;
public class PaginacionConductorDTO
{
    public int TotalRegistros { get; set; }
    public int PaginaActual { get; set; }
    public int RegistrosPorPagina { get; set; }
    public List<ConductorListadoDTO> Conductor { get; set; }
}
