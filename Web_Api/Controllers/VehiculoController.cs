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
            return Ok(new { mensaje = "Vehículo registrado correctamente" });
        else
            return BadRequest(new { mensaje = "Ya existe un vehículo con esa placa" });
    }

    [HttpPut("Actualizar/{idVehiculo}")]
    public async Task<IActionResult> EditarVehiculo(int idVehiculo, [FromBody] VehiculoEdicionDTO dto)
    {
        var resultado = await _vehiculoService.EditarVehiculo(idVehiculo, dto);
        if (!resultado)
            return NotFound("Vehículo no encontrado o no se pudo actualizar.");

        return Ok("Vehículo actualizado correctamente.");
    }



    [HttpGet("listar")]
    public async Task<IActionResult> ListarVehiculos()
    {
        var lista = await _vehiculoService.ListarVehiculos();
        return Ok(lista);
    }

}
