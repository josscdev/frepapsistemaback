using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.ValidacionBundles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_ValidacionBundles
{
    public interface IValidacionBundlesServices
    {
        Task<ValidacionBundle> GetBundlesVentas([FromBody] int idventas);
        Task<Respuesta> ValidarCodigoAuthBundle(int intventasromid, string codigoauthbundle);
        Task<Respuesta> PostBundlesFirma(ValidacionBundle validacionbundle);
        Task<Respuesta> ValidarSubidaS3(int idventasdetalle);
    }
}
