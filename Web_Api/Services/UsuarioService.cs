//ARCHIVO Web_Api/Services/UsuarioService.cs

using AccesoDatos.Models;
using AccesoDatos.Operations;
using AccesoDatos.Plugins;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web_Api.DTOs;


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
            bool correoExiste = await _usuarioDAO.ExisteCorreo(dto.correo);
            if (correoExiste) return false;

            bool dniExiste = await _usuarioDAO.ExisteDNI(dto.dni);
            if (dniExiste) return false;

            var usuario = new Usuario
            {
                correo = dto.correo,
                clave_hash = HashUtil.ObtenerMD5(dto.password),
                rol = dto.rol,
                activo = true,
                fecha_creacion = DateTime.Now
            };

            bool registrado = await _usuarioDAO.RegistrarUsuario(usuario);
            if (!registrado) return false;

            var personal = new Personal
            {
                id_Usuario = usuario.id_Usuario,
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




        public async Task<bool> EliminarUsuario(int id_Usuario)
        {
            return await _usuarioDAO.EliminarUsuario(id_Usuario);
        }


        public async Task<UsuarioDetalleDTO?> ObtenerPerfil(int id_Usuario)
        {
            var usuario = await _usuarioDAO.BuscarUsuario(id_Usuario);
            if (usuario == null) return null;

            var personal = usuario.Personal.FirstOrDefault();

            return new UsuarioDetalleDTO
            {
                id_Usuario = usuario.id_Usuario,
                correo = usuario.correo,
                rol = usuario.rol,
                activo = usuario.activo ?? false,
                fecha_creacion = usuario.fecha_creacion?.ToString("dd/MM/yyyy") ?? "",
                nombre_completo = personal?.nombre_completo ?? "",
                dni = personal?.dni ?? "",
                telefono = personal?.telefono ?? "",
                cargo = personal?.cargo ?? ""
            };
        }



        public async Task<List<UsuarioDetalleDTO>> ListarUsuarios()
        {
            var usuarios = await _usuarioDAO.ListarUsuarios();

            return usuarios.Select(u => new UsuarioDetalleDTO
            {
                id_Usuario = u.id_Usuario,
                correo = u.correo,
                rol = u.rol,
                activo = u.activo ?? false,
                fecha_creacion = u.fecha_creacion?.ToString("dd/MM/yyyy") ?? "",
                nombre_completo = u.Personal.FirstOrDefault()?.nombre_completo ?? "",
                dni = u.Personal.FirstOrDefault()?.dni ?? "",
                telefono = u.Personal.FirstOrDefault()?.telefono ?? "",
                cargo = u.Personal.FirstOrDefault()?.cargo ?? ""
            }).ToList();
        }




        //
    }
}
