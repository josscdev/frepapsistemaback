using RombiBack.Entities.ROM.BIWEB.ValidacionBundles;
using RombiBack.Repository.ROM.BIWEB.ValidacionBundles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.BIWEB.ValidacionBundles
{
    public class ValidacionBundlesRWServices:IValidacionBundlesRWServices
    {
        private readonly IValidacionBundlesRWRepository _validacionBundlesRepository;

        public ValidacionBundlesRWServices(IValidacionBundlesRWRepository validacionBundlesRepository)
        {
            _validacionBundlesRepository = validacionBundlesRepository;
        }
        public async Task<ValidacionBundlesRW> GetBundlesVentas(int intIdVentasPrincipal)
        {
            var respuesta = await _validacionBundlesRepository.GetBundlesVentas(intIdVentasPrincipal);
            return respuesta;
        }

        public async Task<RespuestaRW> ValidarCodigoAuthBundle(int intventasromid, string strcodigoauthbundle)
        {
            var respuesta = await _validacionBundlesRepository.ValidarCodigoAuthBundle(intventasromid, strcodigoauthbundle);
            return respuesta;
        }
        public async Task<RespuestaRW> PostBundlesFirma(ValidacionBundlesRW validacionbundle)
        {
            var respuesta = await _validacionBundlesRepository.PostBundlesFirma(validacionbundle);
            return respuesta;
        }
        public async Task<RespuestaRW> ValidarSubidaS3(int intventasromid)
        {
            var respuesta = await _validacionBundlesRepository.ValidarSubidaS3(intventasromid);
            return respuesta;
        }
    }
}
