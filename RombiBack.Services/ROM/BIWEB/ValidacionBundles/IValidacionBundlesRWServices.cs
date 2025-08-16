using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.BIWEB.ValidacionBundles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.BIWEB.ValidacionBundles
{
    public interface IValidacionBundlesRWServices
    {
        Task<ValidacionBundlesRW> GetBundlesVentas([FromBody] int intIdVentasPrincipal);
        Task<RespuestaRW> ValidarCodigoAuthBundle(int intventasromid, string strcodigoauthbundle);
        Task<RespuestaRW> PostBundlesFirma(ValidacionBundlesRW validacionbundle);
        Task<RespuestaRW> ValidarSubidaS3(int intventasromid);
    }
}
