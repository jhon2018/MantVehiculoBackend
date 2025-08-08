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


    public async Task<bool> EditarVehiculo(int idVehiculo, VehiculoEdicionDTO dto)
    {
        var vehiculoExistente = await _vehiculoDAO.ObtenerPorId(idVehiculo);
        if (vehiculoExistente == null)
            return false;

        vehiculoExistente.placa = dto.placa;
        vehiculoExistente.marca = dto.marca;
        vehiculoExistente.modelo = dto.modelo;
        vehiculoExistente.fecha_compra = dto.fecha_compra.HasValue
            ? DateOnly.FromDateTime(dto.fecha_compra.Value)
            : vehiculoExistente.fecha_compra;

        return await _vehiculoDAO.ActualizarVehiculo(vehiculoExistente);
    }



    public async Task<List<VehiculoListadoDTO>> ListarVehiculos()
    {
        var lista = await _vehiculoDAO.ListarVehiculos();

        return lista.Select(v => new VehiculoListadoDTO
        {
            placa = v.placa,
            marca = v.marca,
            modelo = v.modelo,
            fecha_compra = v.fecha_compra?.ToString("yyyy-MM-dd") ?? ""
        }).ToList();
    }

    public async Task<VehiculoListadoPaginadoDTO> ListarVehiculosPaginado(int page, int pageSize)
    {
        var total = await _vehiculoDAO.ContarVehiculos();
        var vehiculos = await _vehiculoDAO.ListarVehiculosPaginado(page, pageSize);

        var listaDTO = vehiculos.Select(v => new VehiculoListadoDTO
        {
            id_Vehiculo = v.id_Vehiculo,
            placa = v.placa,
            marca = v.marca,
            modelo = v.modelo,
            fecha_compra = v.fecha_compra?.ToString("yyyy-MM-dd") ?? ""
        }).ToList();

        return new VehiculoListadoPaginadoDTO
        {
            totalRegistros = total,
            paginaActual = page,
            registrosPorPagina = pageSize,
            vehiculos = listaDTO
        };
    }


}
