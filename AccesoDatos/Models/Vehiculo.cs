using System;
using System.Collections.Generic;

namespace AccesoDatos.Models;

public partial class Vehiculo
{
    public int id_Vehiculo { get; set; }

    public string placa { get; set; } = null!;

    public string? marca { get; set; }

    public string? modelo { get; set; }

    public DateOnly? fecha_compra { get; set; }

    public virtual ICollection<Mantenimiento> Mantenimiento { get; set; } = new List<Mantenimiento>();
}
