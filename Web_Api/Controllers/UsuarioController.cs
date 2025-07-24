using AccesoDatos.Operations;
using AccesoDatos.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Web_Api.Services;
using Web_Api.DTOs;
using System.Threading.Tasks;

namespace Web_Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly UsuarioService _usuarioService;
        //crea un constructor para inyectar el servicio
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] UsuarioRegistroDTO dto)
        {
            bool resultado = await _usuarioService.registrarUsuarioCompleto(dto);
            return resultado ? Ok("Usuario registrado correctamente") : BadRequest("Ya existe el usuario");
        }


        [HttpPost("autenticacion")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var usuario = await _usuarioService.login(loginDTO);
            Console.WriteLine($"Usuario 2 encontrado: {usuario?.correo ?? "No encontrado"}");
            if (usuario != null)
            {
                return Ok(new
                {
                    exito = true,
                    mensaje = "Credenciales correctas",
                    token = usuario.Token,
                    correo = new
                    {
                        Usuario = usuario.correo,
                        NombreCompleto = usuario.nombre_completo,
                        Rol = usuario.rol

                    }
                });
            }
            else
            {
                return Unauthorized(new { exito = false, mensaje = "Credenciales incorrectas" });
            }
        }



    }
}
