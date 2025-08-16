using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Api.DTOs;
using Web_Api.Services;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientoController : ControllerBase
    {

        private readonly MantenimientoService _service;

        public MantenimientoController(MantenimientoService service)
        {
            _service = service;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] MantenimientoDTO dto)
        {
            try
            {
                var registrado = await _service.RegistrarMantenimientoAsync(dto);

                if (!registrado)
                    return BadRequest(new { exito = false, mensaje = "Ya existe un mantenimiento similar registrado." });

                return Ok(new { exito = true, mensaje = "Mantenimiento registrado correctamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { exito = false, mensaje = ex.Message });
            }
        }

        [HttpPost("upload-foto")]
        public async Task<IActionResult> UploadFoto(IFormFile foto)
        {
            if (foto == null || foto.Length == 0)
                return BadRequest("Foto vacía");

            var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(foto.FileName).ToLower();

            if (!extensionesPermitidas.Contains(extension))
                return BadRequest("Formato de imagen no permitido");

            if (foto.Length > 5 * 1024 * 1024) // 5MB
                return BadRequest("La imagen excede el tamaño permitido,");

            var nombreArchivo = Guid.NewGuid() + extension;
            var ruta = Path.Combine("wwwroot/fotos", nombreArchivo);

            using var stream = new FileStream(ruta, FileMode.Create);
            await foto.CopyToAsync(stream);

            var url = $"{Request.Scheme}://{Request.Host}/fotos/{nombreArchivo}";
            return Ok(new { url });
        }




    }
}
