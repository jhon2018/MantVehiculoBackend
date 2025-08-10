using AccesoDatos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Api.DTOs;
using Web_Api.Services;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleReparacionController : ControllerBase
    {
        private readonly DetalleReparacionService _service;

        public DetalleReparacionController(DetalleReparacionService service)
        {
            _service = service;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarDetalleReparacion([FromBody] DetalleReparacionRegistroDTO dto)
        {
            bool registrado = await _service.RegistrarDetalleReparacionAsync(dto);

            if (!registrado)
                return BadRequest(new { exito = false, mensaje = "No se pudo registrar el detalle." });

            return Ok(new { exito = true, mensaje = "Detalle de reparación registrado correctamente." });
        }

        [HttpGet("listar-por-tipo/{id_tipoReparacion}")]
        public async Task<IActionResult> ListarDetallePorTipo(int id_tipoReparacion)
        {
            var resultado = await _service.ListarDetallePorTipoAsync(id_tipoReparacion);

            if (resultado.detalles == null || !resultado.detalles.Any())
                return NotFound(new { existe = false, mensaje = "No hay detalles para este tipo de reparación." });

            return Ok(new
            {
                existe = true,
                tipo = resultado.tipoDescripcion,
                detalles = resultado.detalles
            });
        }

        [HttpPut("editar")]
        public async Task<IActionResult> Editar([FromBody] DetalleReparacionEdicionDTO dto)
        {
            var entidad = new detalleReparacion
            {
                id_detalleReparacion = dto.idDetalleReparacion,
                descripcion = dto.Descripcion,
                //costo = dto.Costo,
                id_tipoReparacion = dto.IdTipoReparacion
            };

            var resultado = await _service.EditarDetalleReparacionAsync(entidad);

            if (!resultado)
                return NotFound(new { exito = false, mensaje = "No se pudo editar el detalle de reparación" });

            return Ok(new { exito = true, mensaje = "Detalle de reparación editado correctamente" });
        }


    }
}
