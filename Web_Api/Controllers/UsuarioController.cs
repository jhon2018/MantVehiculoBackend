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


        [HttpPost("Login")]
        public async Task<IActionResult> Registrar([FromBody] UsuarioRegistroDTO dto)
        {
            bool resultado = await _usuarioService.registrarUsuarioCompleto(dto);
            return resultado ? Ok("Usuario registrado correctamente") : BadRequest("Ya existe el usuario");
        }


        [HttpPost("Autenticacion")]
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


        [HttpPatch("actualizarPassword")]// solo los campos que tu deseas
        public async Task<IActionResult> actualizarPassword([FromBody] ActualizarPasswordDTO actualizarPasswordDTO)
        {
  
            bool actualizado = await _usuarioService.actualizarPassword(actualizarPasswordDTO);

            if (!actualizado)
            {
                return BadRequest(new { existe = false, mensaje = "Error al actualizar la contraseña. Verifique sus credenciales." });
            }

            return Ok(new { existe = true, mensaje = "Contraseña actualizada correctamente" });

        }


        [HttpGet("buscar/{id_Usuario}")]
        public async Task<IActionResult> Buscar(int id_Usuario)
        {
            var usuario = await _usuarioService.BuscarUsuario(id_Usuario);
            if (usuario != null)
            {
                return Ok(usuario);
            }
            return NotFound(new { mensaje = "Usuario no encontrado" });
        }


        [HttpDelete("eliminar/{id_Usuario}")]
        public async Task<IActionResult> Eliminar(int id_Usuario)
        {
            bool eliminado = await _usuarioService.EliminarUsuario(id_Usuario);
            if (eliminado)
            {
                return Ok(new { mensaje = "Usuario eliminado correctamente" });
            }
            return NotFound(new { mensaje = "No se pudo eliminar el usuario" });
        }

        [HttpGet("UsuarioPersonal/{id_Usuario}")]
        public async Task<IActionResult> ObtenerPerfil(int id_Usuario)
        {
            var perfil = await _usuarioService.ObtenerPerfil(id_Usuario);
            if (perfil != null)
            {
                return Ok(perfil);
            }
            return NotFound(new { mensaje = "Usuario no encontrado" });
        }


    }
}
