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


    public class MantenimientoDAO
    {

        private readonly db_abc1b8_jhtchecklist0725Context context = new db_abc1b8_jhtchecklist0725Context();


        public async Task<bool> AsignarConductorAsync(int idMantenimiento, int idConductor)
        {
            var mantenimiento = await context.Mantenimiento.FindAsync(idMantenimiento);
            if (mantenimiento == null) return false;

            mantenimiento.id_Conductor = idConductor;
            await context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> ExisteMantenimientoSimilarAsync(Mantenimiento dto)
        {
            return await context.Mantenimiento.AnyAsync(m =>
                m.id_Vehiculo == dto.id_Vehiculo &&
                m.id_Conductor == dto.id_Conductor &&
                m.id_detalleReparacion == dto.id_detalleReparacion &&
                m.fecha_Mantenimiento == dto.fecha_Mantenimiento &&
                m.url_foto == dto.url_foto);
        }

    

        public async Task<bool> RegistrarAsync(Mantenimiento mantenimiento)
        {
            bool existe = await context.Mantenimiento.AnyAsync(m =>
                m.id_Vehiculo == mantenimiento.id_Vehiculo &&
                m.id_Conductor == mantenimiento.id_Conductor &&
                m.id_detalleReparacion == mantenimiento.id_detalleReparacion &&
                m.fecha_Mantenimiento == mantenimiento.fecha_Mantenimiento &&
                m.url_foto == mantenimiento.url_foto);

            if (existe) return false;

            await context.Mantenimiento.AddAsync(mantenimiento);
            await context.SaveChangesAsync();
            return true;
        }



        //
    }
}
