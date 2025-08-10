using AccesoDatos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Api.DTOs;
using Web_Api.Services;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoReparacionController : ControllerBase
    {

        private readonly TipoReparacionService _service;

        public TipoReparacionController(TipoReparacionService service)
        {
            _service = service;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarTipoReparacion([FromBody] TipoReparacionRegistroDTO dto)
        {
            bool registrado = await _service.RegistrarTipoReparacionAsync(dto);

            if (!registrado)
                return BadRequest(new { existe = false, mensaje = "Ya existe un tipo de reparación con esa descripción." });

            return Ok(new { existe = true, mensaje = "Tipo de reparación registrado correctamente." });
        }


        [HttpGet("listar-paginado")]
        public async Task<IActionResult> ListarTipoReparacionPaginado([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var resultado = await _service.ListarTipoReparacionPaginado(page, pageSize);
            return Ok(resultado);
        }

        [HttpPut("editar")]
        public async Task<IActionResult> Editar([FromBody] TipoReparacionEdicionDTO dto)
        {
            var entidad = new tipoReparacion
            {
                id_tipoReparacion = dto.Id,
                descripcion = dto.Descripcion
            };

            var resultado = await _service.EditarTipoReparacionAsync(entidad);

            if (!resultado)
                return NotFound(new { exito = false, mensaje = "Tipo de reparación no encontrado o descripción inválida" });

            return Ok(new { exito = true, mensaje = "Tipo de reparación editado correctamente" });
        }


    }
}
