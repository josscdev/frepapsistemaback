using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Bundles;
using RombiBack.Repository.ROM.ENTEL_TPF.MGM_MantenimientoTPF.MGM_BundleTPF;

namespace RombiBack.Services.ROM.ENTEL_TPF.MGM_MantenimientoTPF.MGM_BundleTPF
{
    public class BundleTPFServices : IBundleTPFServices
    {
        private readonly IBundleTPFRepository _bundleTPFRepository;
        public BundleTPFServices(IBundleTPFRepository bundleTPFRepository)
        {
            _bundleTPFRepository = bundleTPFRepository;
        }

        public async Task<List<ListarBundle>> GetBundlesTPF(int idemppaisnegcue)
        {
            var respuesta = await _bundleTPFRepository.GetBundlesTPF(idemppaisnegcue);
            return respuesta;
        }

        public async Task<ResultadoBundle> UpdateBundleTPF(ActualizarBundle actualizarbundle)
        {
            var respuesta = await _bundleTPFRepository.UpdateBundleTPF(actualizarbundle);
            return respuesta;
        }
    }
}
