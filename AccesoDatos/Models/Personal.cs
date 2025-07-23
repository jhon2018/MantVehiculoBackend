using System;
using System.Collections.Generic;

namespace AccesoDatos.Models;

public partial class Personal
{
    public int id_Personal { get; set; }

    public int? id_Usuario { get; set; }

    public string? nombre_completo { get; set; }

    public string? dni { get; set; }

    public string? telefono { get; set; }

    public string? cargo { get; set; }

    public virtual ICollection<Conductor> Conductor { get; set; } = new List<Conductor>();

    public virtual Usuario? id_UsuarioNavigation { get; set; }
}
