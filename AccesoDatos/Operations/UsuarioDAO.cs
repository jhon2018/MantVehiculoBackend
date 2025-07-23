using AccesoDatos.Context;
using AccesoDatos.Models;
using AccesoDatos.Plugins;
using Microsoft.EntityFrameworkCore;


namespace AccesoDatos.Operations
{
    public class UsuarioDAO
    {
        private readonly db_abc1b8_jhtchecklist0725Context context = new db_abc1b8_jhtchecklist0725Context();

        //public Usuario? Login(string correo, string password)
        //{
        //    string passwordHash = HashUtil.ObtenerMD5(password); // Encriptar la contraseña

        //    var usuario = context.Usuario
        //        .FirstOrDefault(u => u.correo == correo && u.clave_hash == passwordHash);

        //    return usuario;
        //}

        public async Task<Usuario?> Login(string correo, string password)
        {
            string passwordHash = HashUtil.ObtenerMD5(password);

            var usuario = await context.Usuario
                .FirstOrDefaultAsync(u => u.correo == correo && u.clave_hash == passwordHash);

            return usuario;
        }


        public async Task<bool> RegistrarUsuario(Usuario usuario)
        {
            // Verifica si ya existe un usuario con el mismo correo
            bool yaExiste = await context.Usuario.AnyAsync(u => u.correo == usuario.correo);
            if (yaExiste)
            {
                return false; // El usuario ya existe
            }

            // Encripta la contraseña antes de guardar
            usuario.clave_hash = HashUtil.ObtenerMD5(usuario.clave_hash);

            // Agrega el nuevo usuario de forma asincrónica
            await context.Usuario.AddAsync(usuario);
            await context.SaveChangesAsync();

            return true; // Registro exitoso
        }


        public async Task<bool> ActualizarUsuario(Usuario usuario)
        {
            try
            {
                var usuarioExistente = await context.Usuario
                    .FirstOrDefaultAsync(u => u.id_Usuario == usuario.id_Usuario);

                if (usuarioExistente == null)
                    return false;

                usuarioExistente.correo = usuario.correo;
                usuarioExistente.rol = usuario.rol;
                usuarioExistente.activo = usuario.activo;
                usuarioExistente.fecha_creacion = usuario.fecha_creacion;

                if (!string.IsNullOrWhiteSpace(usuario.clave_hash))
                    usuarioExistente.clave_hash = HashUtil.ObtenerMD5(usuario.clave_hash);

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROlR ActualizarUsuario: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> EliminarUsuario(int id_Usuario)
        {
            try
            {
                var usuario = await context.Usuario.FirstOrDefaultAsync(u => u.id_Usuario == id_Usuario);
                if (usuario == null)
                    return false;

                context.Usuario.Remove(usuario);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR EliminarUsuario: {ex.Message}");
                return false;
            }
        }

        public async Task<Usuario?> BuscarUsuario(int id_Usuario)
        {
            try
            {
                return await context.Usuario.FirstOrDefaultAsync(u => u.id_Usuario == id_Usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR BuscarUsuario: {ex.Message}");
                return null;
            }
        }






        //
    }
}
