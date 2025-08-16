using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Bundles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Bundles
{
    public interface IBundleRepository
    {
        Task<List<ListarBundle>> GetBundles(int idemppaisnegcue);
        Task<ResultadoBundle> UpdateBundle(ActualizarBundle actualizarbundle);

    }
}
