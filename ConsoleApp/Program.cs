using System;
using System.Threading.Tasks;
using AccesoDatos.Models;
using AccesoDatos.Operations;
using AccesoDatos.Plugins;

class Program
{
    static async Task Main(string[] args)
    {
        UsuarioDAO usuarioDAO = new UsuarioDAO();

        //// Prueba de registro de usuario (asíncrono)
        //Usuario nuevoUsuario = new Usuario
        //{
        //    correo = "Jonathan@TranspCompany.jht",
        //    clave_hash = "12345678", // Se encriptará dentro del método
        //    rol = "admin",
        //    activo = true,
        //    fecha_creacion = DateTime.Now
        //};

        //bool registrado = await usuarioDAO.RegistrarUsuario(nuevoUsuario);
        //Console.WriteLine(registrado
        //    ? "Usuario registrado correctamente."
        //    : "El usuario ya existe.");

        //// Prueba de login (asíncrono)
        //Usuario? usuarioLogueado = await usuarioDAO.Login("Jonathan@TranspCompany.jht", "12345678");
        //if (usuarioLogueado != null)
        //{
        //    Console.WriteLine($"Login exitoso. Bienvenido: {usuarioLogueado.correo}");
        //}
        //else
        //{
        //    Console.WriteLine("Login fallido. Usuario o contraseña incorrectos.");
        //}

        //Console.ReadLine();
        Console.WriteLine($"Hash local de '1234': {HashUtil.ObtenerMD5("1234")}");




        //// Prueba de actualización de usuario (asíncrono)
        //if (usuarioLogueado != null)
        //{
        //    // Modifica los campos que deseas actualizar
        //    usuarioLogueado.rol = "admin";
        //    usuarioLogueado.activo = false;
        //    usuarioLogueado.clave_hash = "nuevaClave123"; // Se encriptará si no está vacía

        //    bool actualizado = await usuarioDAO.ActualizarUsuario(usuarioLogueado);
        //    Console.WriteLine(actualizado
        //        ? "Usuario actualizado correctamente."
        //        : "No se pudo actualizar el usuario.");
        //}
        //else
        //{
        //    Console.WriteLine("No se puede actualizar porque el usuario no existe.");
        //}


        Usuario usuarioActualizado = new Usuario
        {
            id_Usuario = 1,
            correo = "nuevoCorreo@TranspCompany.jht",
            rol = "Conductor",
            activo = true,
            fecha_creacion = DateTime.Now,
            clave_hash = "nuevaClave123" // Se encriptará si no está vacía
        };

        bool actualizado = await usuarioDAO.ActualizarUsuario(usuarioActualizado);
        Console.WriteLine(actualizado
            ? "Usuario actualizado correctamente."
            : "No se pudo actualizar el usuario.");



    }
}
