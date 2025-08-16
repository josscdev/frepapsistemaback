using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.ValidacionBundles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_ValidacionBundles
{
    public interface IValidacionBundlesRepository
    {
        Task<ValidacionBundle> GetBundlesVentas(int idventas);
        Task<Respuesta> ValidarCodigoAuthBundle(int idventasdetalle, string codigoauthbundle);
        Task<Respuesta> PostBundlesFirma(ValidacionBundle validacionbundle);
        Task<Respuesta> ValidarSubidaS3(int idventasdetalle);
    }
}
