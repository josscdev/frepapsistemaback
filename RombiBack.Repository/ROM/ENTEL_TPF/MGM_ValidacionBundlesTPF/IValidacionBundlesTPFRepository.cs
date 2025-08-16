
using RombiBack.Entities.ROM.BIWEB.ValidacionBundles;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.ValidacionBundles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.ENTEL_TPF.MGM_ValidacionBundlesTPF
{
    public interface IValidacionBundlesTPFRepository
    {
        Task<ValidacionBundle> GetBundlesVentasTPF(int idventas);
        Task<Respuesta> ValidarCodigoAuthBundleTPF(int idventasdetalle, string codigoauthbundle);
        Task<Respuesta> PostBundlesFirmaTPF(ValidacionBundle validacionbundle);
        Task<Respuesta> ValidarSubidaS3TPF(int idventasdetalle);
    }
}
