using AccesoDatos.Models;
using AccesoDatos.Operations;
using AccesoDatos.Plugins;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web_Api.DTOs;
//Web_Api/Service/

namespace Web_Api.Services
{
    public class UsuarioService
    {
        private readonly UsuarioDAO _usuarioDAO;
        private string claveSecreta;
        public UsuarioService(UsuarioDAO usuarioDAO, IConfiguration configuration)
        {
            _usuarioDAO = usuarioDAO;
            claveSecreta = configuration.GetValue<string>("ApiSettings:Secreta");
        }


        public async Task<bool> registrarUsuarioCompleto(UsuarioRegistroDTO dto)
        {
            var correo = new Usuario
            {
                correo = dto.correo,
                clave_hash = HashUtil.ObtenerMD5(dto.password),
                rol = dto.rol,
                activo = true,
                fecha_creacion = DateTime.Now
            };

            bool registrado = await _usuarioDAO.RegistrarUsuario(correo);
            if (!registrado) return false;

            var personal = new Personal
            {
                id_Usuario = correo.id_Usuario,
                nombre_completo = dto.nombre_completo,
                dni = dto.dni,
                telefono = dto.telefono,
                cargo = dto.cargo
            };

            return await _usuarioDAO.RegistrarPersonal(personal);
        }




        public async Task<UsuarioLoginRespuestaDTO?> login(LoginDTO loginDTO)
        {
var usuario = await _usuarioDAO.Login(loginDTO.correo, loginDTO.password);
            Console.WriteLine($"Usuario encontrado: {usuario?.correo ?? "No encontrado"}");
            Console.WriteLine(loginDTO.correo + " " + loginDTO.password + " " + usuario?.clave_hash);
            if (usuario == null) { return null; }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario.correo),
                    new Claim("Nombre", usuario.correo)//Si usas correo.Nombre, cada token llevará el nombre real del correo autenticado.
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new UsuarioLoginRespuestaDTO
            {
                correo = usuario.correo,
                nombre_completo = usuario.Personal.FirstOrDefault()?.nombre_completo ?? "", // si tienes relación cargada
                rol = usuario.rol,
                Token = tokenString
            };
        }


        public async Task<bool> actualizarPassword(ActualizarPasswordDTO actualizarPasswordDTO)
        {
            var profesor = await _usuarioDAO.Login(actualizarPasswordDTO.correo, actualizarPasswordDTO.PasswordActual);
            if (profesor == null)
            {
                return false; // Usuario no encontrado o contraseña incorrecta
            }
            return await _usuarioDAO.actualizarPassword(actualizarPasswordDTO.correo, actualizarPasswordDTO.PasswordNuevo);
        }


        //

    }
}
