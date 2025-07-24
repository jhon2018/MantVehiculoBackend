using AccesoDatos.Context;
using AccesoDatos.Models;
using AccesoDatos.Plugins;
using Microsoft.EntityFrameworkCore;


namespace AccesoDatos.Operations
{
    public class UsuarioDAO
    {
        private readonly db_abc1b8_jhtchecklist0725Context context = new db_abc1b8_jhtchecklist0725Context();

    

        public async Task<Usuario?> Login(string correo, string password)
        {
            string passwordHash = HashUtil.ObtenerMD5(password);
            Console.WriteLine($"Intentando iniciar sesión con correo: {correo} y contraseña hash: {passwordHash} password:{password}");

            var usuario = await context.Usuario
          .Include(u => u.Personal) // 🔍 Asegura que cargue los datos personales
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


            // Agrega el nuevo usuario de forma asincrónica
            await context.Usuario.AddAsync(usuario);
            await context.SaveChangesAsync();

            return true; // Registro exitoso
        }


        public async Task<bool> RegistrarPersonal(Personal personal)
        {
            try
            {
                // Verifica que el usuario asociado exista
                bool usuarioExiste = await context.Usuario.AnyAsync(u => u.id_Usuario == personal.id_Usuario);
                if (!usuarioExiste)
                    return false;

                // Agrega el nuevo registro de personal
                await context.Personal.AddAsync(personal);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR RegistrarPersonal: {ex.Message}");
                return false;
            }
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
                var usuario = await context.Usuario
                    .Include(u => u.Personal) // incluir datos relacionados
                    .FirstOrDefaultAsync(u => u.id_Usuario == id_Usuario);

                if (usuario == null) return false;

                // Eliminar primero los personales asociados
                if (usuario.Personal.Any())
                {
                    context.Personal.RemoveRange(usuario.Personal);
                }

                // Luego eliminar el usuario
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
                return await context.Usuario
                    .Include(u => u.Personal)
                    .FirstOrDefaultAsync(u => u.id_Usuario == id_Usuario);
                    //usuario.Personal.FirstOrDefault()?.nombre_completo

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR BuscarUsuario: {ex.Message}");
                return null;
            }
        }


        public async Task<bool> actualizarPassword(string usuario, string nuevoPassword)
        {
            var user = context.Usuario.FirstOrDefault(p => p.correo == usuario);//buscar la primera coincidencia del usuario

            if (user == null)
            {
                return false;
            }
            user.clave_hash = HashUtil.ObtenerMD5(nuevoPassword);
            context.SaveChanges();
            return true;
        }



        //
    }
}
