using AutoMapper;
using RombiBack.Entities.ROM.BIWEB.ValidacionBundles;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.ValidacionBundles;
using RombiBack.Repository.ROM.ENTEL_TPF.MGM_Allocation;
using RombiBack.Repository.ROM.ENTEL_TPF.MGM_ValidacionBundlesTPF;
using RombiBack.Services.ROM.ENTEL_TPF.MGM_Allocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_TPF.MGM_ValidacionBundlesTPF
{
    public class ValidacionBundlesTPFServices : IValidacionBundlesTPFServices
    {
        private readonly IValidacionBundlesTPFRepository _validacionBundlesTPFRepository;


        public ValidacionBundlesTPFServices(IValidacionBundlesTPFRepository validacionBundlesTPFRepository, IMapper mapper)
        {
            _validacionBundlesTPFRepository = validacionBundlesTPFRepository;
        }
        public async Task<ValidacionBundle> GetBundlesVentasTPF(int idventas)
        {
            var respuesta = await _validacionBundlesTPFRepository.GetBundlesVentasTPF(idventas);
            return respuesta;
        }

        public async Task<Respuesta> ValidarCodigoAuthBundleTPF(int idventasdetalle, string codigoauthbundle)
        {
            var respuesta = await _validacionBundlesTPFRepository.ValidarCodigoAuthBundleTPF(idventasdetalle, codigoauthbundle);
            return respuesta;
        }
        public async Task<Respuesta> PostBundlesFirmaTPF(ValidacionBundle validacionbundle)
        {
            var respuesta = await _validacionBundlesTPFRepository.PostBundlesFirmaTPF(validacionbundle);
            return respuesta;
        }
        public async Task<Respuesta> ValidarSubidaS3TPF(int idventasdetalle)
        {
            var respuesta = await _validacionBundlesTPFRepository.ValidarSubidaS3TPF(idventasdetalle);
            return respuesta;
        }

    }
}
