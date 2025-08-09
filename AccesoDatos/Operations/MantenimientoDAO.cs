using AccesoDatos.Context;
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


    }
}
