using System;
using System.Collections.Generic;

namespace AccesoDatos.Models;

public partial class tipoReparacion
{
    public int id_tipoReparacion { get; set; }

    public string? descripcion { get; set; }

    public virtual ICollection<detalleReparacion> detalleReparacion { get; set; } = new List<detalleReparacion>();
}
