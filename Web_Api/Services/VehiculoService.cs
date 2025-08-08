using AccesoDatos.Models;
using AccesoDatos.Operations;
using Microsoft.Win32;
using Web_Api.DTOs;

public class VehiculoService
{
    private readonly VehiculoDAO _vehiculoDAO;

    public VehiculoService(VehiculoDAO vehiculoDAO)
    {
        _vehiculoDAO = vehiculoDAO;
    }

    //✅ Si el cliente envía una fecha válida, se usa.
    //✅ Si el cliente envía null o lo deja vacío, se registra la fecha actual.
    public async Task<bool> RegistrarVehiculo(VehiculoRegistroDTO dto)
    {
        var fechaCompraFinal = dto.fecha_compra.HasValue
            ? DateOnly.FromDateTime(dto.fecha_compra.Value)
            : DateOnly.FromDateTime(DateTime.UtcNow); // o DateTime.Now si prefieres local

        var vehiculo = new Vehiculo
        {
            placa = dto.placa,
            marca = dto.marca,
            modelo = dto.modelo,
            fecha_compra = fechaCompraFinal
        };

        return await _vehiculoDAO.RegistrarVehiculo(vehiculo);
    }

}
