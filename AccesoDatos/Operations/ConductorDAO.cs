//ARCHIVO AccesoDatos/Operations/ConductorDAO.cs
using AccesoDatos.Context;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;

public class ConductorDAO
{
    private readonly db_abc1b8_jhtchecklist0725Context context = new db_abc1b8_jhtchecklist0725Context();


    //SQL Server lanza una excepción por violación de la clave foránea y Swagger muestra un 500 como no hay try-cath
    public async Task<bool> RegistrarConductorAsync(Conductor dto)
    {
        try
        {
            var nuevoConductor = new Conductor
            {
                id_Personal = dto.id_Personal,
                licencia = dto.licencia
            };

            context.Conductor.Add(nuevoConductor);
            await context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"Error al registrar conductor, Revisa id_personal si existe.: {ex.Message}");
            return false;
        }
    }

    public async Task<Conductor> ObtenerConductorPorIdAsync(int idConductor)
    {
        return await context.Conductor
            .FirstOrDefaultAsync(c => c.id_Conductor == idConductor);
    }



}
