using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Modelo;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Modelo
{
    public interface IModeloServices
    {
        Task<List<Modelo>> GetModeloRomWeb(int idemppaisnegcue);

        Task<Respuesta> PostModeloRomWeb(Modelo modelo);

        Task<Respuesta> DeleteModeloRomWeb(Modelo modelo);
    }
}
