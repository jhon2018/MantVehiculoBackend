using AccesoDatos.Context;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Operations;

public class VehiculoDAO
{
    private readonly db_abc1b8_jhtchecklist0725Context context = new db_abc1b8_jhtchecklist0725Context();

    public async Task<bool> RegistrarVehiculo(Vehiculo vehiculo)
    {
        bool existe = await context.Vehiculo.AnyAsync(v => v.placa == vehiculo.placa);
        if (existe) return false;

        await context.Vehiculo.AddAsync(vehiculo);
        await context.SaveChangesAsync();
        return true;
    }
}
