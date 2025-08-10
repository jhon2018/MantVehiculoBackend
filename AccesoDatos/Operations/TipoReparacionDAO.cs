using AccesoDatos.Context;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Operations
{
    public class TipoReparacionDAO
    {

        private readonly db_abc1b8_jhtchecklist0725Context context = new db_abc1b8_jhtchecklist0725Context();


        public async Task<bool> RegistrarTipoReparacionAsync(tipoReparacion tipo)
        {
            bool existe = await context.tipoReparacion
                .AnyAsync(t => t.descripcion == tipo.descripcion);

            if (existe) return false;

            await context.tipoReparacion.AddAsync(tipo);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<int> ContarTipoReparaciones()
        {
            return await context.tipoReparacion.CountAsync();
        }

        public async Task<List<tipoReparacion>> ListarTipoReparacionPaginado(int page, int pageSize)
        {
            return await context.tipoReparacion
                .OrderBy(t => t.id_tipoReparacion)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<tipoReparacion?> ObtenerTipoPorIdAsync(int id_tipoReparacion)
        {
            return await context.tipoReparacion.FindAsync(id_tipoReparacion);
        }

        public async Task<bool> EditarAsync(tipoReparacion tiporeparacion)
        {
            var entidad = await context.tipoReparacion.FindAsync(tiporeparacion.id_tipoReparacion);
            if (entidad == null) return false;

            entidad.descripcion = tiporeparacion.descripcion;

            context.tipoReparacion.Update(entidad);
            await context.SaveChangesAsync();
            return true;
        }




    }
}
