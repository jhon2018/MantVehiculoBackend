using AccesoDatos.Models;
using AccesoDatos.Operations;
using Web_Api.DTOs;

namespace Web_Api.Services
{
    public class DetalleReparacionService
    {
        private readonly DetalleReparacionDAO _DetalleReparacionDAO;
        private readonly TipoReparacionDAO _TipoReparacionDAO;

        public DetalleReparacionService(DetalleReparacionDAO dao, TipoReparacionDAO tipoReparaciondao)
        {
            _DetalleReparacionDAO = dao;
            _TipoReparacionDAO = tipoReparaciondao;
        }

        public async Task<bool> RegistrarDetalleReparacionAsync(DetalleReparacionRegistroDTO dto)
        {
            int tipoId = await _DetalleReparacionDAO.ObtenerORegistrarTipoReparacionAsync(dto.tipoReparacionDescripcion);

            var entidad = new detalleReparacion
            {
                descripcion = dto.descripcion,
                id_tipoReparacion = tipoId
            };

            return await _DetalleReparacionDAO.RegistrarDetalleReparacionAsync(entidad);
        }

        public async Task<DetalleReparacionPorTipoDTO> ListarDetallePorTipoAsync(int id_tipoReparacion)
        {
            var detalles = await _DetalleReparacionDAO.ListarDetallePorTipoAsync(id_tipoReparacion);
            var tipo = await _TipoReparacionDAO.ObtenerTipoPorIdAsync(id_tipoReparacion);

            var listaDTO = detalles.Select(d => new DetalleReparacionListadoDTO
            {
                id_detalleReparacion = d.id_detalleReparacion,
                descripcion = d.descripcion
            }).ToList();

            return new DetalleReparacionPorTipoDTO
            {
                id_tipoReparacion = id_tipoReparacion,
                tipoDescripcion = tipo?.descripcion,
                detalles = listaDTO
            };
        }

        public async Task<bool> EditarDetalleReparacionAsync(detalleReparacion detalle)
        {
            if (string.IsNullOrWhiteSpace(detalle.descripcion)) return false;
            //if (detalle.costo <= 0) return false;
            if (detalle.id_tipoReparacion <= 0) return false;

            return await _DetalleReparacionDAO.EditarAsync(detalle);
        }


    }

}
