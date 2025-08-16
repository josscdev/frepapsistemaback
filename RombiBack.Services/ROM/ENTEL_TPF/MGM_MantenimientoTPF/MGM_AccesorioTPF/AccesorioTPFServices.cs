using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Accesorios;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Repository.ROM.ENTEL_TPF.MGM_MantenimientoTPF.MGM_AccesorioTPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_TPF.MGM_MantenimientoTPF.MGM_AccesorioTPF
{
    public class AccesorioTPFServices : IAccesorioTPFServices
    {
        private readonly IAccesorioTPFRepository _accesorioTPFRepository;

        public AccesorioTPFServices(IAccesorioTPFRepository accesorioTPFRepository)
        {
            _accesorioTPFRepository = accesorioTPFRepository;
        }

        public async Task<List<Accesorio>> GetAccesorioRomWebTPF(int idemppaisnegcue)
        {
            var respuesta = await _accesorioTPFRepository.GetAccesorioRomWebTPF(idemppaisnegcue);
            return respuesta;
        }

        //public async Task<List<Accesorio>> GetAccesorioRomBI()
        //{
        //    var respuesta = await _accesorioRepository.GetAccesorioRomBI();
        //    return respuesta;
        //}


        public async Task<Respuesta> PostAccesesorioRomWebTPF(Accesorio accesorio)
        {
            var respuesta = await _accesorioTPFRepository.PostAccesesorioRomWebTPF(accesorio);
            return respuesta;
        }

        public async Task<Respuesta> DeleteAccesesorioRomWebTPF(Accesorio accesorio)
        {
            var respuesta = await _accesorioTPFRepository.DeleteAccesesorioRomWebTPF(accesorio);
            return respuesta;
        }
    }
}
