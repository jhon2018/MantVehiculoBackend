

using System;
using AccesoDatos.Models;
using AccesoDatos.Operations;

class Program
{
    static void Main(string[] args)
    {
        UsuarioDAO usuarioDAO = new UsuarioDAO();

        // Prueba de registro de usuario
        Usuario nuevoUsuario = new Usuario
        {
            correo = "prueba@TranspCompany.jht",
            clave_hash = "123456", // Se encriptará dentro del método
            rol = "admin",
            activo = true,
            fecha_creacion = DateTime.Now
        };

        bool registrado = usuarioDAO.RegistrarUsuario(nuevoUsuario);
        Console.WriteLine(registrado
            ? "Usuario registrado correctamente."
            : "El usuario ya existe.");

        // Prueba de login
        Usuario? usuarioLogueado = usuarioDAO.Login("prueba@TranspCompany.jht", "123456");
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