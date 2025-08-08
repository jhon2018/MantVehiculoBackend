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


    //public async Task<bool> ActualizarVehiculo(Vehiculo vehiculo) //ACTUALIZAR POR ID Y PLACA busca esto en service  return await _vehiculoDAO.ActualizarVehiculo(vehiculoExistente);
    //{
    //    var existente = await context.Vehiculo.FirstOrDefaultAsync(v => v.placa == vehiculo.placa);
    //    if (existente == null) return false;

    //    existente.marca = vehiculo.marca;
    //    existente.modelo = vehiculo.modelo;
    //    existente.fecha_compra = vehiculo.fecha_compra;

    //    await context.SaveChangesAsync();
    //    return true;
    //}

    public async Task<bool> ActualizarVehiculo(Vehiculo vehiculo)
    {
        context.Vehiculo.Update(vehiculo);
        await context.SaveChangesAsync();
        return true;
    }


    public async Task<Vehiculo?> ObtenerPorPlaca(string placa)
    {
        return await context.Vehiculo.FirstOrDefaultAsync(v => v.placa == placa);
    }

    public async Task<Vehiculo?> ObtenerPorId(int idVehiculo)
    {
        return await context.Vehiculo.FirstOrDefaultAsync(v => v.id_Vehiculo == idVehiculo);
    }


    public async Task<List<Vehiculo>> ListarVehiculos()
    {
        return await context.Vehiculo.ToListAsync();
    }


    public async Task<int> ContarVehiculos()
    {
        return await context.Vehiculo.CountAsync();
    }

    public async Task<List<Vehiculo>> ListarVehiculosPaginado(int page, int pageSize)
    {
        return await context.Vehiculo
            .OrderBy(v => v.id_Vehiculo)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }


}
