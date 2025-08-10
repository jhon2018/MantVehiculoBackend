// ARCHIVO AccesoDatos/Models/detalleReparacion.cs
using System;
using System.Collections.Generic;

namespace AccesoDatos.Models;

public partial class detalleReparacion
{
    public int id_detalleReparacion { get; set; }

    public int? id_tipoReparacion { get; set; }

    public string? descripcion { get; set; }

    public virtual ICollection<Mantenimiento> Mantenimiento { get; set; } = new List<Mantenimiento>();

    public virtual tipoReparacion? id_tipoReparacionNavigation { get; set; }
}
