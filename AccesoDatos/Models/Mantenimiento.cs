using System;
using System.Collections.Generic;

namespace AccesoDatos.Models;

public partial class Mantenimiento
{
    public int id_Mantenimiento { get; set; }

    public int? id_Vehiculo { get; set; }

    public int? id_Conductor { get; set; }

    public int? id_Proveedor { get; set; }

    public int? id_detalleReparacion { get; set; }

    public DateTime fecha_Mantenimiento { get; set; }

    public string? observacion { get; set; }

    public string? url_foto { get; set; }

    public virtual Conductor? id_ConductorNavigation { get; set; }

    public virtual Proveedor? id_ProveedorNavigation { get; set; }

    public virtual Vehiculo? id_VehiculoNavigation { get; set; }

    public virtual detalleReparacion? id_detalleReparacionNavigation { get; set; }
}
