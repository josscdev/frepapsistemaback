using AutoMapper;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Bundles;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Bundles;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Ventas;
using RombiBack.Security.Model.UserAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Bundle
{
    public class BundleServices : IBundleServices
    {
        private readonly IBundleRepository _bundlesRepository;
        public BundleServices(IBundleRepository bundlerepository)
        {
            _bundlesRepository = bundlerepository;
        }
        public async Task<List<ListarBundle>> GetBundles(int idemppaisnegcue)
        {
            var respuesta = await _bundlesRepository.GetBundles(idemppaisnegcue);
            return respuesta;
        }

        public async Task<ResultadoBundle> UpdateBundle(ActualizarBundle actualizarbundle)
        {
            var respuesta = await _bundlesRepository.UpdateBundle(actualizarbundle);
            return  respuesta;
        }
    }
}
