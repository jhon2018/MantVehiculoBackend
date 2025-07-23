using System;
using System.Collections.Generic;

namespace AccesoDatos.Models;

public partial class Proveedor
{
    public int id_Proveedor { get; set; }

    public string? razon_social { get; set; }

    public string? ruc { get; set; }

    public string? telefono { get; set; }

    public virtual ICollection<Mantenimiento> Mantenimiento { get; set; } = new List<Mantenimiento>();
}
