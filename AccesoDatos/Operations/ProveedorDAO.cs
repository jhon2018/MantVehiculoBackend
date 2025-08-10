using AccesoDatos.Context;
using AccesoDatos.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Web_Api.Controllers
{
    public class ProveedorDAO
    {
        private readonly db_abc1b8_jhtchecklist0725Context context = new db_abc1b8_jhtchecklist0725Context();


        public async Task<bool> RegistrarProveedorAsync(Proveedor proveedor)
        {
            bool existe = await context.Proveedor.AnyAsync(p => p.ruc == proveedor.ruc);
            if (existe) return false;

            await context.Proveedor.AddAsync(proveedor);
            await context.SaveChangesAsync();
            return true;
        }
    


    public async Task<bool> EditarProveedorAsync(Proveedor proveedor)
        {
            var entidad = await context.Proveedor.FindAsync(proveedor.id_Proveedor);
            if (entidad == null) return false;

            // Validar que el nuevo RUC no esté duplicado en otro proveedor
            bool rucDuplicado = await context.Proveedor
                .AnyAsync(p => p.ruc == proveedor.ruc && p.id_Proveedor != proveedor.id_Proveedor);
            if (rucDuplicado) return false;

            entidad.razon_social = proveedor.razon_social;
            entidad.ruc = proveedor.ruc;
            entidad.telefono = proveedor.telefono;

            await context.SaveChangesAsync();
            return true;
        }


        public async Task<List<Proveedor>> BuscarProveedorAsync(string filtro)
        {
            Console.Write("filtro", filtro);
            return await context.Proveedor
                
                .Where(p => p.razon_social.Contains(filtro) || p.ruc.Contains(filtro))
                .OrderBy(p => p.razon_social)
                .ToListAsync();
        }



        public async Task<int> ContarProveedores()
        {
            return await context.Proveedor.CountAsync();
        }

        public async Task<List<Proveedor>> ListarProveedorPaginado(int page, int pageSize)
        {
            return await context.Proveedor
                .OrderBy(p => p.id_Proveedor)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }



        //
    }

}




