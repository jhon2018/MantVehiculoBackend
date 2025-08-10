using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Api.DTOs;
using Web_Api.Services;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {


        private readonly ProveedorService proveedorService;

        public ProveedorController(ProveedorService proveedorService)
        {
            this.proveedorService = proveedorService;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarProveedor([FromBody] ProveedorRegistroDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            bool registrado = await proveedorService.RegistrarProveedorAsync(dto);
            if (registrado)
                return Ok(new { existe = true, mensaje = "Proveedor registrado correctamente." });
            else
                return BadRequest(new { existe = false, mensaje = "Ya existe un proveedor con ese RUC." });
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> EditarProveedor([FromBody] ProveedorEdicionDTO dto)
        {
            bool actualizado = await proveedorService.EditarProveedorAsync(dto);

            if (!actualizado)
                return BadRequest(new { existe = false, mensaje = "Proveedor no encontrado o RUC duplicado." });

            return Ok(new { existe = true, mensaje = "Proveedor actualizado correctamente." });
        }


        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarProveedor([FromQuery] string filtro)
        {
            
            var resultados = await proveedorService.BuscarProveedorAsync(filtro);

            if (resultados == null || !resultados.Any())
                return NotFound(new { existe = false, mensaje = "No se encontraron proveedores o ruc con ese criterio." });

            return Ok(new
            {
                existe = true,
                mensaje = $"Se encontraron {resultados.Count} proveedor(es).",
                proveedores = resultados.Select(p => new
                {
                    p.id_Proveedor,
                    p.razon_social,
                    p.ruc,
                    p.telefono
                })
            });
        }


        [HttpGet("listar-paginado")]
        public async Task<IActionResult> ListarProveedorPaginado([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var resultado = await proveedorService.ListarProveedorPaginado(page, pageSize);
            return Ok(resultado);
        }




        //
    }
}

