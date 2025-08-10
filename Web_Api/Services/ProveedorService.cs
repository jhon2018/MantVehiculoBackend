using AccesoDatos.Models;
using Web_Api.Controllers;
using Web_Api.DTOs;

namespace Web_Api.Services
{
    public class ProveedorService
    {
        private readonly ProveedorDAO _proveedorDAO;

        public ProveedorService(ProveedorDAO proveedorDAO)
        {
            _proveedorDAO = proveedorDAO;
        }

        public async Task<bool> RegistrarProveedorAsync(ProveedorRegistroDTO dto)
        {
            var proveedor = new Proveedor
            {
                razon_social = dto.razon_social,
                ruc = dto.ruc,
                telefono = dto.telefono
            };

            return await _proveedorDAO.RegistrarProveedorAsync(proveedor);
        }


        public async Task<bool> EditarProveedorAsync(ProveedorEdicionDTO dto)
        {
            var proveedor = new Proveedor
            {
                id_Proveedor = dto.idProveedor,
                razon_social = dto.razonSocial,
                ruc = dto.ruc,
                telefono = dto.telefono
            };

            return await _proveedorDAO.EditarProveedorAsync(proveedor);
        }


        public async Task<List<Proveedor>> BuscarProveedorAsync(string filtro)
        {
            return await _proveedorDAO.BuscarProveedorAsync(filtro);
        }

        public async Task<ProveedorListadoPaginadoDTO> ListarProveedorPaginado(int page, int pageSize)
        {
            var total = await _proveedorDAO.ContarProveedores();
            var proveedores = await _proveedorDAO.ListarProveedorPaginado(page, pageSize);

            var listaDTO = proveedores.Select(p => new ProveedorListadoDTO
            {
                id_Proveedor = p.id_Proveedor,
                razon_social = p.razon_social,
                ruc = p.ruc,
                telefono = p.telefono
            }).ToList();

            return new ProveedorListadoPaginadoDTO
            {
                totalRegistros = total,
                paginaActual = page,
                registrosPorPagina = pageSize,
                proveedor = listaDTO
            };
        }


        //
    }

}
