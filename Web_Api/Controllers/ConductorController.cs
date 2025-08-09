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
            return Ok(new { licenciaVigente = vigente });
        }




    }

}
