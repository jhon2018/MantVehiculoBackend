//Archivo Models/Usuario.cs
using System;
using System.Collections.Generic;

namespace AccesoDatos.Models;

public partial class Usuario
{
    public int id_Usuario { get; set; }

    public string correo { get; set; } = null!;

    public string clave_hash { get; set; } = null!;

    public string rol { get; set; } = null!;

    public bool? activo { get; set; }

    public DateTime? fecha_creacion { get; set; }

    public virtual ICollection<Personal> Personal { get; set; } = new List<Personal>();
}
