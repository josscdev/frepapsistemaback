using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Modelo;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.ENTEL_TPF.MGM_MantenimientoTPF.MGM_ModeloTPF
{
    public interface IModeloTPFRepository
    {
        Task<Respuesta> PostModeloRomWebTPF(Modelo modelo);
        Task<List<Modelo>> GetModeloRomWebTPF(int idemppaisnegcue);
        Task<Respuesta> DeleteModeloRomWebTPF(Modelo modelo);
    }
}
