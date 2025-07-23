using System;
using System.Threading.Tasks;
using AccesoDatos.Models;
using AccesoDatos.Operations;

class Program
{
    static async Task Main(string[] args)
    {
        UsuarioDAO usuarioDAO = new UsuarioDAO();

        // Prueba de registro de usuario (asíncrono)
        Usuario nuevoUsuario = new Usuario
        {
            correo = "Jonathan@TranspCompany.jht",
            clave_hash = "12345678", // Se encriptará dentro del método
            rol = "admin",
            activo = true,
            fecha_creacion = DateTime.Now
        };

        bool registrado = await usuarioDAO.RegistrarUsuario(nuevoUsuario);
        Console.WriteLine(registrado
            ? "Usuario registrado correctamente."
            : "El usuario ya existe.");

        // Prueba de login (asíncrono)
        Usuario? usuarioLogueado = await usuarioDAO.Login("Jonathan@TranspCompany.jht", "12345678");
        if (usuarioLogueado != null)
        {
            Console.WriteLine($"Login exitoso. Bienvenido: {usuarioLogueado.correo}");
        }
        else
        {
            Console.WriteLine("Login fallido. Usuario o contraseña incorrectos.");
        }

        Console.ReadLine();
    }
}
