using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Modelo;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Modelo
{
    public interface IModeloRepository
    {
        Task<Respuesta> PostModeloRomWeb(Modelo modelo);
        Task<List<Modelo>> GetModeloRomWeb(int idemppaisnegcue);
        Task<Respuesta> DeleteModeloRomWeb(Modelo modelo);


    }
}
