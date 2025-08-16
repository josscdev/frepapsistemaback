using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Bundles;

namespace RombiBack.Services.ROM.ENTEL_TPF.MGM_MantenimientoTPF.MGM_BundleTPF
{
    public interface IBundleTPFServices
    {
        Task<List<ListarBundle>> GetBundlesTPF(int idemppaisnegcue);
        Task<ResultadoBundle> UpdateBundleTPF(ActualizarBundle actualizarbundle);
    }
}
