//ARCHIVO Web_Api/Services/ConductorService.cs
using Web_Api.DTOs;
using AccesoDatos.Operations;
using AccesoDatos.Models;

public class ConductorService
{
    private readonly ConductorDAO _ConductorDAO;
    private readonly MantenimientoDAO _mantenimientoDAO;


    public ConductorService(ConductorDAO ConductorDAO, MantenimientoDAO MantenimientoDAO)
    {
        _ConductorDAO = ConductorDAO;
        _mantenimientoDAO = MantenimientoDAO;

    }

 

    //Convertir el DTO a entidad en el servicio
    public async Task<bool> RegistrarConductorAsync(ConductorRegistroDTO dto)
    {
        var entidad = new Conductor
        {
            id_Personal = dto.id_Personal,
            licencia = dto.licencia
        };

       return await _ConductorDAO.RegistrarConductorAsync(entidad);
    }


    public async Task<bool> LicenciaVigenteAsync(int idConductor)
    {
        var conductor = await _ConductorDAO.ObtenerConductorPorIdAsync(idConductor);
        if (conductor == null || string.IsNullOrEmpty(conductor.licencia))
            return false;

        return true;
    }


    public async Task<bool> AsignarConductorAsync(AsignarConductorDTO dto)
    {
        return await _mantenimientoDAO.AsignarConductorAsync(dto.id_mantenimiento, dto.id_conductor);
    }


    public async Task<bool> EditarConductorAsync(ConductorEdicionDTO dto)
    {
        var entidad = new Conductor
        {
            id_Conductor = dto.id_Conductor,
            id_Personal = dto.id_Personal,
            licencia = dto.licencia
        };

        return await _ConductorDAO.EditarConductorAsync(entidad);
    }


    public async Task<ConductorListadoPaginadoDTO> ListarConductorPaginado(int page, int pageSize)
    {
        var total = await _ConductorDAO.ContarConductores();
        var conductors = await _ConductorDAO.ListarConductorPaginado(page, pageSize);

        var listaDTO = conductors.Select(c => new ConductorListadoDTO
        {
            id_Conductor = c.id_Conductor,
            id_Personal = (int)c.id_Personal,
            licencia = c.licencia

        }).ToList();

        return new ConductorListadoPaginadoDTO
        {
            totalRegistros = total,
            paginaActual = page,
            registrosPorPagina = pageSize,
            conductor = listaDTO
        };
    }



}
