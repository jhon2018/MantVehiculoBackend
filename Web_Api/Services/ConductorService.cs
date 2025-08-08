//ARCHIVO Web_Api/Services/ConductorService.cs
using Web_Api.DTOs;
using AccesoDatos.Operations;
using AccesoDatos.Models;

public class ConductorService
{
    private readonly ConductorDAO _ConductorDAO;

    public ConductorService(ConductorDAO ConductorDAO)
    {
        _ConductorDAO = ConductorDAO;
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

}
