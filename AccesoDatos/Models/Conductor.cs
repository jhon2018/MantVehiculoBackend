// ARCHIVO AccesoDatos/Models/Conductor.cs
using System;
using System.Collections.Generic;

namespace AccesoDatos.Models;

public partial class Conductor
{
    public int id_Conductor { get; set; }

    public int? id_Personal { get; set; }

    public string? licencia { get; set; }

    public virtual ICollection<Mantenimiento> Mantenimiento { get; set; } = new List<Mantenimiento>();

    public virtual Personal? id_PersonalNavigation { get; set; }
}
