using Microsoft.AspNetCore.Mvc;
using Web_Api.DTOs;
using Web_Api.Services;

[Route("api/[controller]")]
[ApiController]
public class VehiculoController : ControllerBase
{
    private readonly VehiculoService _vehiculoService;

    public VehiculoController(VehiculoService vehiculoService)
    {
        _vehiculoService = vehiculoService;
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> RegistrarVehiculo([FromBody] VehiculoRegistroDTO dto)
    {
        bool registrado = await _vehiculoService.RegistrarVehiculo(dto);
        if (registrado)
            return Ok(new { existe = true, mensaje = "Vehículo registrado correctamente" });
        else
            return BadRequest(new { existe = false, mensaje = "Ya existe un vehículo con esa placa" });
    }

    [HttpPut("Actualizar/{idVehiculo}")]
    public async Task<IActionResult> EditarVehiculo(int idVehiculo, [FromBody] VehiculoEdicionDTO dto)
    {
        var resultado = await _vehiculoService.EditarVehiculo(idVehiculo, dto);
        if (!resultado) { 
            return NotFound(new { existe = false, mensaje = "Vehículo no encontrado o no se pudo actualizar." });
        }
        return Ok(new { existe = true, mensaje = "Vehículo actualizado correctamente." });

    }



    [HttpGet("listar")]
    public async Task<IActionResult> ListarVehiculos()
    {
        var lista = await _vehiculoService.ListarVehiculos();
        return Ok(lista);
    }


    [HttpGet("listarPaginas")]
    public async Task<IActionResult> ListarVehiculos([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var resultado = await _vehiculoService.ListarVehiculosPaginado(page, pageSize);
        return Ok(resultado);
    }


}
