using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Api.DTOs;
using static ConductorDAO;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConductorController : ControllerBase
    {

        private readonly ConductorService _service;

        public ConductorController(ConductorService service)
        {
            _service = service;
        }



        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarConductor([FromBody] ConductorRegistroDTO dto)
        {
            bool registrado=await _service.RegistrarConductorAsync(dto);

            if (!registrado)
            {
                return BadRequest(new { existe = false, mensaje = "Error al Registrar Conductor. Verifique id_personal si existe?" });
            }

            return Ok(new { existe = true, mensaje = "Conductor registrado exitosamente" });

        }



        [HttpPost("validar-licencia")]
        public async Task<IActionResult> ValidarLicencia([FromBody] ValidarLicenciaDTO dto)
        {
            bool vigente = await _service.LicenciaVigenteAsync(dto.IdConductor);
            if (!vigente)
                return NotFound(new { exito = false, mensaje = "No se econtro Registro Conductor.!" });

            return Ok(new { exito = true, mensaje = "Conductor con Licencia registrada.!" });

        }


        [HttpPost("asignar-conductor")]
        public async Task<IActionResult> AsignarConductor([FromBody] AsignarConductorDTO dto)
        {
            bool asignado = await _service.AsignarConductorAsync(dto);

            if (!asignado)
                return BadRequest(new { exito = false, mensaje = "Mantenimiento no encontrado o no se pudo asignar conductor." });

            return Ok(new { exito = true, mensaje = "Conductor asignado correctamente al mantenimiento." });
        }


        [HttpPut("editar")]
        public async Task<IActionResult> EditarConductor([FromBody] ConductorEdicionDTO dto)
        {
            bool actualizado = await _service.EditarConductorAsync(dto);

            if (!actualizado)
                return BadRequest(new { exito = false, mensaje = "Conductor no encontrado o no se pudo actualizar." });

            return Ok(new { exito = true, mensaje = "Conductor actualizado correctamente." });
        }


        [HttpGet("listar")]
        public async Task<IActionResult> ListarConductores([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var resultado = await _service.ListarConductorPaginado(page, pageSize);
            return Ok(resultado);
        }


    }

}
