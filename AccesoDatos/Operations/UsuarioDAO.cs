using AccesoDatos.Context;
using AccesoDatos.Models;
using AccesoDatos.Plugins;


namespace AccesoDatos.Operations
{
    public class UsuarioDAO
    {
        private readonly db_abc1b8_jhtchecklist0725Context context = new db_abc1b8_jhtchecklist0725Context();



        public Usuario? Login(string correo, string password)
        {
            string passwordHash = HashUtil.ObtenerMD5(password); // Encriptar la contraseña

            var usuario = context.Usuario
                .FirstOrDefault(u => u.correo == correo && u.clave_hash == passwordHash);

            return usuario;
        }

        public bool RegistrarUsuario(Usuario usuario)
        {
            // Verifica si ya existe un usuario con el mismo correo
            bool yaExiste = context.Usuario.Any(u => u.correo == usuario.correo);
            if (yaExiste)
            {
                return false; // El usuario ya existe
            }

            // Encripta la contraseña antes de guardar
            usuario.clave_hash = HashUtil.ObtenerMD5(usuario.clave_hash);

            context.Usuario.Add(usuario); // Agrega el nuevo usuario
            context.SaveChanges(); // Guarda los cambios en la base de datos
            return true; // Registro exitoso
        }





        //
    }
}
