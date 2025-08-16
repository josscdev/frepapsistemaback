using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Accesorios;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_TPF.MGM_MantenimientoTPF.MGM_AccesorioTPF
{
    public interface IAccesorioTPFServices
    {
        Task<List<Accesorio>> GetAccesorioRomWebTPF(int idemppaisnegcue);
        //Task<List<Accesorio>> GetAccesorioRomBI();

        Task<Respuesta> PostAccesesorioRomWebTPF(Accesorio accesorio);
        Task<Respuesta> DeleteAccesesorioRomWebTPF(Accesorio accesorio);
    }
}
