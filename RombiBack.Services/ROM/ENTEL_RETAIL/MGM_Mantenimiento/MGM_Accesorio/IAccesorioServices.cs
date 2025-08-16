using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Accesorios;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Accesorio
{
    public interface IAccesorioServices
    {
        Task<List<Accesorio>> GetAccesorioRomWeb(int idemppaisnegcue);
        //Task<List<Accesorio>> GetAccesorioRomBI();

        Task<Respuesta> PostAccesesorioRomWeb(Accesorio accesorio);
        Task<Respuesta> DeleteAccesesorioRomWeb(Accesorio accesorio);


    }
}
