using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Accesorios;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Modelo;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Accesorio;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Accesorio
{
    public class AccesorioServices : IAccesorioServices
    {

        private readonly IAccesorioRepository _accesorioRepository;


        public AccesorioServices(IAccesorioRepository accesorioRepository)
        {
            _accesorioRepository = accesorioRepository;
        }

        public async Task<List<Accesorio>> GetAccesorioRomWeb(int idemppaisnegcue)
        {
            var respuesta = await _accesorioRepository.GetAccesorioRomWeb(idemppaisnegcue);
            return respuesta;
        }

        //public async Task<List<Accesorio>> GetAccesorioRomBI()
        //{
        //    var respuesta = await _accesorioRepository.GetAccesorioRomBI();
        //    return respuesta;
        //}


        public async Task<Respuesta> PostAccesesorioRomWeb(Accesorio accesorio)
        {
            var respuesta = await _accesorioRepository.PostAccesesorioRomWeb(accesorio);
            return respuesta;
        }

        public async Task<Respuesta> DeleteAccesesorioRomWeb(Accesorio accesorio)
        {
            var respuesta = await _accesorioRepository.DeleteAccesesorioRomWeb(accesorio);
            return respuesta;
        }

    }
}
