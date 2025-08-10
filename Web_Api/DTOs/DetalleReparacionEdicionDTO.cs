namespace Web_Api.DTOs
{
    public class DetalleReparacionEdicionDTO
    {
        public int idDetalleReparacion { get; set; } // id_detalleReparacion
        public string Descripcion { get; set; }
        //public decimal Costo { get; set; }
        public int IdTipoReparacion { get; set; }
    }
}
