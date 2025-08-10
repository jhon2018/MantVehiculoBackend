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
    public class DetalleReparacionDAO
    {
        private readonly db_abc1b8_jhtchecklist0725Context context = new db_abc1b8_jhtchecklist0725Context();


        public async Task<int> ObtenerORegistrarTipoReparacionAsync(string? descripcion)
        {
            var tipo = await context.tipoReparacion
                .FirstOrDefaultAsync(t => t.descripcion == descripcion);

            if (tipo != null) return tipo.id_tipoReparacion;

            var nuevoTipo = new tipoReparacion { descripcion = descripcion };
            await context.tipoReparacion.AddAsync(nuevoTipo);
            await context.SaveChangesAsync();

            return nuevoTipo.id_tipoReparacion;
        }

        public async Task<bool> RegistrarDetalleReparacionAsync(detalleReparacion detalle)
        {
            await context.detalleReparacion.AddAsync(detalle);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<detalleReparacion>> ListarDetallePorTipoAsync(int id_tipoReparacion)
        {
            return await context.detalleReparacion
                .Where(d => d.id_tipoReparacion == id_tipoReparacion)
                .OrderBy(d => d.id_detalleReparacion)
                .ToListAsync();
        }

        public async Task<bool> EditarAsync(detalleReparacion detalle)
        {
            var entidad = await context.detalleReparacion.FindAsync(detalle.id_detalleReparacion);
            if (entidad == null) return false;

            entidad.descripcion = detalle.descripcion;
            //entidad.costo = detalle.costo;
            entidad.id_tipoReparacion = detalle.id_tipoReparacion;

            context.detalleReparacion.Update(entidad);
            await context.SaveChangesAsync();
            return true;
        }



    }

}
