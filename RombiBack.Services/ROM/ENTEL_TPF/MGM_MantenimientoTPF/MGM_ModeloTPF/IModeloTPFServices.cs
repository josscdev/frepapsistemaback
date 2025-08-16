using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Modelo;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_TPF.MGM_MantenimientoTPF.MGM_ModeloTPF
{
    public interface IModeloTPFServices
    {
        Task<List<Modelo>> GetModeloRomWebTPF(int idemppaisnegcue);

        Task<Respuesta> PostModeloRomWebTPF(Modelo modelo);

        Task<Respuesta> DeleteModeloRomWebTPF(Modelo modelo);
    }
}
