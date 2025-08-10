using AccesoDatos.Models;
using AccesoDatos.Operations;
using Web_Api.DTOs;

namespace Web_Api.Services
{
    public class TipoReparacionService
    {
        private readonly TipoReparacionDAO _TipoReparacionDAO;

        public TipoReparacionService(TipoReparacionDAO tipoReparacion)
        {
            _TipoReparacionDAO = tipoReparacion;
        }

        public async Task<bool> RegistrarTipoReparacionAsync(TipoReparacionRegistroDTO dto)
        {
            var entidad = new tipoReparacion
            {
                descripcion = dto.descripcion
            };

            return await _TipoReparacionDAO.RegistrarTipoReparacionAsync(entidad);
        }


        public async Task<TipoReparacionListadoPaginadoDTO> ListarTipoReparacionPaginado(int page, int pageSize)
        {
            var total = await _TipoReparacionDAO.ContarTipoReparaciones();
            var tipos = await _TipoReparacionDAO.ListarTipoReparacionPaginado(page, pageSize);

            var listaDTO = tipos.Select(t => new TipoReparacionListadoDTO
            {
                id_tipoReparacion = t.id_tipoReparacion,
                descripcion = t.descripcion
            }).ToList();

            return new TipoReparacionListadoPaginadoDTO
            {
                totalRegistros = total,
                paginaActual = page,
                registrosPorPagina = pageSize,
                tipoReparacion = listaDTO
            };
        }

 
        public async Task<bool> EditarTipoReparacionAsync(tipoReparacion tiporeparacion)
        {
            if (string.IsNullOrWhiteSpace(tiporeparacion.descripcion)) return false;
            return await _TipoReparacionDAO.EditarAsync(tiporeparacion);
        }



    }

}
