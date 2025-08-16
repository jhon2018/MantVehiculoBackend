//ARCHIVO AccesoDatos/Services/MantenimientoService.cs
using AccesoDatos.Models;
using AccesoDatos.Operations;
using Web_Api.DTOs;

namespace Web_Api.Services
{
    public class MantenimientoService
    {
        private readonly MantenimientoDAO _mantenimientoDAO;

        public MantenimientoService(MantenimientoDAO dao)
        {
            _mantenimientoDAO = dao;
        }

        public async Task<bool> RegistrarMantenimientoAsync(MantenimientoDTO dto)
        {
            var mantenimiento = new Mantenimiento
            {
                id_Vehiculo = dto.id_Vehiculo,
                id_Conductor = dto.id_Conductor,
                id_Proveedor = dto.id_Proveedor,
                id_detalleReparacion = dto.id_detalleReparacion,
                fecha_Mantenimiento = dto.fecha_Mantenimiento,
                observacion = dto.observacion,
                url_foto = dto.url_foto
            };

            if (await _mantenimientoDAO.ExisteMantenimientoSimilarAsync(mantenimiento))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(dto.url_foto))
                throw new ArgumentException("La foto es obligatoria para registrar el mantenimiento.");


            return await _mantenimientoDAO.RegistrarAsync(mantenimiento);
        }





    }

}
