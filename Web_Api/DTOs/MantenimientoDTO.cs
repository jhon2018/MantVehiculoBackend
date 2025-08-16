namespace Web_Api.DTOs;
    using System;
using System.Collections.Generic;


    public partial class MantenimientoDTO
    {

        public int id_Vehiculo { get; set; }
        public int id_Conductor { get; set; }
        public int id_Proveedor { get; set; }
        public int id_detalleReparacion { get; set; }
        public DateTime fecha_Mantenimiento { get; set; }
        public string observacion { get; set; }
        public string url_foto { get; set; } // nombre del archivo generado por UploadFoto

    }

